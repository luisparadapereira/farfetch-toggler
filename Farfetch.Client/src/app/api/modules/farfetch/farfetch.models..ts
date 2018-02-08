import { FarfetchModels } from './ifarfetch.models';

export module FarfetchClasses {
  export class CustomService implements FarfetchModels.ServiceDto {
    id: string;
    name: string;
    version: string;
    apiKey: string;

    constructor() {
      this.id = '00000000-0000-0000-0000-000000000000';
      this.apiKey = '';
      this.name = '';
      this.version = ''
    }
  }

  export class InsertToggle implements FarfetchModels.ToggleDto {
    id: string;
    name: string;
    value: boolean;
    overrides: boolean;
    serviceList: FarfetchModels.ServiceDto[];

    constructor() {
      this.id = '00000000-0000-0000-0000-000000000000';
      this.serviceList = new Array<CustomService>();
      this.value = false;
      this.overrides = false;
      this.name = ''
    }
  }
}
