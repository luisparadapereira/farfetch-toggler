import { Component, OnInit } from '@angular/core';
import { HttpService } from 'app/api/http/http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FarfetchModels } from 'app/api/modules/farfetch/ifarfetch.models';

@Component({
  selector: 'app-toggle',
  templateUrl: './toggle.component.html',
  styleUrls: ['./toggle.component.css']
})
export class ToggleComponent implements OnInit {

  private URL = '/Toggler';
  public toggleList: Array<FarfetchModels.ToggleListDto>;
  private loading = false;



  constructor(private http: HttpService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.http.get<FarfetchModels.TogglerMessage<Array<FarfetchModels.ToggleListDto>>>(this.URL)
      .subscribe(
        data => this.success(data.result),
        error => this.error(error)
      );
   }

  success(data: Array<FarfetchModels.ToggleListDto>) {
    this.toggleList = data;
  }

  error(error: any) {
    console.error('Error has occurred getting the information: ' + error);
  }

  showLoading() {
    this.loading = true;
  }

  hideLoading() {
    this.loading = false;
  }

  goToToggle(id: string) {
    this.router.navigate(['/user-area/toggle-edit/' + id]);
   }


   insertNew() {
    this.router.navigate(['/user-area/toggle-edit/0000']);
   }
}
