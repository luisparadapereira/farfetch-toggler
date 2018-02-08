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
        { path: 'toggle', component: ToggleComponent},
        { path: 'toggle-edit/:id', component: ToggleEditComponent},
        { path: 'service', component: ServiceComponent},
        { path: 'service-register/:id', component: ServiceRegisterComponent},
    ]}
];

@NgModule({
    imports: [
        RouterModule.forChild(userAreaRoutes)
    ],
    exports: [ RouterModule ]
})

export class UserAreaRoutingModule { }
