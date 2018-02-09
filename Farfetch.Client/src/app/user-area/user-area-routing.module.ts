import { AuthGuardService } from './../api/modules/authentication/auth-guard.service';
import { RoleGuardService } from './../api/modules/authentication/role-guard.service';
import { UserComponent } from './user/user.component';
import { ServiceComponent } from './service/service.component';
import { ServiceRegisterComponent } from './service-register/service-register.component';
import { NgModule } from '@angular/core';
import { UserAreaComponent } from './user-area.component';
import { Routes, RouterModule } from '@angular/router';
import { ToggleComponent } from './toggle/toggle.component';
import { ToggleEditComponent } from './toggle-edit/toggle-edit.component';

const userAreaRoutes: Routes = [
    { path: '', component: UserAreaComponent, children: [
        { path: '', component: ToggleComponent },
        { path: 'user', component: UserComponent, canActivate: [RoleGuardService], data: { expectedRole: ['Admin'] }  },
        { path: 'toggle', component: ToggleComponent, canActivate: [RoleGuardService] , data: { expectedRole: ['Admin', 'Developer', 'Public'] }  },
        { path: 'toggle-edit/:id', component: ToggleEditComponent, canActivate: [RoleGuardService] , data: { expectedRole: ['Admin', 'Developer'] }  },
        { path: 'service', component: ServiceComponent, canActivate: [RoleGuardService] , data: { expectedRole: ['Admin', 'Developer'] }  },
        { path: 'service-register/:id', component: ServiceRegisterComponent, canActivate: [RoleGuardService] , data: { expectedRole: ['Admin', 'Developer'] }  },
    ]}
];

@NgModule({
    imports: [
        RouterModule.forChild(userAreaRoutes)
    ],
    exports: [ RouterModule ]
})

export class UserAreaRoutingModule { }
