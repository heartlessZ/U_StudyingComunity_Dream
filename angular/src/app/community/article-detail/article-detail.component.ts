import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ArticleService, UserDetailService } from 'services';
import { AppComponentBase } from '@shared/component-base';
import { ArticleDetailDto, CurrentUserDetailDto, CommentCreate, CommentDto } from 'entities';
import { ReplyModalComponent } from './reply-modal/reply-modal.component';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent extends AppComponentBase implements OnInit {
  @ViewChild('replyModal', { static: true }) replyModal: ReplyModalComponent;

  validateForm: FormGroup;
  isConfirmLoading: boolean = false;
  comments: CommentDto[];
  content: string;
  config: any = {
    initialFrameHeight: 500
  };
  search: any = { articleId: 0, maxResultCount: 10, skipCount: 0 };

  article: ArticleDetailDto //= new ArticleDetailDto();
  currentUser: CurrentUserDetailDto;
  headurl: string;
  isLogin: boolean = false;
  isAdmin: boolean = false;
  isCurrentUser:boolean=false;//当前登录用户是否为文章作者
  isAlreadyPraise:boolean=false;//是否已经点过赞了

  auditBtnLoading:boolean=false;

  commentsCount: number = 0;

  articleId: number;
  baseUrl:string;

  constructor(injector: Injector
    , private fb: FormBuilder
    , private router: Router
    , private actRouter: ActivatedRoute
    , private articleService: ArticleService
    , private userDetailService: UserDetailService) {
    super(injector);

    // this.validateForm = this.fb.group({
    //   name: [null, [Validators.required]],
    //   tags: [null]
    // });
    this.articleId = this.actRouter.snapshot.params['id'];
    this.baseUrl = this.articleService.baseUrl;
  }

  ngOnInit() {
    $("div#banner").removeClass('homepage-mid-read');
    $("div#banner").removeClass('homepage-mid-community');
    $("div#banner").removeClass('homepage-mid-personal');
    $("div#banner").removeClass('homepage-mid-learning');
    $("div#banner").removeClass('homepage-mid-library');
    this.article = new ArticleDetailDto();
    this.currentUser = new CurrentUserDetailDto();
    this.search.articleId = this.articleId;
    this.getArticleDetailById();
    this.getCurrentUser();
    this.getCommentsByArticleId();
  }

  createVisitVolume():void{
    this.articleService.createVisitVolume(this.articleId).subscribe(()=>{

    })
  }

  createPraise():void{
    if(this.isAlreadyPraise){
      this.notify.warn("点赞虽爽，可不要贪多哟")
    }else{
      this.articleService.createPraise(this.articleId).subscribe((result)=>{
        if(result){
          this.notify.success("点赞成功")
          this.isAlreadyPraise=true;
        }
      })
    }
  }

  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      if (result.userId != undefined) {
        this.isLogin = true;
        this.currentUser = result;
        this.headurl = this.userDetailService.baseUrl + result.headPortraitUrl;
        //console.log(this.currentUser);

        if(this.article.userDetailId == this.currentUser.userDetailId){
          this.isCurrentUser = true;
        }else{
          //如果不是当前登录用户在查看文章则添加一条访问记录
          this.createVisitVolume();
        }
      }
    })
  }

  getArticleDetailById(): void {
    this.articleService.getArticleById(this.articleId).subscribe((result) => {
      this.article = result;
      //this.article.creationTime = new Date(this.article.creationTime).getTimezoneOffset
    })
  }

  getCommentsByArticleId(): void {
    this.articleService.getCommentsByArticleId(this.search).subscribe((result) => {
      this.commentsCount = result.totalCount;
      this.comments = CommentDto.fromJSArray(result.items);
      this.setCommentsAvatar(this.comments);
      //console.log(this.comments);
    })
  }

  //递归对用户头像赋值
  setCommentsAvatar(comments: CommentDto[]) {
    comments.forEach(element => {
      element.avatar = this.userDetailService.baseUrl + element.avatar;
      //element.creationTime = element.creationTime.ToString("yy-MM-dd HH:mm:ss")
      if (element.children.length > 0)
        this.setCommentsAvatar(element.children)
    });
  }

  submitting = false;
  inputValue = '';
  comment: CommentCreate;

  handleSubmit(parent: number): void {
    this.submitting = true;
    this.comment = new CommentCreate();
    this.comment.content = this.inputValue;
    this.comment.articleId = this.articleId;
    this.comment.userDetailId = this.currentUser.userDetailId;
    this.comment.parent = parent;
    // //console.log(this.comment);
    // return;
    this.articleService.createComment(this.comment).subscribe((result) => {
      if (result) {
        this.notify.success("成功");
        this.submitting = false;
        this.inputValue = '';
        //刷新
        this.getCommentsByArticleId();
      }
    })
  }

  reply(entity: CommentDto) {
    //console.log("reply")
    this.comment = new CommentCreate();
    this.comment.articleId = this.articleId;
    this.comment.userDetailId = this.currentUser.userDetailId;
    this.comment.parent = entity.id;
    this.comment.author = entity.author;
    this.replyModal.show(this.comment);
  }

  toLogin(): void {
    this.router.navigate(["account/login"])
  }

  createComment(): void {

  }

  audit(status:number){
    this.auditBtnLoading = true;
    this.articleService.updateArticleStatus(this.article.id,status).subscribe((result)=>{
      if(result){
        this.notify.success("审核成功");
        this.getArticleDetailById();
        //this.router.
      }
      this.auditBtnLoading = false;
    })
  }

  goUserDetail(userDetailId:any):void{
    this.router.navigate(["app/personal-center/"+userDetailId]);
  }

  Anotify(): void {
    //console.log('notify');
  }

  delete(id:any){
    this.articleService.deleteComment(id).subscribe((result)=>{
      this.notify.success("删除成功");
      this.getCommentsByArticleId();
    })
  }

}
