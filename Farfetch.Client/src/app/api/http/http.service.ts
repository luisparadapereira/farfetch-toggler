import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { LocationStrategy } from '@angular/common';

Injectable()
export class HttpService {
    // tslint:disable-next-line:member-ordering
    private SERVER = 'http://localhost:57146';
    // private SERVER = 'http://localhost:5000';

    private AUTHENTICATION = this.SERVER + '/UserAuthentication';

    constructor(public http: HttpClient) {    }

    get<T> (url: string): Observable<T> {
        return this.http.get<T>(this.SERVER + url);
    }

    post<T> (url: string, data: T): Observable<T> {
        return this.http.post<T>(this.SERVER + url, data);
    }

    put<T> (url: string, data: T): Observable<T> {
        return this.http.put<T>(this.SERVER + url, data);
    }

    delete<T> (url: string): Observable<T> {
        return this.http.delete<T>(this.SERVER + url);
    }

    login (username: string, password: string): Observable<Object> {
        const body = {
            'Username': username,
            'Password': password
        };

        return this.http.post(
            this.AUTHENTICATION,
            body
        )
    }
}




