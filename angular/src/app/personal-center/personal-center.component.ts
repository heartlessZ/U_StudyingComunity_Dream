import { Component, OnInit , Injector, Input, ViewChild} from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivatedRoute } from '@angular/router'
import { UserDetailService } from 'services';
import { UserDetailDto } from 'entities';
import { UserDetailEditComponent } from './user-detail-edit/user-detail-edit.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

@Component({
  selector: 'app-personal-center',
  templateUrl: './personal-center.component.html',
  styleUrls: [
    '../../../node_modules/ng-zorro-antd/ng-zorro-antd.less',
    './personal-center.component.css']
})
export class PersonalCenterComponent extends AppComponentBase implements OnInit {
  @ViewChild('userDetailEditModal',{static:true}) userDetailEditModal: UserDetailEditComponent;
  @ViewChild('changePasswordModal',{static:true}) changePasswordModal: ChangePasswordComponent;
  @Input() userDetailId;

  user : UserDetailDto = new UserDetailDto();

  constructor(injector: Injector,
    private actRouter: ActivatedRoute,
    private userDetailService : UserDetailService) {
    super(injector);
    this.userDetailId = this.actRouter.snapshot.params['id'];
  }

  ngOnInit() {
    this.getUserDetail();
  }

  getUserDetail():void{
    console.log(this.userDetailId);
    this.userDetailService.getUserDetailById(this.userDetailId).subscribe((result)=>{
      console.log(result);
      if(result.id != undefined)
      {
        this.user = result;
      }
    })
  }

  changePassword():void{
    this.changePasswordModal.show();
  }

  refreshData():void{

  }

}
