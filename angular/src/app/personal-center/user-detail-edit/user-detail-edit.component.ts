import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-user-detail-edit-model',
  templateUrl: './user-detail-edit.component.html',
  styleUrls: ['./user-detail-edit.component.css']
})
export class UserDetailEditComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  
  constructor(injector: Injector,) {
    super(injector);
  }

  ngOnInit() {
  }

}
