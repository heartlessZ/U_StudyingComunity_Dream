import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { BookService } from 'services';
import { BookCategoryDto } from 'entities';
import { BookDetailDto } from 'entities/book-detail';
import { Observable, Observer } from 'rxjs';
import { UploadFile, NzModalService, NzCascaderOption } from 'ng-zorro-antd';
import { BookResourceDto } from 'entities/book-resource';

@Component({
  selector: 'app-book-detail-modal',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  validateForm: FormGroup;
  emodalVisible = false;//模态框是否显示
  isOkLoading = false;
  loading = false;
  name: string;
  book: BookDetailDto;
  title: string;
  isEdit: boolean;
  resourceId: number;

  avatarUrl: string;
  fileUploadUrl: string;
  fileUrl: string;//图片最终上传所得的地址

  
  nzOptions: NzCascaderOption[];
  //nzOptions:SelectBookCategory[];
  values: string[] | null = null;


  fileList: BookResourceDto[] = [];
  newFileList: BookResourceDto[] = [];

  constructor(injector: Injector,
    private fb: FormBuilder,
    private bookService: BookService,
    private modal: NzModalService) {
    super(injector);

    this.validateForm = this.fb.group({
      name: ['', [Validators.required]],
      author: ['', [Validators.required]],
      otherUrls: [null],
      description: [null],
      values: [null],
    });
  }

  protected setFormValues(entity: BookDetailDto): void {
    this.setControlVal('name', entity.name);
    this.setControlVal('author', entity.author);
  }

  
  setControlVal(name: string, val: any) {
    this.validateForm.controls[name].setValue(val);
  }

  /**
   * 显示模态框（进入用户详情页）
   */
  show(id?: number) {
    this.emodalVisible = true;
    this.getBookCategories();
    if (id == undefined) {
      this.title = "新增书籍"
      this.isEdit = false;
      this.book = new BookDetailDto();
    } else {
      this.title = "编辑书籍"
      this.isEdit = true;
      this.getBookDetailById(id);
    }
  }

  
  getBookCategories():void{
    this.bookService.getBookCategoriesSelect().subscribe((result)=>{
      this.nzOptions = result;
      console.log(this.nzOptions);
    })
  }

  getBookDetailById(id: number): void {
    this.bookService.getBookDetailById(id).subscribe((result) => {
      console.log(result);
      this.book = result;
      this.values.push(this.book.categoryId.toString());
    });
  }

  handleOk(): void {
    this.isOkLoading = true;
    //管理员添加默认审核通过
    this.book.status = 1;
    //this.submitForm();
    if (!this.validateForm.valid)
      return;
    
    this.bookService.createOrUpdateBook(this.book).subscribe((result) => {
      if (result) {
        this.message.success("成功");
        this.emodalVisible = false;
        this.isOkLoading = false;
        this.modalSave.emit(null);
      }
    })
  }

  handleCancel(): void {
    this.emodalVisible = false;
    this.validateForm.reset();
  }

  ngOnInit() {

  }

  submitForm(value: any): void {
    for (const key in this.validateForm.controls) {
      this.validateForm.controls[key].markAsDirty();
      this.validateForm.controls[key].updateValueAndValidity();
    }
  }

  beforeUpload = (file: File) => {
    return new Observable((observer: Observer<boolean>) => {
      const isJPG = file.type === 'image/jpeg';
      if (!isJPG) {
        this.message.error('You can only upload JPG file!');
        observer.complete();
        return;
      }
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.message.error('Image must smaller than 2MB!');
        observer.complete();
        return;
      }
      // check height
      this.checkImageDimension(file).then(dimensionRes => {
        if (!dimensionRes) {
          this.message.error('Image only 300x300 above');
          observer.complete();
          return;
        }

        observer.next(isJPG && isLt2M && dimensionRes);
        observer.complete();
      });
    });
  };

  private getBase64(img: File, callback: (img: string) => void): void {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result!.toString()));
    reader.readAsDataURL(img);
  }

  private checkImageDimension(file: File): Promise<boolean> {
    return new Promise(resolve => {
      const img = new Image(); // create image
      img.src = window.URL.createObjectURL(file);
      img.onload = () => {
        const width = img.naturalWidth;
        const height = img.naturalHeight;
        window.URL.revokeObjectURL(img.src!);
        resolve(width === height && width >= 300);
      };
    });
  }

  handleChange(info: { file: UploadFile }): void {
    switch (info.file.status) {
      case 'uploading':
        this.loading = true;
        break;
      case 'done':
        this.fileUrl = info.file.response.result.data.url;
        // Get this url from response in real world.
        this.getBase64(info.file!.originFileObj!, (img: string) => {
          this.loading = false;
          this.avatarUrl = img;
        });
        break;
      case 'error':
        this.message.error('上传失败，请稍后重试');
        this.loading = false;
        break;
    }
  }

  //图书资源上传
  handleChangeBookResource(info: { file: UploadFile }): void {

  }

  deleteBookResource = (file: UploadFile): boolean => {
    // console.log(file);
    // console.log(this.fileList);
    // return true;
    if (file) {
      this.modal.confirm({
        nzContent: '确定是否删除资料文档?',
        nzOnOk: () => {
          if (this.resourceId) {
            this.bookService.deleteBookResourceById(file.uid).subscribe(() => {
              this.notify.success('删除成功！', '');
              //this.getAttachmentList();
              // this.fileList.pop();
              let tflist = JSON.parse(JSON.stringify(this.fileList));
              tflist.pop();
              this.fileList = tflist;
              return true;
            });
          } else {
            let tflist: BookResourceDto[] = BookResourceDto.fromJSArray(JSON.parse(JSON.stringify(this.fileList)));
            tflist.pop();
            this.newFileList = [];
            this.fileList = tflist;
          }
        }
      });
      return false;
    }
  }

  //分类选择器改变事件
  onChanges(values: string[]): void {
    console.log(values, this.values);
  }
}
