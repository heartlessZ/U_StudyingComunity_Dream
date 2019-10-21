import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { BookDetailDto, BookResourceDto } from 'entities';
import { BookService } from 'services';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent extends AppComponentBase implements OnInit {

  
  id: any = '';
  book: BookDetailDto;
  bookResources:BookResourceDto[];
  fileDownloadUrl:string;
  coverUrl:string;//封面地址

  constructor(injector: Injector
    , private actRouter: ActivatedRoute
    , private router: Router
    , private bookService: BookService) {
      super(injector); 
      
      this.id = this.actRouter.snapshot.params['id'];
    }

  ngOnInit() {
    
    this.fileDownloadUrl = this.bookService.baseUrl+"/api/File/download";
    this.getBookDetailById();
  }

  
  getBookDetailById(): void {
    this.bookService.getBookDetailById(this.id).subscribe((result) => {
      this.book = result;
      //封面地址拼接
      this.coverUrl = this.bookService.baseUrl + this.book.coverUrl;

      //获取资源集合
      this.getResourceListByBookId();
    });
  }

  getResourceListByBookId():void{
    this.bookService.getResourceListByBookId(this.id).subscribe((result)=>{
      this.bookResources = result;
      this.bookResources.forEach(i=>i.url = this.fileDownloadUrl + "?url=" + i.url);
    });
  }

}
