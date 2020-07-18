import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookService, ArticleService } from 'services';
import { NzCascaderOption } from 'ng-zorro-antd/cascader';

import { NzFormatEmitEvent } from 'ng-zorro-antd/core';
import { ArticleCategoryDto } from 'entities';
import { Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
@Component({
  selector: 'app-community',
  templateUrl: './community.component.html',
  styleUrls: ['./community.component.css']
})
export class CommunityComponent extends PagedListingComponentBase<any> {

  search: any = { name: '', categoryId: null, releaseStatus: 1 };
  isTableLoading = false;
  selectedValue: number;
  tags: ArticleCategoryDto[];

  baseUrl: any;
  articleStatus: any = [
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
    },
    {
      status: 4,
      lable: '草稿'
    },
  ];

  constructor(private articleService: ArticleService,
    private injector: Injector,
    private router: Router, ) {
    super(injector);
  }

  nzEvent(event: NzFormatEmitEvent): void {
    // console.log(event);

  }
  ngOnInit() {
    // console.log(AppConsts.appBaseUrl)
    this.baseUrl = AppConsts.appBaseUrl + '/app/community/article-detail/';
    // 首先加载类别选项
    this.getAllTags();
    this.refreshData();
  }

  getAllTags(): void {
    this.articleService.getAllTags().subscribe((result) => {
      this.tags = result;
    });
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
    this.search = { name: '', categoryId: '', releaseStatus: 1};
    this.refresh();
  }

  detail(articleId: number) {
    this.router.navigate(['app/community/article-detail/' + articleId]);
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
    params.CategoryId = this.search.categoryId;
    params.ReleaseStatus = this.search.releaseStatus;
    this.articleService.getArticlePaged(params)
      .subscribe((result: PagedResultDto) => {
        this.isTableLoading = false;
        this.dataList = result.items;
        this.totalItems = result.totalCount;
        // console.log(this.dataList);

        // 初始化封面地址
        this.dataList.forEach(element => {
          if (element.coverUrl != null) {
            element.coverUrl = this.articleService.baseUrl + element.coverUrl;
          }
        });
      });
  }
}
