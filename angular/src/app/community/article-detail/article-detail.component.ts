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
  // data = {
  //   author: 'Han Solo',
  //   avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
  //   content:
  //     'We supply a series of design principles, practical patterns and high quality design resources' +
  //     '(Sketch and Axure), to help people create their product prototypes beautifully and efficiently.',
  //   children: [
  //     {
  //       author: 'Han Solo',
  //       avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
  //       content:
  //         'We supply a series of design principles, practical patterns and high quality design resources' +
  //         '(Sketch and Axure), to help people create their product prototypes beautifully and efficiently.',
  //       children: [
  //         {
  //           author: 'Han Solo',
  //           avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
  //           content:
  //             'We supply a series of design principles, practical patterns and high quality design resources' +
  //             '(Sketch and Axure), to help people create their product prototypes beautifully and efficiently.'
  //         },
  //         {
  //           author: 'Han Solo',
  //           avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
  //           content:
  //             'We supply a series of design principles, practical patterns and high quality design resources' +
  //             '(Sketch and Axure), to help people create their product prototypes beautifully and efficiently.'
  //         }
  //       ]
  //     }
  //   ]
  // };
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

  auditBtnLoading:boolean=false;

  commentsCount: number = 0;

  articleId: number;

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
  }

  ngOnInit() {
    this.article = new ArticleDetailDto();
    this.currentUser = new CurrentUserDetailDto();
    this.search.articleId = this.articleId;
    this.getCurrentUser();
    this.getArticleDetailById();
    this.getCommentsByArticleId();
  }

  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      if (result.userId != undefined) {
        this.isLogin = true;
        this.currentUser = result;
        this.headurl = this.userDetailService.baseUrl + result.headPortraitUrl;
        console.log(this.currentUser);
      }
    })
  }

  getArticleDetailById(): void {
    this.articleService.getArticleById(this.articleId).subscribe((result) => {
      this.article = result;
    })
  }

  getCommentsByArticleId(): void {
    this.articleService.getCommentsByArticleId(this.search).subscribe((result) => {
      this.commentsCount = result.totalCount;
      this.comments = CommentDto.fromJSArray(result.items);
      this.setCommentsAvatar(this.comments);
      console.log(this.comments);

    })
  }

  //递归对用户头像赋值
  setCommentsAvatar(comments: CommentDto[]) {
    comments.forEach(element => {
      element.avatar = this.userDetailService.baseUrl + element.avatar;
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
    // console.log(this.comment);
    // return;
    this.articleService.createComment(this.comment).subscribe((result) => {
      if (result) {
        this.notify.success("成功");
        this.submitting = false;
        //刷新
        this.getCommentsByArticleId();
      }
    })
  }

  reply(entity: CommentDto) {
    console.log("reply")
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
      }
      this.auditBtnLoading = false;
    })
  }

}
