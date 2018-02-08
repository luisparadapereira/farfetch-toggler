import { FarfetchClasses } from '../../api/modules/farfetch/farfetch.models.';
import { Component, OnInit } from '@angular/core';
import { HttpService } from 'app/api/http/http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FarfetchModels } from '../../api/modules/farfetch/ifarfetch.models';

@Component({
  selector: 'app-service-register',
  templateUrl: './service-register.component.html',
  styleUrls: ['./service-register.component.css']
})
export class ServiceRegisterComponent implements OnInit {

  private URL = '/Service';
  private RegisterURL = '/ServiceAuthorization'
  public serviceData: FarfetchModels.ServiceDto;
  private loading = false;
  private insert = false;

  constructor(private http: HttpService, private route: ActivatedRoute,
    private router: Router) {
    this.loading = true;
  }

  ngOnInit() {
    this.route.params.subscribe(params => this.getService(params['id']));
  }

  success(data: FarfetchModels.ServiceDto) {
    this.serviceData = data;
    this.loading = false;
  }

  error(error: any) {
    console.error('Error has occurred getting the information: ' + error);
  }

  cancel() {
    this.router.navigate(['/user-area/service']);
  }


  private getService(id: string) {
    if (id === '0000') {
      this.insert = true;
      this.serviceData = new FarfetchClasses.CustomService();
      this.loading = false;

      return;
    }
    this.insert = false;
    this.http.get<FarfetchModels.TogglerMessage<FarfetchModels.ServiceDto>>(this.URL + '/' + id)
      .subscribe(
        data => this.success(data.result),
        error => this.error(error)
      );
  }

  private successfulToken(data) {
    this.serviceData.apiKey = data['token'];
  }

  private generateToken(service: FarfetchModels.ServiceDto) {
    this.http.post<FarfetchModels.ServiceDto>(this.RegisterURL, service)
      .subscribe(
        data => this.successfulToken(data),
        error => this.error(error)
      );
  }

  private save(service: FarfetchModels.ServiceDto) {
    if (this.insert) {
      this.http.post<FarfetchModels.ServiceDto>(this.URL, service)
        .subscribe(
          data => this.successfulInput(data),
          error => this.error(error)
        );
    } else {

      this.http.put<FarfetchModels.ServiceDto>(this.URL, service)
        .subscribe(
          data => this.successfulInput(data),
          error => this.error(error)
        );
    }
  }

  private successfulInput(service: FarfetchClasses.CustomService) {
    this.serviceData = service
    this.router.navigate(['/user-area/service']);
  }

  private delete(id: string) {
    this.http.delete<FarfetchClasses.CustomService>(this.URL + '/' + id)
      .subscribe(
        data => this.successfulInput(data),
        error => this.error(error)
      );
  }


  private copyToClip() {
    copyToClipboard(this.serviceData.apiKey);
  }



}

export const copyToClipboard = (url: string) => {
  document.addEventListener('copy', (e: ClipboardEvent) => {
    e.clipboardData.setData('text/plain', url);
    e.preventDefault();
  });
  document.execCommand('copy');
};
