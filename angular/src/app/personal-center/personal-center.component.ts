import { Component, OnInit, Injector, Input, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { ActivatedRoute } from '@angular/router'
import { UserDetailService } from 'services';
import { UserDetailDto, CurrentUserDetailDto } from 'entities';
import { UserDetailEditComponent } from './user-detail-edit/user-detail-edit.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ArticleAndProjectComponent } from './article-and-project/article-and-project.component';

@Component({
  selector: 'app-personal-center',
  templateUrl: './personal-center.component.html',
  styleUrls: [
    './personal-center.component.css'
  ],
})
export class PersonalCenterComponent extends AppComponentBase implements OnInit {
  @ViewChild('userDetailEditModal', { static: true }) userDetailEditModal: UserDetailEditComponent;
  @ViewChild('changePasswordModal', { static: true }) changePasswordModal: ChangePasswordComponent;
  @ViewChild('ArticleAndProjectComponent', { static: true }) ArticleAndProjectComponent: ArticleAndProjectComponent;
  @Input() userDetailId;

  user: UserDetailDto = new UserDetailDto();
  currentUser: CurrentUserDetailDto = new CurrentUserDetailDto();
  headUrl: string;
  isCurrentUser: boolean = false;;
  isAttention: boolean = false;

  constructor(injector: Injector,
    private actRouter: ActivatedRoute,
    private userDetailService: UserDetailService) {
    super(injector);
    this.userDetailId = this.actRouter.snapshot.params['id'];
  }

  ngOnInit() {
    this.getCurrentUser();
    this.getIsAttentionUser();
  }

  //获取当前登录用户
  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      if (result.userId != undefined) {
        this.currentUser = result;
        this.getUserDetail();
      }
    })
  }

  //获取当前用户是否已经关注该用户
  getIsAttentionUser(): void {
    this.userDetailService.getIsAttentionUser(this.user.id, this.currentUser.userDetailId).subscribe((result) => {
      if (result) {
        this.isAttention = true;
      } else {
        this.isAttention = false;
      }
    })
  }

  //关注当前浏览客户
  createAttentionRecord(): void {
    this.userDetailService.createAttentionRecord(this.user.id, this.currentUser.userDetailId).subscribe((result) => {
      if (result) {
        this.notify.success("关注成功")
        this.isAttention = true;
      }
    })
  }

  //关注当前浏览客户
  deleteAttentionRecord(): void {
    this.userDetailService.deleteAttentionRecord(this.user.id, this.currentUser.userDetailId).subscribe((result) => {
      if (result) {
        this.notify.success("取关成功")
        this.isAttention = false;
      }
    })
  }

  getUserDetail(): void {
    this.userDetailService.getUserDetailById(this.userDetailId).subscribe((result) => {
      if (result.id == this.currentUser.userDetailId) {
        this.isCurrentUser = true;
      }
      this.user = result;
      this.headUrl = this.userDetailService.baseUrl + result.headPortraitUrl;
      this.ArticleAndProjectComponent.isCurrentUser = this.isCurrentUser;
      console.log(result.id+"------"+this.currentUser.userDetailId+"------"+this.isCurrentUser)
    })
  }

  changePassword(): void {
    this.changePasswordModal.show();
  }

  editUserDetail(): void {
    this.userDetailEditModal.showWin(this.userDetailId);
  }

  refreshData(): void {
    this.getCurrentUser();
  }

}
