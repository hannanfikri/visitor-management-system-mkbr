import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { Paginator } from 'primeng/paginator';
import { Router } from '@angular/router';
import { ViewAppointmentModalComponent } from './view-appointment_Yesterday-modal.component';

@Component({
    selector: 'CheckIn',
    templateUrl: './check-in.component.html',
})
export class CheckIn extends AppComponentBase {
    @ViewChild('CheckIn', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @ViewChild('viewAppointmentModalComponent', { static: true }) viewAppointmentModal: ViewAppointmentModalComponent;
    
    active = false;
    saving = false;

    appointment: CreateOrEditAppointmentDto = new CreateOrEditAppointmentDto();
    AppointmentId : any;

    constructor(
        injector: Injector, 
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _router: Router,
    ) {
        super(injector);
    }

    show(appointmentId?: string): void {
//this.viewAppointmentModal.close();

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
    change_Status_Register_To_In() :void
    {
        this.saving = true;
        this.AppointmentId = this.appointment.id;
        this._appointmentsServiceProxy
            .changeStatusToIn(this.appointment)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((result) => {
                this.modalSave.emit(null);
                window.location.reload();
                this.notify.info(this.l('CheckInSuccessfully'));
            });
        
    }    
}
