import { AuthGuardService } from './../api/modules/authentication/auth-guard.service';
import { UserComponent } from './user/user.component';
import { ServiceComponent } from './service/service.component';
import { ServiceRegisterComponent } from './service-register/service-register.component';
import { ToggleComponent } from './toggle/toggle.component';
import { ToggleEditComponent } from './toggle-edit/toggle-edit.component';

import { FormsModule } from '@angular/forms';

// import { SidebarComponent } from './sidebar/sidebar.component';
import { UserAreaRoutingModule } from './user-area-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserAreaComponent } from './user-area.component';
import { RoleGuardService } from '../api/modules/authentication/role-guard.service';

@NgModule({
  imports: [
    CommonModule,
    UserAreaRoutingModule,
    FormsModule
  ],
  declarations: [
    UserAreaComponent,
    // SidebarComponent,

    ToggleComponent,
    ToggleEditComponent,
    ServiceComponent,
    ServiceRegisterComponent,
    UserComponent
  ],
  providers : [
    AuthGuardService,
    RoleGuardService
  ]
})
export class UserAreaModule { }
