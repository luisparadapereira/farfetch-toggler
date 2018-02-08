import { Router } from '@angular/router';
import { HttpService } from 'app/api/http/http.service';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';

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

  ngOnInit() {
  }

  login() {
    this.httpService.login(this.username, this.userpassword).subscribe(
      data => this.successfulLogin(data),
      err => this.failedLogin(err)
    );
  }

  successfulLogin( loginData: any ) {
    this.router.navigate(['/user-area/toggle']);
  }

  failedLogin( error: any) {
    console.error('Error during login');
  }
}
