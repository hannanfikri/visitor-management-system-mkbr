import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { result } from 'lodash-es';

@Component({
    selector: 'createOrEditAppointmentModal',
    templateUrl: './create-or-edit-appointment-modal.component.html',
})
export class CreateOrEditAppointmentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    appointment: CreateOrEditAppointmentDto = new CreateOrEditAppointmentDto();
    arrPOV:  Array<any> = [];
    arrTitle:  Array<any> = [];
    arrTower:  Array<any> = [];
    arrLevel:  Array<any> = [];
    

    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(appointmentId?: string): void {
        if (!appointmentId) {
            
            this.getPOVArray();
            this.getLevelArray();
            this.getTitleArray();
            this.getTowerArray();

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
    setDate(): void {
        let date: Date = new Date();  
        console.log("Date = " + date);
            
    }

    ngOnInit(): void {}

    //ListPurposeOfVisit
    getPOVArray():void{
        this._appointmentsServiceProxy.getPurposeOfVisit().subscribe((result) =>
        { this.arrPOV.push(result);})
    }
    //List title
    getTitleArray():void{
        this._appointmentsServiceProxy.getTitle().subscribe((result) =>
        { this.arrTitle.push(result);})
    }
    //List tower
    getTowerArray():void{
        this._appointmentsServiceProxy.getTower().subscribe((result) =>
        { this.arrTower.push(result);})
    }
    //List level
    getLevelArray():void{
        this._appointmentsServiceProxy.getLevel().subscribe((result) =>
        { this.arrLevel.push(result);})
    }
}
