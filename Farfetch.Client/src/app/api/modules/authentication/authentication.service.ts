import { tokenNotExpired } from 'angular2-jwt';
import { HttpRequest } from '@angular/common/http';

import { Injectable } from '@angular/core';

@Injectable()
export class AuthenticationService {

    cachedRequests: Array<HttpRequest<any>> = [];

    public isAuthenticated(): boolean {
        const token = this.getToken();
        if(token === undefined) return false;
        return tokenNotExpired(null, token);
    }

    public getToken(): string {
        return localStorage.getItem('farfetch_token');
    }

    public collectFailedRequests(request): void {
        this.cachedRequests.push(request);
    }

    public retryFailedRequests(): void {}
}
