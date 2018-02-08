import { RequestOptions, Http } from '@angular/http';
import { AuthenticationService } from './modules/authentication/authentication.service';
import { HttpClient } from '@angular/common/http';
import { HttpService } from './http/http.service';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule  } from '@angular/common/http';
import { AuthHttp, AuthConfig } from 'angular2-jwt';

export function HttpServiceFactory(http: HttpClient) {
   return new HttpService(http);
}

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule
  ],
  declarations: [],
  providers: [
    {
      provide: HttpService,
      useFactory: HttpServiceFactory,
      deps: [HttpClient]
    }
  ]
})

export class ApiModule {
  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: ApiModule,
      providers: [
        { provide: AuthHttp, useFactory: AuthenticationService, deps: [Http, RequestOptions]}
      ]
    }
  }
}
