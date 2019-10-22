import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { Router } from '@angular/router';

@Component({
  selector: 'app-article-and-project',
  templateUrl: './article-and-project.component.html',
  styleUrls: ['./article-and-project.component.css']
})
export class ArticleAndProjectComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector,
    private router: Router, ) {
    super(injector);
  }

  ngOnInit() {
  }

  createArticle(): void {
    this.router.navigate(["app/create-article"])
  }

}
