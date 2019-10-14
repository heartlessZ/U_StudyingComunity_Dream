import { NgModule, ModuleWithProviders } from '@angular/core';
//import { HTTP_INTERCEPTORS } from '@angular/common/http';
//import { AbpHttpInterceptor } from 'abp-ng2-module/dist/src/abpHttpInterceptor';
import { CommonHttpClient } from './common-httpclient';
import { UserDetailService } from 'services';

@NgModule({
    providers: [
        CommonHttpClient,
        //{ provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
    ],
    imports:[
        
    ]
})
export class ServicesModule { 
    static forRoot(): ModuleWithProviders {
        return {
          ngModule: ServicesModule,
          providers: [
            UserDetailService,
          ],
        };
      }
    
}
