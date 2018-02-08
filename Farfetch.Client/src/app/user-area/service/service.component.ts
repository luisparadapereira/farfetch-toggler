import { FarfetchModels } from 'app/api/modules/farfetch/ifarfetch.models';
import { Component, OnInit } from '@angular/core';
import { HttpService } from 'app/api/http/http.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.css']
})
export class ServiceComponent implements OnInit {

  private URL = '/Service';
  public serviceList: Array<FarfetchModels.ServiceDto>;
  private loading = false;

  constructor(private http: HttpService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.http.get<FarfetchModels.TogglerMessage<Array<FarfetchModels.ServiceDto>>>(this.URL)
    .subscribe(
      data => this.success(data.result),
      error => this.error(error)
    );
   }

   success(data: Array<FarfetchModels.ServiceDto>) {
    this.serviceList = data;
  }

  error(error: any) {
    console.error('Error has occurred getting the information: ' + error);
  }

  goToService(id: string) {
    this.router.navigate(['/user-area/service-register/' + id]);
   }


   insertNew() {
    this.router.navigate(['/user-area/service-register/0000']);
   }

}
