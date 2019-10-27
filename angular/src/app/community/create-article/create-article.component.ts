import { Component, OnInit, SimpleChanges, Injector } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/component-base';
import { ArticleDetailDto, ArticleCategoryDto, SelectArticleCategoryDto } from 'entities';
import { ArticleService, UserDetailService } from 'services';

@Component({
  selector: 'app-create-article',
  templateUrl: './create-article.component.html',
  styleUrls: ['./create-article.component.css']
})

export class CreateArticleComponent extends AppComponentBase implements OnInit {

  validateForm: FormGroup;
  isConfirmLoading: boolean = false;
  content: string;
  config: any = {
    initialFrameHeight: 500,
    
  };

  article: ArticleDetailDto //= new ArticleDetailDto();
  currentUserId: string;


  constructor(injector: Injector
    , private fb: FormBuilder
    , private router: Router
    , private articleService: ArticleService
    , private userDetailService: UserDetailService) {
    super(injector);

    this.validateForm = this.fb.group({
      name: [null, [Validators.required]],
      tags: [null]
    });
  }

  //标签
  listOfOption: Array<{ label: string; value: number }> = [];
  //已选中标签
  listOfTagOptions: number[] = [];


  ngOnInit() {
    this.article = new ArticleDetailDto();
    this.getCurrentUser();
    this.getAllArticleCategories();
    
  }

  getAllArticleCategories(): void {
    this.articleService.getAllArticleCategories().subscribe((result) => {
      console.log(result);
      if (result.length > 0) {
        result.forEach(item => {
          this.listOfOption.push({ label: item.lable, value: item.id });
        });
      }
    })
  }


  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      console.log(result);
      if (result.userId != undefined) {
        this.currentUserId = result.userDetailId;
      }
      else {
        this.router.navigate(["account/login"]);
      }
    })
  }

  contentChange(content: SimpleChanges): void {
    console.log(content);

  }

  saveArticle(status:number):void{
    this.article.releaseStatus = status;
    //this.save();
  }
  
  save(): void {
    this.isConfirmLoading = true;
    //标签
    this.article.content = this.content;
    this.article.userDetailId = this.currentUserId;
    this.article.categoryIds = this.listOfTagOptions;
    if (!this.validateForm.valid) {
      return;
    }
    if (this.content == undefined || this.content.length < 20) {
      this.notify.error("文章内容过短。");
      return;
    }
    console.log(this.article);
    return;
    this.articleService.createOrUpdateArticle(this.article).subscribe((result) => {
      if (result) {
        this.isConfirmLoading = false;
        this.message.success("成功")
        this.router.navigate(["app/community"])
      }
    })

  }

  back(): void {
    //this.router.navigate()
  }

}
