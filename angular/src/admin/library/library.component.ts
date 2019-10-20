import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookService } from 'services';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';

import { NzFormatEmitEvent } from 'ng-zorro-antd/core';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { BookCategoryDto, SelectBookCategory } from 'entities';

const options = [
  {
    value: 'zhejiang',
    label: 'Zhejiang',
    children: [
      {
        value: 'hangzhou',
        label: 'Hangzhou',
        children: [
          {
            value: 'xihu',
            label: 'West Lake',
            isLeaf: true
          }
        ]
      },
      {
        value: 'ningbo',
        label: 'Ningbo',
        isLeaf: true
      }
    ]
  },
  {
    value: 'jiangsu',
    label: 'Jiangsu',
    children: [
      {
        value: 'nanjing',
        label: 'Nanjing',
        children: [
          {
            value: 'zhonghuamen',
            label: 'Zhong Hua Men',
            isLeaf: true
          }
        ]
      }
    ]
  }
];

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})


export class LibraryComponent extends PagedListingComponentBase<any> {
  @ViewChild('bookDetailModal', { static: true }) bookDetailModal: BookDetailComponent;

  search: any = { name: '', categoryId: '' };
  isTableLoading: boolean = false;
  nzOptions: NzCascaderOption[] = options;
  //nzOptions:SelectBookCategory[];
  values: string[] | null = null;

  constructor(private bookService: BookService,
    private injector: Injector) {
    super(injector);
  }
  
  nzEvent(event: NzFormatEmitEvent): void {
    console.log(event);
    
  }
  ngOnInit() {
    this.getBookCategories();
    this.refreshData();
  }

  getBookCategories():void{
    this.bookService.getBookCategoriesSelect().subscribe((result)=>{
      this.nzOptions = result;
      console.log(this.nzOptions);
    })
  }

  //分类选择器改变事件
  onChanges(values: string[]): void {
    console.log(values, this.values);
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
    this.search = { name: '', categoryId: '' };
    this.refresh();
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this.isTableLoading = true;
    if (this.values!=null || this.values!=undefined)
      this.search.categoryId = this.values.reverse()[0];
    let params: any = {};
    params.SkipCount = request.skipCount;
    params.MaxResultCount = request.maxResultCount;
    params.Name = this.search.name;
    params.CategoryId = this.search.categoryId;
    this.bookService.getBookListPaged(params)
      // .finally(() => {
      //     finishedCallback();
      // })
      .subscribe((result: PagedResultDto) => {
        console.log(result);
        this.isTableLoading = false;
        this.dataList = result.items
        this.totalItems = result.totalCount;
      });
  }

  createBook():void{
    this.bookDetailModal.show();
  }

  edit(id:number):void{
    this.bookDetailModal.show(id);
  }
}