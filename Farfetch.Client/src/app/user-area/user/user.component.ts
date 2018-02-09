import { FarfetchModels } from 'app/api/modules/farfetch/ifarfetch.models';
import { Component, OnInit } from '@angular/core';
import { HttpService } from 'app/api/http/http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FarfetchClasses } from '../../api/modules/farfetch/farfetch.models.';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  private URL = '/UserAccounts';
  public userList: Array<FarfetchModels.UserLoginDto>;
  private loading = false;
  private userProfiles = ['Admin', 'Developer', 'Public'];
  toUpdate = false;
  private changedUsers: Array<FarfetchModels.UserLoginDto>;

  constructor(private http: HttpService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.getUsers();
   }

   success(data: Array<FarfetchModels.UserLoginDto>) {
    this.userList = data;
  }

  error(error: any) {
    console.error('Error has occurred getting the information: ' + error);
  }

  changed(user: FarfetchModels.UserLoginDto) {
    if(this.changedUsers===undefined) { this.changedUsers = new Array<FarfetchClasses.User>(); }
    this.changedUsers.push(user);
    this.toUpdate = true;
  }

  update() {
    this.changedUsers.forEach(element => {
      this.http.put<FarfetchModels.UserLoginDto>(this.URL, element)
      .subscribe(
        data => {},
        error => this.error(error)
      );
    });
  }

getUsers() {
  this.http.get<FarfetchModels.TogglerMessage<Array<FarfetchModels.UserLoginDto>>>(this.URL)
    .subscribe(
      data => this.success(data.result),
      error => this.error(error)
    );
}

   delete(username: string) {
     const url = this.URL + '/' + username;
    this.http.delete<FarfetchModels.TogglerMessage<FarfetchModels.UserLoginDto>>(url)
    .subscribe(
      data => { this.getUsers() },
      error => this.error(error)
    );
   }

}
