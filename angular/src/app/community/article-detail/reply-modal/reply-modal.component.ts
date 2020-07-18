import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import {  CommentCreate } from 'entities';
import {  ArticleService } from 'services';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-reply-modal',
  templateUrl: './reply-modal.component.html',
  styleUrls: ['./reply-modal.component.css']
})
export class ReplyModalComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();


  validateForm: FormGroup;
  comment: CommentCreate;
  emodalVisible = false; // 模态框是否显示
  isOkLoading = false;
  loading = false;
  avatarUrl: string;
  fileUploadUrl: string;
  fileUrl: string; // 图片最终上传所得的地址
  birthday: Date;
  title: string;
  constructor(injector: Injector,
    private fb: FormBuilder,
    private articleService: ArticleService) {
    super(injector);

    this.validateForm = this.fb.group({
      content: ['', [Validators.required]]
    });
  }

  /**
   * 显示模态框（进入用户详情页）
   */
  show(comment: CommentCreate) {
    this.emodalVisible = true;
    this.comment = comment;
    this.title = '回复：' + comment.author;
  }

  handleOk(): void {
    this.isOkLoading = true;
    this.submitForm(this.comment);
    if (!this.validateForm.valid) {
      return;
    }
    this.articleService.createComment(this.comment).subscribe((result) => {
      if (result) {
        this.message.success('成功');
        this.emodalVisible = false;
        this.isOkLoading = false;
        this.modalSave.emit(null);
      }
    });
  }


  submitForm(value: any): void {
    for (const key in this.validateForm.controls) {
      this.validateForm.controls[key].markAsDirty();
      this.validateForm.controls[key].updateValueAndValidity();
    }
  }

  handleCancel(): void {
    this.emodalVisible = false;
    this.validateForm.reset();
  }

  ngOnInit() {

  }

}
