import { AppRoutingModule } from './app-routing.module';
import { AuthenticationService } from 'app/api/modules/authentication/authentication.service';
import { TokenInterceptor } from './api/http/token.interceptor';
import { ApiModule } from './api/api.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationComponent } from './authentication/authentication.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { UnauthorizedComponent } from './core/unauthorized/unauthorized.component';
import { NglModule } from 'ng-lightning/ng-lightning';

@NgModule({
  declarations: [
    AppComponent,
    AuthenticationComponent,
    ServerErrorComponent,
    NotFoundComponent,
    UnauthorizedComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ApiModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
