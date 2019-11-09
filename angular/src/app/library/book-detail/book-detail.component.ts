import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { BookDetailDto, BookResourceDto, CurrentUserDetailDto } from 'entities';
import { BookService, UserDetailService } from 'services';
import { NzMessageService } from 'ng-zorro-antd/message';
import { UploadFile, UploadFilter } from 'ng-zorro-antd/upload';
import { Observable, Observer } from 'rxjs';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent extends AppComponentBase implements OnInit {


  id: any = '';
  book: BookDetailDto = new BookDetailDto();
  bookResources: BookResourceDto[];
  fileDownloadUrl: string;
  coverUrl: string;//封面地址
  thirdUrls: string[] = [];

  currentUser: CurrentUserDetailDto;
  isLogin: boolean = false;
  headUrl: string = "";

  baseUrl:string;
  isAlreadyPraise:boolean=false;//是否已经点过赞了

  constructor(injector: Injector
    , private actRouter: ActivatedRoute
    , private router: Router
    , private bookService: BookService
    , private userDetailService: UserDetailService) {
    super(injector);

    this.id = this.actRouter.snapshot.params['id'];
  }

  ngOnInit() {
    this.baseUrl = this.bookService.baseUrl;
    this.fileUploadUrl = this.bookService.baseUrl + "/api/File/upload";
    this.fileDownloadUrl = this.bookService.baseUrl + "/api/File/download";
    this.getBookDetailById();
    this.getCurrentUser();
  }


  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      console.log(result);
      if (result.userId != undefined) {
        this.isLogin = true;
        this.currentUser = result;
        this.headUrl = this.userDetailService.baseUrl + result.headPortraitUrl;
        console.log(this.headUrl);

      }
    })
  }

  getBookDetailById(): void {
    this.bookService.getBookDetailById(this.id).subscribe((result) => {
      //console.log(result);

      this.book = result;
      //封面地址拼接
      this.coverUrl = this.bookService.baseUrl + this.book.coverUrl;

      if (this.book.otherUrls != null) {
        let urls = this.book.otherUrls.split(',');
        urls.forEach(url => {
          this.thirdUrls.push(url);
        });
      }
      console.log(this.thirdUrls);


      //获取资源集合
      this.getResourceListByBookId();
    });
  }

  getResourceListByBookId(): void {
    this.bookService.getResourceListByBookId(this.id, 2).subscribe((result) => {
      this.bookResources = result;
      this.bookResources.forEach(i => i.url = this.fileDownloadUrl + "?url=" + i.url);

      console.log(this.book);
      console.log(this.bookResources);
    });
  }

  filters: UploadFilter[] = [
    {
      name: 'type',
      fn: (fileList: UploadFile[]) => {
        const filterFiles = fileList.filter(w => ~['application/pdf'].indexOf(w.type));
        if (filterFiles.length !== fileList.length) {
          this.notify.error(`包含文件格式不正确，只支持 pdf 格式`);
          return filterFiles;
          //return [];
        }
        return fileList;
        //return [];
      }
    }
  ];


  fileUploadUrl: string;

  fileList = [

  ];

  bookResource: BookResourceDto;

  beforeUpload = (file: File) => {
    if (this.fileList.length >= 1) {
      this.notify.error('只能上传一个附件,请先删除原有附件');
      return false;
    }
    const isLt100M = file.size / 1024 / 1024 < 100;
    if (!isLt100M) {
      this.message.error('封面大小不超过100M！');
      return false;
    }
    return true;
  };

  //图书资源上传
  handleChange(info: { file: UploadFile }): void {
    console.log(info.file);
    //return;
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
          this.bookResource.status = 1;
          this.bookResource.uploader = this.currentUser.userDetailId;
          this.saveBookResource(this.bookResource);
        } else {
          this.notify.error(res.msg);
          this.fileList.pop();
        }
      }
    }
  }

  //保存书籍资源
  saveBookResource(bookResource: BookResourceDto): void {
    console.log(bookResource);
    // return;
    this.bookService.createBookResource(bookResource).subscribe((result) => {
      this.notify.success('分享成功,请等待管理员审核');
    })
  }

  downloadResource(url:any){
    console.log(url);
    //return;
    this.bookService.downloadResource(url).subscribe((result)=>{

    })
  }

  back():void{
    this.router.navigate(["app/library"])
  }

  createPraise():void{
    if(this.isAlreadyPraise){
      this.notify.warn("点赞虽爽，可不要贪多哟")
    }else{
      this.bookService.createPraise(this.id).subscribe((result)=>{
        if(result){
          this.notify.success("点赞成功")
          this.isAlreadyPraise=true;
        }
      })
    }
  }

}
