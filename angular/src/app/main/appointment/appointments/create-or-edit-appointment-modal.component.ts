import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto, DepartmentDto, GetDepartmentForViewDto, StatusType } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { HtmlHelper } from '@shared/helpers/HtmlHelper';
import { result } from 'lodash-es';
import { key } from 'localforage';

@Component({
    selector: 'createOrEditAppointmentModal',
    templateUrl: './create-or-edit-appointment-modal.component.html',
})
export class CreateOrEditAppointmentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    //statusenum : Array<any> = []
    keys = Object.keys(StatusType);
    statusType: Array<string> = [];
    statusenum: typeof StatusType = StatusType;
    appointment: CreateOrEditAppointmentDto = new CreateOrEditAppointmentDto();

    arrDepartment: Array<any> = []; // Declare an array of type 'any' and initialize with empty array

    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getStatusEnum(): void{
        this.statusType = [];
        for(let s in StatusType){
            if(isNaN(Number(s))){
                this.statusType.push(s);
            }
        }
    }

    // Subscribe from an observable and push into array
    getDepartmentNameArray(): void {
        this._appointmentsServiceProxy.getDepartmentName().subscribe((result) => {
            this.arrDepartment = [];
            this.arrDepartment.push(result);
        })
    }

    show(appointmentId?: string): void {
        this.getDepartmentNameArray();
        this.getStatusEnum();
        if (!appointmentId) {
            this.appointment = new CreateOrEditAppointmentDto();
            this.appointment.id = appointmentId;
            this.active = true;
            this.modal.show();
        } else {
            this._appointmentsServiceProxy.getAppointmentForEdit(appointmentId).subscribe((result) => {
                this.appointment = result.appointment;
                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;
        this._appointmentsServiceProxy
            .createOrEdit(this.appointment)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
