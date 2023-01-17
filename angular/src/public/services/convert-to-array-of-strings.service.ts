import { Injectable } from '@angular/core';
import { DepartmentDto, GetDepartmentForViewDto } from '@shared/service-proxies/service-proxies';
import { values } from 'lodash-es';

@Injectable({
  providedIn: 'root'
})
export class ConvertToArrayOfStringsService {

  array: Array<any> = [];
  arrayString: Array<any> = [];
  getdepartment: GetDepartmentForViewDto;
  department: DepartmentDto;

  constructor() {

  }

  getArrayString(array: Array<any>) {
    for(var i = 0; i < array.length; i++) {
      var temp = this.array.map((res) => res.getdepartment.department);
      this.arrayString.push(temp);
    }
    return this.arrayString;
  }
}
