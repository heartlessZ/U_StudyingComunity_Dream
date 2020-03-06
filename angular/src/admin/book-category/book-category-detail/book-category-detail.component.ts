import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { BookService } from 'services';
import { UserDetailDto, BookCategoryDto } from 'entities';

@Component({
  selector: 'app-book-category-detail-modal',
  templateUrl: './book-category-detail.component.html',
  styleUrls: ['./book-category-detail.component.css']
})
export class BookCategoryDetailComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  validateForm: FormGroup;
  emodalVisible = false; // 模态框是否显示
  isOkLoading = false;
  loading = false;
  category: BookCategoryDto;

  constructor(injector: Injector,
    private fb: FormBuilder,
    private bookService: BookService) {
    super(injector);

    this.validateForm = this.fb.group({
      name: ['', [Validators.required]]
    });
  }
  /**
   * 显示模态框（进入用户详情页）
   */
  show(currentNode: BookCategoryDto) {
    this.emodalVisible = true;
    this.category = currentNode;
  }

  handleOk(): void {
    this.isOkLoading = true;
    this.submitForm(name);
    if (!this.validateForm.valid) {
      return;
    }
    this.bookService.createOrUpdateCategory(this.category.title, this.category.parent, this.category.key).subscribe((result) => {
      if (result) {
        this.message.success('修改成功');
        this.emodalVisible = false;
        this.isOkLoading = false;
        this.modalSave.emit(null);
      }
    });
  }

  handleCancel(): void {
    this.emodalVisible = false;
    this.validateForm.reset();
  }

  ngOnInit() {

  }

  submitForm(value: any): void {
    for (const key in this.validateForm.controls) {
      this.validateForm.controls[key].markAsDirty();
      this.validateForm.controls[key].updateValueAndValidity();
    }
  }
}
