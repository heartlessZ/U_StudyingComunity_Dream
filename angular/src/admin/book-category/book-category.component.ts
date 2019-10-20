import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { UserDetailService, BookService } from 'services/index';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/component-base';
import { BookCategoryDetailComponent } from './book-category-detail/book-category-detail.component';
import { NzFormatEmitEvent, NzTreeNode } from 'ng-zorro-antd/core';
import { BookCategoryDto } from 'entities/book-category';
import { CreateCategoryComponent } from './create-category/create-category.component';

@Component({
  selector: 'app-book-category',
  templateUrl: './book-category.component.html',
  styleUrls: ['./book-category.component.css']
})
export class BookCategoryComponent extends AppComponentBase implements OnInit {
  @ViewChild('createCategoryModal', { static: true }) createCategoryModal: CreateCategoryComponent;
  @ViewChild('bookCategoryDetailModal', { static: true }) bookCategoryDetailModal: BookCategoryDetailComponent;

  // nodes = [
  //   {
  //     title: 'parent 1',
  //     key: '100',
  //     expanded: true,
  //     children: [
  //       {
  //         title: 'parent 1-0',
  //         key: '1001',
  //         expanded: true,
  //         children: [
  //           { title: 'leaf', key: '10010', isLeaf: true },
  //           { title: 'leaf', key: '10011', isLeaf: true },
  //           { title: 'leaf', key: '10012', isLeaf: true }
  //         ]
  //       },
  //       {
  //         title: 'parent 1-1',
  //         key: '1002',
  //         children: [{ title: 'leaf', key: '10020', isLeaf: true }]
  //       },
  //       {
  //         title: 'parent 1-2',
  //         key: '1003',
  //         children: [{ title: 'leaf', key: '10030', isLeaf: true }, { title: 'leaf', key: '10031', isLeaf: true }]
  //       }
  //     ]
  //   }
  // ];
  currentNode: BookCategoryDto;//当前选中节点
  nodes: NzTreeNode;
  bookCategoryNodes: BookCategoryDto[];

  constructor(private bookService: BookService,
    private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.refreshData();
  }

  nzEvent(event: NzFormatEmitEvent): void {
    console.log(event);
    if (event.keys.length <= 0) {
      this.currentNode = undefined;
      return;
    }
    this.currentNode = new BookCategoryDto();
    this.currentNode.key = Number(event.keys[0]);
    this.currentNode.title = event.node.title;
    this.currentNode.parent = event.node.origin.parent;
  }

  editNode(event: NzFormatEmitEvent): void {
    console.log(event);
  }

  search: any = {};
  isTableLoading: boolean = false;

  refreshData() {
    this.getBookCategoryNodes();
  }

  getBookCategoryNodes(): void {
    this.bookService.getBookCategoryList().subscribe((result) => {
      console.log(result);
      this.bookCategoryNodes = result;
    });
  }

  createRootNode():void{
    this.createCategoryModal.show();
  }

  createChild(): void {
    if (this.currentNode == undefined) {
      this.message.error("未选中任何节点。");
      return;
    }
    this.createCategoryModal.show(this.currentNode);
  }

  edit(): void {
    if (this.currentNode == undefined) {
      this.message.error("未选中任何节点。");
      return;
    }
    this.bookCategoryDetailModal.show(this.currentNode);
  }


  delete(id: string): void {

  }
}
