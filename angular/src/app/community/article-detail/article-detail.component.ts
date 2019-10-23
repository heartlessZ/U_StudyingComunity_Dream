import { Component, OnInit, Injector } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ArticleService, UserDetailService } from 'services';
import { AppComponentBase } from '@shared/component-base';
import { ArticleDetailDto } from 'entities';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent extends AppComponentBase implements OnInit {

  validateForm: FormGroup;
  isConfirmLoading:boolean = false;
  content: string;
  config: any = {
    initialFrameHeight: 500
  };

  article:ArticleDetailDto //= new ArticleDetailDto();

  articleId:number;

  constructor(injector: Injector
    ,private fb: FormBuilder
    , private router: Router
    , private actRouter: ActivatedRoute
    , private articleService:ArticleService
    , private userDetailService : UserDetailService) {
      super(injector);

    // this.validateForm = this.fb.group({
    //   name: [null, [Validators.required]],
    //   tags: [null]
    // });
    this.articleId = this.actRouter.snapshot.params['id'];
  }

  ngOnInit() {
    this.article = new ArticleDetailDto();
    this.getArticleDetailById();
  }

  getArticleDetailById():void{
    this.articleService.getArticleById(this.articleId).subscribe((result)=>{
      this.article = result;
    })
  }

}
