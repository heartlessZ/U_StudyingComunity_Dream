import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { BookService } from 'services';
import { BookCategoryDto } from 'entities';
import { BookDetailDto } from 'entities/book-detail';
import { Observable, Observer } from 'rxjs';
import { UploadFile, NzModalService, NzCascaderOption, UploadFilter } from 'ng-zorro-antd';
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
  bookResource: BookResourceDto;

  avatarUrl: string;
  fileUploadUrl: string;
  fileDownloadUrl:string;
  fileUrl: string;//图片最终上传所得的地址


  nzOptions: NzCascaderOption[];
  //nzOptions:SelectBookCategory[];
  values: string[] | null = null;


  fileList: BookResourceDto[] = [];
  newFileList: BookResourceDto[] = [];
  filters: UploadFilter[] = [
    {
      name: 'type',
      fn: (fileList: UploadFile[]) => {
        const filterFiles = fileList.filter(w => ~['application/pdf'].indexOf(w.type));
        if (filterFiles.length !== fileList.length) {
          this.notify.error(`包含文件格式不正确，只支持 pdf 格式`);
          return filterFiles;
        }
        return fileList;
      }
    }
  ];

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
      values: ['', [Validators.minLength(1)]],
    });
  }

  protected setFormValues(entity: BookDetailDto): void {
    this.setControlVal('name', entity.name);
    this.setControlVal('author', entity.author);
  }


  setControlVal(name: string, val: any) {
    this.validateForm.controls[name].setValue(val);
  }

  searchCategoryName(id: string): any {
    return this.nzOptions.filter(i => i.value == id);
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
    this.setFormValues(this.book);
  }


  getBookCategories(): void {
    this.bookService.getBookCategoriesSelect().subscribe((result) => {
      this.nzOptions = result;
      //console.log(this.nzOptions);
    })
  }

  getBookDetailById(id: number): void {
    this.bookService.getBookDetailById(id).subscribe((result) => {
      //console.log(result);
      this.book = result;
      //this.values = this.searchCategoryName(this.book.categoryId.toString())
      this.values = [this.book.categoryId.toString()]
      //this.book.categoryName = this.searchCategoryName(this.book.categoryId.toString())
      this.setFormValues(this.book);

      //封面赋值
      this.avatarUrl = this.bookService.baseUrl + this.book.coverUrl;

      //获取资源集合
      this.getResourceListByBookId(this.book.id);
    });
  }

  getResourceListByBookId(id:number):void{
    this.bookService.getResourceListByBookId(id).subscribe((result)=>{
      //console.log(result);
      
      this.fileList = result;
      this.fileList.forEach(i=>i.url = this.fileDownloadUrl + "?url=" + i.url);
      //console.log(this.fileList);
    });
  }

  handleOk(): void {
    this.isOkLoading = true;
    //管理员添加默认审核通过
    this.book.coverUrl = this.fileUrl;
    this.book.status = 2;
    if (this.values != null || this.values != undefined)
      this.book.categoryId = Number(this.values.reverse()[0]);
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
    this.fileUploadUrl = this.bookService.baseUrl+"/api/File/upload";
    this.fileDownloadUrl = this.bookService.baseUrl+"/api/File/download";
    this.book = new BookDetailDto();
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
        this.message.error('只能上传jpg格式的图片！');
        observer.complete();
        return;
      }
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.message.error('封面大小不超过2M！');
        observer.complete();
        return;
      }
      // check height
      this.checkImageDimension(file).then(dimensionRes => {
        // if (!dimensionRes) {
        //   this.message.error('Image only 300x300 above');
        //   observer.complete();
        //   return;
        // }

        //observer.next(isJPG && isLt2M && dimensionRes);
        observer.next(isJPG && isLt2M);
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
    this.bookResource = new BookResourceDto();
    if (info.file.status === 'error') {
      this.notify.error('上传文件异常，请重试');
      this.fileList.pop();
    }

    else {
      if (info.file.status === 'done') {
        var res = info.file.response.result;
        if (res.code == 0) {
          this.fileList.forEach(element => {
            if (info.file.uid == element.id.toString()) {
              element.url = res.data.url;
            }
          });
          this.bookResource.bookId = this.book.id;
          this.bookResource.name = info.file.name;
          this.bookResource.url = res.data.url;
          this.bookResource.status = 2;
          this.saveBookResource(this.bookResource);
        } else {
          this.notify.error(res.msg);
          this.fileList.pop();
        }
      }
    }
  }

  //保存书籍资源
  saveBookResource(bookResource:BookResourceDto):void{
    //console.log(bookResource);
    // return;
    this.bookService.createBookResource(bookResource).subscribe((result)=>{
      this.notify.success('上传文件成功');
    })
  }

  deleteBookResource = (file: UploadFile): boolean => {
    // //console.log(file);
    // //console.log(this.fileList);
    // return false;
    if (file) {
      this.modal.confirm({
        nzContent: '确定是否删除资料文档?',
        nzOnOk: () => {
          if (file.id) {
            this.bookService.deleteBookResourceById(file.id).subscribe(() => {
              //console.log(file.id);
              
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
    return false;
  }

  beforeUploadResource = (file: File) => {
    return true;
  };

  //分类选择器改变事件
  onChanges(values: string[]): void {
    //console.log(values, this.values);
  }
}
