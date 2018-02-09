import { AuthenticationService } from 'app/api/modules/authentication/authentication.service';
import { Injectable } from '@angular/core';
import {
    Router,
    CanActivate,
    ActivatedRouteSnapshot
} from '@angular/router';

import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class RoleGuardService implements CanActivate {
    jwtHelper: JwtHelper = new JwtHelper();

    constructor(public auth: AuthenticationService, public router: Router) { }

    canActivate(route: ActivatedRouteSnapshot): boolean {
        // this will be passed from the route config
        // on the data property
        const expectedRole: Array<string> = route.data.expectedRole;
        const token = localStorage.getItem('farfetch_token');
        if (token === undefined) this.router.navigate(['/']);

        // decode the token to get its payload
        let tokenPayload = {};
        try {
            tokenPayload = this.jwtHelper.decodeToken(token);
            const currentRole = tokenPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            if (
                !this.auth.isAuthenticated() || expectedRole.find(x => x === currentRole) === undefined
            ) {
                this.router.navigate(['/unauthorized']);
                return false;
            }
            return true;
        } catch (e) {
            this.router.navigate(['/']);
        }

    }
}