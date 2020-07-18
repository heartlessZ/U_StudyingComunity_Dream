import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookService } from 'services';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';

import { NzFormatEmitEvent } from 'ng-zorro-antd/core';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { BookCategoryDto, SelectBookCategory } from 'entities';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})


export class LibraryComponent extends PagedListingComponentBase<any> {
  @ViewChild('bookDetailModal', { static: true }) bookDetailModal: BookDetailComponent;

  search: any = { name: '', categoryId: '', status: 1};
  isTableLoading = false;
  nzOptions: NzCascaderOption[];
  // nzOptions:SelectBookCategory[];
  values: string[] | null = null;

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
    // 首先加载类别选项
    this.getBookCategories();
    this.refreshData();
  }

  getBookCategories(): void {
    this.bookService.getBookCategoriesSelect().subscribe((result) => {
      this.nzOptions = result;
      // console.log(this.nzOptions);

      // 查出分类名称
      this.dataList.forEach(element => {
        element.categoryName = this.searchCategoryName(element.categoryId);
      });
    });
  }

  searchCategoryName(id: string): string {
    // console.log(this.nzOptions);

    const option = this.nzOptions.filter(i => i.value == id);
    // console.log(option);
    if (option.length > 0) {
      return option[0].label;
    }
    return undefined;
  }

  // 分类选择器改变事件
  onChanges(values: string[]): void {
    // console.log(values, this.values);
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
    this.search = { name: '', categoryId: '', status: 1 };
    this.values = null;
    this.refresh();
  }

  createBook(): void {
    this.bookDetailModal.show();
  }

  edit(id: number): void {
    this.bookDetailModal.show(id);
  }

  delete(id: number): void {
    this.bookService.deleteBook(id).subscribe((result) => {
      this.notify.success('删除成功');
      this.refresh();
    });
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
    params.Name = this.search.name;
    params.CategoryId = this.search.categoryId;
    params.Status = this.search.status;
    this.bookService.getBookListPaged(params)
      // .finally(() => {
      //     finishedCallback();
      // })
      .subscribe((result: PagedResultDto) => {
        this.isTableLoading = false;
        this.dataList = result.items;
        this.totalItems = result.totalCount;
        // console.log(this.dataList);

        // 初始化封面地址
        this.dataList.forEach(element => {
          if (element.coverUrl != null) {
            element.coverUrl = this.bookService.baseUrl + element.coverUrl;
          }
        });
      });
  }
}
