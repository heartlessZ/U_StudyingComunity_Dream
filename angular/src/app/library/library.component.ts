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

  search: any = { name: '', categoryId: '' };
  isTableLoading: boolean = false;
  nzOptions:SelectBookCategory[];
  values: string[] | null = null;

  constructor(private bookService: BookService,
    private injector: Injector, 
    private router: Router) {
    super(injector);
  }

  ngOnInit() {
    this.getBookCategories();
    this.refreshData();
  }

  

  getBookCategories(): void {
    this.bookService.getBookCategoriesSelect().subscribe((result) => {
      this.nzOptions = result;
      console.log(this.nzOptions);

      //查出分类名称
      // this.dataList.forEach(element => {
      //   element.categoryName = this.searchCategoryName(element.categoryId);
      // });
    })
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

  detail(id:number):void{
    this.router.navigate(['app/book-detail/' + id]);
  }
}
