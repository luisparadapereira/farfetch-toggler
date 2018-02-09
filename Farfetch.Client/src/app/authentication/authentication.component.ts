import { FarfetchModels } from 'app/api/modules/farfetch/ifarfetch.models';
import { Router } from '@angular/router';
import { HttpService } from 'app/api/http/http.service';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';
import { FarfetchClasses } from '../api/modules/farfetch/farfetch.models.';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})

export class AuthenticationComponent implements OnInit {

  public username: string;
  public userpassword: string;
  private access_token: string;

  constructor(private httpService: HttpService,
              public router: Router) {}

  ngOnInit() { }

  login() {
    this.httpService.login(this.username, this.userpassword).subscribe(
      data => this.success(data),
      err => this.error(err)
    );
  }

  register() {
    const registerUrl = '/UserAccounts';
    const user = new FarfetchClasses.User();
    user.username = this.username;
    user.password = this.userpassword;
    this.httpService.post<FarfetchModels.UserLoginDto>(registerUrl, user).subscribe(
      data => this.login(),
      err => this.error(err)
    );
  }

  success( loginData: any ) {
    this.router.navigate(['/user-area/toggle']);
  }

  error( error: any) {
    console.error('Error during login');
  }
}
