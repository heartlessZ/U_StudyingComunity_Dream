import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookService } from 'services';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';

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

  search: any = { name: '', categoryId: '' };
  isTableLoading: boolean = false;
  nzOptions: NzCascaderOption[] = options;
  values: string[] | null = null;

  constructor(private bookService: BookService,
    private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
  }

  onChanges(values: string[]): void {
    console.log(values, this.values);
    if(values.length > 0){
      //this.search.categoryId = va
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
    this.search = { name: '', categoryId: '' };
    this.refresh();
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this.isTableLoading = true;
    let params: any = {};
    params.SkipCount = request.skipCount;
    params.MaxResultCount = request.maxResultCount;
    params.Name = this.search.name;
    params.CategoryId = this.search.CategoryId;
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
}
