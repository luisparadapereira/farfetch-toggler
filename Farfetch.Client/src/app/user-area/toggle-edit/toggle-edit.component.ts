import { FarfetchModels } from 'app/api/modules/farfetch/ifarfetch.models';
import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from 'app/api/http/http.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FarfetchClasses } from '../../api/modules/farfetch/farfetch.models.';

@Component({
  selector: 'app-toggle-edit',
  templateUrl: './toggle-edit.component.html',
  styleUrls: ['./toggle-edit.component.css']
})
export class ToggleEditComponent implements OnInit {

  private insert = false;
  private URL = '/Toggler';
  private myToggle: FarfetchModels.ToggleDto;
  loading = false;
  private serviceList: Array<FarfetchModels.ServiceDto>
  private serviceMap: Map<string, FarfetchModels.ServiceDto>
  private selectedServicesMap: Map<string, FarfetchModels.ServiceDto>
  allSelected = false;
  nonSelected = false;

  constructor(private http: HttpService, private route: ActivatedRoute,
    private router: Router) {
    this.loading = true;
  }

  ngOnInit() {
    this.route.params.subscribe(params => this.getToggle(params['id']));
    this.getServiceList();
  }

  isSelected(serviceId) {
    if (this.selectedServicesMap === undefined) { return false; }
    return this.selectedServicesMap.has(serviceId);
  }

  toggleService(serviceId: string) {

    if (this.selectedServicesMap === undefined) {
      this.selectedServicesMap = new Map<string, FarfetchModels.ServiceDto>();
      this.addServiceToToggle(serviceId);
      return;
    }
    if (this.selectedServicesMap.has(serviceId)) {
      this.removeServiceFromToggle(serviceId);
      return;
    }
    this.addServiceToToggle(serviceId);
  }

  updateServicesConnectedToToggle() {
    this.selectedServicesMap = new Map<string, FarfetchModels.ServiceDto>(
      this.myToggle.serviceList.map(x => [x.id, x] as [string, FarfetchModels.ServiceDto]));
  }

  addServiceToToggle(serviceId) {
    this.selectedServicesMap.set(serviceId, this.serviceMap.get(serviceId));
    this.nonSelected = false;
    if ( this.selectedServicesMap.size === this.serviceMap.size) {
      this.allSelected = true;
    }
  }

  removeServiceFromToggle(serviceId) {
    this.selectedServicesMap.delete(serviceId);
    this.allSelected = false;
    if ( this.selectedServicesMap.size === 0) {
      this.nonSelected = true;
    }
  }

  addAllServices() {
    this.selectedServicesMap = this.serviceMap;
    this.allSelected = true;
    this.nonSelected = false;
  }

  removeAllServices() {
    this.selectedServicesMap = new Map<string, FarfetchModels.ServiceDto>();
    this.allSelected = false;
    this.nonSelected = true;
  }


  success(data: FarfetchModels.ToggleDto) {
    this.myToggle = data;
    this.updateServicesConnectedToToggle();
    this.loading = false;
  }

  error(error: any) {
    console.error('Error has occurred getting the information: ' + error);
  }

  cancel() {
    this.router.navigate(['/user-area/toggle']);
  }

  private getToggle(id: string) {
    if (id === '0000') {
      this.insert = true;
      this.myToggle = new FarfetchClasses.InsertToggle();
      this.loading = false;

      return;
    }
    this.insert = false;
    this.http.get<FarfetchModels.TogglerMessage<FarfetchModels.ToggleDto>>(this.URL + '/' + id)
      .subscribe(
        data => this.success(data.result),
        error => this.error(error)
      );
  }

  private getServiceList() {
    this.http.get<FarfetchModels.TogglerMessage<Array<FarfetchModels.ServiceDto>>>('/Service')
      .subscribe(
        data => {
          this.serviceList = data.result;
          this.serviceMap = new Map<string, FarfetchModels.ServiceDto>(
            this.serviceList.map(x => [x.id, x] as [string, FarfetchModels.ServiceDto]));
        },
        error => this.error(error)
      );
  }

  private save(toggle: FarfetchModels.ToggleDto) {
    if (this.selectedServicesMap === undefined) { this.myToggle.serviceList = new Array<FarfetchModels.ServiceDto>(); }
    else {
      this.myToggle.serviceList = Array.from(this.selectedServicesMap.values());
    }

    if (this.insert) {
      this.http.post<FarfetchModels.ToggleDto>(this.URL, toggle)
        .subscribe(
          data => this.successfulInput(data),
          error => this.error(error)
        );
    } else {

      this.http.put<FarfetchModels.ToggleDto>(this.URL, toggle)
        .subscribe(
          data => this.successfulInput(data),
          error => this.error(error)
        );
    }
  }

  private successfulInput(toggle: FarfetchModels.ToggleDto) {
    this.myToggle = toggle
    this.router.navigate(['/user-area/toggle']);
  }

  private delete(id: string) {
    this.http.delete<FarfetchModels.ToggleDto>(this.URL + '/' + id)
      .subscribe(
        data => this.successfulInput(data),
        error => this.error(error)
      );
  }

}
