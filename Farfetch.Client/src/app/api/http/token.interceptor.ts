import { JwtHelper } from 'angular2-jwt';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import 'rxjs/add/operator/do';
import { AuthenticationService } from 'app/api/modules/authentication/authentication.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    jwtHelper: JwtHelper = new JwtHelper();

    constructor(public auth: AuthenticationService, public router: Router) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!req.url.endsWith('UserAuthentication')) {
            req = req.clone(
                {
                    setHeaders: { Authorization: `Bearer ${this.auth.getToken()}`}
                }
            );
        }

        return next.handle(req).do(
            (event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    if (event.body.status !== 200) {
                        this.handleError(event.body.status);
                    }
                    if (req.url.endsWith('UserAuthentication')) {
                        const tokenDecoded = this.jwtHelper.decodeToken(event.body.token);
                        localStorage.setItem('farfetch_token', event.body.token);
                    }
                }
            },
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    this.handleError(err.status);
                }
            }
        );
    }

    handleError(errorCode) {
        switch (errorCode) {
            case 401:
                this.router.navigate(['/unauthorized']);
                break;
            case 404:
                this.router.navigate(['/notfound']);
                break;
            case 500:
                this.router.navigate(['/serverError']);
                break;
       }
    }
}
