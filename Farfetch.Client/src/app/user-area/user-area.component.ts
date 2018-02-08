import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-area',
  templateUrl: './user-area.component.html',
  styleUrls: ['./user-area.component.css']
})
export class UserAreaComponent implements OnInit {

  private activeClass = 'slds-is-active';
  private userClass = '';
  private toggleClass = '';
  private serviceClass = '';

  constructor(private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
      this.toggleClass = this.activeClass;
   }

  private goToManageUsers() {
    this.resetClasses();
    this.router.navigate(['/user-area/user']);
    this.userClass = this.activeClass;
  }

  private goToManageToggle() {
    this.resetClasses();
    this.router.navigate(['/user-area/toggle']);
    this.toggleClass = this.activeClass;
  }

  private goToManageService() {
    this.resetClasses();
    this.router.navigate(['/user-area/service']);
    this.serviceClass = this.activeClass;
  }

  private goToManageLogout() {
    this.resetClasses();
    localStorage.clear();
    this.router.navigate(['/']);
  }

  private resetClasses() {
    this.userClass = '';
    this.toggleClass = '';
    this.serviceClass = '';
  }

  public getClass(className: string) {
    switch (className) {
      case ('user'): return this.userClass;
      case ('toggle'): return this.toggleClass;
      case ('service'): return this.serviceClass;
    }
  }

}

// this.router.navigate([url], { relativeTo: this.route });