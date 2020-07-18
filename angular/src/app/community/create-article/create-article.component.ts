import { Component, OnInit, SimpleChanges, Injector, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/component-base';
import { ArticleDetailDto, ArticleCategoryDto, SelectArticleCategoryDto } from 'entities';
import { ArticleService, UserDetailService } from 'services';
// import { NgxNeditorComponent } from '../../projects/notadd/ngx-neditor/src/public_api';

@Component({
  selector: 'app-create-article',
  templateUrl: './create-article.component.html',
  styleUrls: ['./create-article.component.css']
})

export class CreateArticleComponent extends AppComponentBase implements OnInit {
  // @ViewChild('neditor', { static: true }) neditor: NgxNeditorComponent;

  id: any;
  validateForm: FormGroup;
  isConfirmLoading = false;
  content: string;
  config: any = {
    initialFrameHeight: 500,

  };

  article: ArticleDetailDto; // = new ArticleDetailDto();
  currentUserId: string;

  // 标签
  listOfOption: Array<{ label: string; value: number }> = [];
  // 已选中标签
  listOfTagOptions: number[] = [];


  constructor(injector: Injector
    , private fb: FormBuilder
    , private router: Router
    , private actRouter: ActivatedRoute
    , private articleService: ArticleService
    , private userDetailService: UserDetailService) {
    super(injector);
    this.id = this.actRouter.snapshot.params['id'];

    this.validateForm = this.fb.group({
      name: [null, [Validators.required]],
      description: [null, [Validators.required]],
      tags: [null, [Validators.required]]
    });
  }


  ngOnInit() {
    this.article = new ArticleDetailDto();
    this.getCurrentUser();
    this.getAllArticleCategories();

    if (this.id != undefined) {
      this.getArticleById();
    }
  }

  getAllArticleCategories(): void {
    this.articleService.getAllArticleCategories().subscribe((result) => {
      // console.log(result);
      if (result.length > 0) {
        result.forEach(item => {
          this.listOfOption.push({ label: item.lable, value: item.id });
        });
      }
    });
  }

  getArticleById(): void {
    this.articleService.getArticleById(this.id).subscribe((result) => {
      this.article = result;
      this.listOfTagOptions = result.categoryIds;
      this.content = result.content;
      // console.log(result);

    });
  }

  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      // console.log(result);
      if (result.userId != undefined) {
        this.currentUserId = result.userDetailId;
      } else {
        this.router.navigate(['account/login']);
      }
    });
  }

  contentChange(content: SimpleChanges): void {
    // console.log(content);

  }

  saveArticle(status: number): void {
    this.article.releaseStatus = status;
    // this.save();
  }

  save(): void {
    this.isConfirmLoading = true;
    // 标签
    this.article.content = this.content;
    this.article.userDetailId = this.currentUserId;
    this.article.categoryIds = this.listOfTagOptions;
    if (!this.validateForm.valid) {
      return;
    }
    if (this.content == undefined || this.content.length < 20) {
      this.notify.error('文章内容过短。');
      this.isConfirmLoading = false;
      return;
    }
    this.articleService.createOrUpdateArticle(this.article).subscribe((result) => {
      if (result) {
        this.isConfirmLoading = false;
        this.message.success('成功');
        this.router.navigate(['app/community']);
      }
    });

  }

  back(): void {
    // this.router.navigate()
  }

}
