import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookService } from 'services';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';

import { NzFormatEmitEvent } from 'ng-zorro-antd/core';

@Component({
  selector: 'app-book-resource',
  templateUrl: './book-resource.component.html',
  styleUrls: ['./book-resource.component.css']
})
export class BookResourceComponent extends PagedListingComponentBase<any> {

  search: any = { name: '', status: 1};
  isTableLoading = false;

  bookStatus: any = [
    {
      status: 1,
      lable: '待审核'
    },
    {
      status: 2,
      lable: '审核通过'
    },
    {
      status: 3,
      lable: '已拒绝'
    }
  ];

  constructor(private bookService: BookService,
    private injector: Injector) {
    super(injector);
  }

  nzEvent(event: NzFormatEmitEvent): void {
    // console.log(event);

  }
  ngOnInit() {
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
    this.search = { name: '', status: 1 };
    this.refresh();
  }

  audit(entity: any, status: any) {
    const sourceStatus = entity.status;
    entity.status = status;
    this.bookService.createBookResource(entity).subscribe((result) => {
      this.notify.success('审核成功');
    });
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
    params.Status = this.search.status;
    this.bookService.getResourcePaged(params)
      // .finally(() => {
      //     finishedCallback();
      // })
      .subscribe((result: PagedResultDto) => {
        this.isTableLoading = false;
        this.dataList = result.items;
        this.totalItems = result.totalCount;
        // console.log(this.dataList);
      });
  }

}
