import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'CheckOut',
    templateUrl: './check-out.component.html',
})
export class CheckOut extends AppComponentBase {
    @ViewChild('CheckOut', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    appointment: CreateOrEditAppointmentDto = new CreateOrEditAppointmentDto();
    AppointmentId : any;

    constructor(
        injector: Injector, 
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
    ) {
        super(injector);
    }

    show(appointmentId?: string): void {
        this._appointmentsServiceProxy.getAppointmentForEdit(appointmentId).subscribe((result) => {
            this.appointment = result.appointment;
        this.active = true;
        

        this.appointment.id = appointmentId;
        this.modal.show();
    });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
    change_Status_Register_To_Out() :void
    {
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
    save(): void {
        this.saving = true;
        //this.uploader.uploadAll();
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
}
