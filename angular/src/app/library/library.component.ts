import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookService } from 'services';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';
import { SelectBookCategory } from 'entities';
import { Router } from '@angular/router';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})


export class LibraryComponent extends PagedListingComponentBase<any> {

  search: any = { keyword: '', categoryId: '', status: 2};
  isTableLoading = false;
  nzOptions: SelectBookCategory[];
  values: string[] | null = null;

  constructor(private bookService: BookService,
    private injector: Injector,
    private router: Router) {
    super(injector);
  }

  ngOnInit() {
    $('div#banner').removeClass('homepage-mid-read');
    $('div#banner').removeClass('homepage-mid-community');
    $('div#banner').removeClass('homepage-mid-personal');
    $('div#banner').removeClass('homepage-mid-learning');
    $('div#banner').removeClass('homepage-mid-library');
    $('div#banner').addClass('homepage-mid-library');
    this.getBookCategories();
    this.refreshData();
  }



  getBookCategories(): void {
    this.bookService.getBookCategoriesSelect().subscribe((result) => {
      this.nzOptions = result;
      // console.log(this.nzOptions);

      // 查出分类名称
      // this.dataList.forEach(element => {
      //   element.categoryName = this.searchCategoryName(element.categoryId);
      // });
    });
  }

  onChanges(values: string[]): void {
    // console.log(values, this.values);
    if (values.length > 0) {
      // this.search.categoryId = va
    }
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
    this.search = { keyword: '', categoryId: '', status: 2};
    this.refresh();
  }

  detail(id: number): void {
    this.router.navigate(['app/library/book-detail/' + id]);
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this.isTableLoading = true;
    if (this.values != null || this.values != undefined) {
      this.search.categoryId = this.values.reverse()[0];
    }
    const params: any = {};
    params.SkipCount = request.skipCount;
    params.MaxResultCount = request.maxResultCount;
    params.Keyword = this.search.keyword;
    params.CategoryId = this.search.categoryId;
    params.Status = this.search.status;
    // console.log(params);

    this.bookService.getBookListPaged(params)
      // .finally(() => {
      //     finishedCallback();
      // })
      .subscribe((result: PagedResultDto) => {
        // console.log(result);
        this.isTableLoading = false;
        this.dataList = result.items;
        this.totalItems = result.totalCount;

        this.dataList.forEach(d => {
          // 简介过长自动截取
          if (d.description != undefined) {
            if (d.description.length > 151) {
              d.description = d.description.substr(0, 150) + '...';
            }
          }

          d.coverUrl = this.bookService.baseUrl + d.coverUrl;
        });
      });
  }
}
