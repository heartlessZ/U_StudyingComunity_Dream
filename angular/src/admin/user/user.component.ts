import { Component, OnInit, Injector } from '@angular/core';
import { UserDetailService } from 'services/index';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent extends PagedListingComponentBase<any> {

  // 分页获取用户详情
  // getUserDetailList(){

  //   let searchParams:any = {};
  //   searchParams.SkipCount = 0;
  //   searchParams.MaxResultCount = 10;
  //   this.userDetailService.getAll(searchParams).subscribe((data)=>{
  //     //console.log(data);
  //   });
  // }

    search: any = {};
    isTableLoading = false;
    switchLoading = false;

  constructor(private userDetailService: UserDetailService,
    private injector: Injector) {
      super(injector);
   }

  ngOnInit() {
    // this.getUserDetailList();
    this.refreshData();
  }

    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    refreshData() {
        this.pageNumber = 1;
        this.refresh();
    }
    /**
     * 重置
     */
    reset() {
        this.pageNumber = 1;
        this.search = { name: '', mobile: '' };
        this.refresh();
    }

    switchChange(e, id: string): boolean {
      this.switchLoading = true;
      this.userDetailService.updateUserStatus(id).subscribe((result) => {
        this.switchLoading = false;
        return result;
      });
      return false;
    }

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
      this.isTableLoading = true;
        const params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.Name = this.search.name;
        params.Mobile = this.search.mobile;
        this.userDetailService.getUserListPaged(params)
            // .finally(() => {
            //     finishedCallback();
            // })
            .subscribe((result: PagedResultDto) => {
                // console.log(result);
                this.isTableLoading = false;
                this.dataList = result.items;
                this.totalItems = result.totalCount;
                // this.dataList.forEach(i=>i.gender==0?)
            });
    }

}
