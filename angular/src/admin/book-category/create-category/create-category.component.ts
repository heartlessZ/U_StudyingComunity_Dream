import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { BookService } from 'services';
import { BookCategoryDto } from 'entities';

@Component({
  selector: 'app-create-category-model',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  validateForm: FormGroup;
  emodalVisible = false; // 模态框是否显示
  isOkLoading = false;
  loading = false;
  name: string;
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
  show(currentNode?: BookCategoryDto) {
    this.emodalVisible = true;
    this.category = currentNode;
  }

  handleOk(): void {
    this.isOkLoading = true;
    this.submitForm(name);
    if (!this.validateForm.valid) {
      return;
    }
    let parent = 0;
    if (this.category != undefined) {
      parent = this.category.key;
    }
    this.bookService.createOrUpdateCategory(this.name, parent).subscribe((result) => {
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
