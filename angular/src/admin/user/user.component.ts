import { Component, OnInit, Injector } from '@angular/core';
import { UserDetailService } from 'services/index';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent extends AppComponentBase implements OnInit {

  constructor(private userDetailService : UserDetailService,
    private injector: Injector) {
      super(injector);
   }

  ngOnInit() {
    this.getUserDetailList();
  }

  //分页获取用户详情
  getUserDetailList(){
    
    let searchParams:any = {};
    searchParams.SkipCount = 0;
    searchParams.MaxResultCount = 10;
    this.userDetailService.getAll(searchParams).subscribe((data)=>{
      console.log(data);
    });
  }

}
