import { Routes, RouterModule } from '@angular/router';
import { AuthenticationComponent } from 'app/authentication/authentication.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { UnauthorizedComponent } from './core/unauthorized/unauthorized.component';
import { NgModule } from '@angular/core';

const appRoutes: Routes = [
    { path: '', component: AuthenticationComponent, pathMatch: 'full'},
    { path: 'login', component: AuthenticationComponent},
    { path: 'user-area', loadChildren: './user-area/user-area.module#UserAreaModule', data: {preload: true} },
    { path: 'unauthorized', component: UnauthorizedComponent },
    { path: 'notfound', component: NotFoundComponent },
    { path: 'serverError', component: ServerErrorComponent },
    { path: '**', component: NotFoundComponent}
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ]
  })

  export class AppRoutingModule {}
