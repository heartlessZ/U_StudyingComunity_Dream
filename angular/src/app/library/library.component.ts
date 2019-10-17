import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookService } from 'services';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent extends PagedListingComponentBase<any> {

  constructor(private bookService : BookService,
    private injector: Injector) {
      super(injector);
   }

  ngOnInit() {
  }
  search: any = {};
    isTableLoading:boolean=false;

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

    protected fetchDataList(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function,
    ): void {
      this.isTableLoading=true;
        let params: any = {};
        params.SkipCount = request.skipCount;
        params.MaxResultCount = request.maxResultCount;
        params.Name = this.search.name;
        params.Mobile = this.search.mobile;
        this.bookService.getBookListPaged(params)
            // .finally(() => {
            //     finishedCallback();
            // })
            .subscribe((result: PagedResultDto) => {
                console.log(result);
                this.isTableLoading=false;
                this.dataList = result.items
                this.totalItems = result.totalCount;
            });
    }
}
