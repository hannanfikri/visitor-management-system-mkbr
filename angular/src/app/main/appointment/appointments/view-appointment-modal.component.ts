﻿import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetAppointmentForViewDto, AppointmentDto, AppointmentsServiceProxy, StatusType, CreateOrEditAppointmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DomSanitizer } from '@angular/platform-browser';
import { finalize } from 'rxjs/operators';
import { Router } from '@angular/router';
import { CheckIn } from './check-in.component';

@Component({
    selector: 'viewAppointmentModal',
    templateUrl: './view-appointment-modal.component.html',
})
export class ViewAppointmentModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('checkIn', { static: true }) checkIn: CheckIn;

    active = false;
    saving = false;

    imageUrl: any;
    imageBlob: any;
    imageReader = new FileReader();
    image: any;

    item: GetAppointmentForViewDto;
    appointment: CreateOrEditAppointmentDto = new CreateOrEditAppointmentDto();
    AppointmentId: any;
    paginator: any;

    constructor(injector: Injector, private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _router: Router, private _sanitizer: DomSanitizer) {
        super(injector);
        this.item = new GetAppointmentForViewDto();
        this.item.appointment = new AppointmentDto();
    }

    displayImage(imageId: string): void {
        this._appointmentsServiceProxy.getFilePictureByIdOrNull(imageId)
            .pipe(

        )
            .subscribe((result) => {
                this.imageBlob = result;
                this.image = 'data:image/jpg;base64,' + this.imageBlob;
            });
    }

    show(item: GetAppointmentForViewDto): void {
        this.item = item;
        this._appointmentsServiceProxy.getAppointmentForEdit(item.appointment.id).subscribe((result) => {
            this.appointment = result.appointment;
        this.appointment.id = item.appointment.id;});
        this.active = true;
        this.displayImage(this.item.appointment.imageId);
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    setAllowCheckIn(): boolean {
        if (this.item.appointment.status == StatusType.Registered)
            return true;
        else
            return false;
    }
    setAllowCheckOut(): boolean {
        if (this.item.appointment.status == StatusType.In)
            return true;
        else
            return false;
    }

    change_Status_Register_To_Out(): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this.saving = true;
                this.AppointmentId = this.appointment.id;
                this.save();
                this._appointmentsServiceProxy.changeStatusToOut(this.AppointmentId)
                this._appointmentsServiceProxy
                    .changeStatusToOut(this.AppointmentId)
                    .pipe(
                        finalize(() => {
                            this.saving = false;
                        })
                    )
                    .subscribe((result) => {
                        this.notify.info(this.l('CheckOutSuccessfully'));
                        this.close();
                        this.modalSave.emit(null);
                        
                    });
                    
            }
        });
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
            .subscribe((result) => {
                this.modalSave.emit(null);
            });
    }

    goToModelCheckIn(appointmentId?:string): void {
        
        //new this.modal1.onHide();
        this.checkIn.modal.show();
        this.checkIn.show(appointmentId);
        
    }
}
