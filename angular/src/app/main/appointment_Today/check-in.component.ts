import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { Paginator } from 'primeng/paginator';
import { Router } from '@angular/router';
import { ViewAppointmentModalComponent } from './view-appointment_Today-modal.component';

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
        this.save();
        this._appointmentsServiceProxy.changeStatusToIn(this.AppointmentId)
        this._appointmentsServiceProxy
            .changeStatusToIn(this.AppointmentId)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe((result) => {
                this.notify.info(this.l('CheckInSuccessfully'));
                this.close();
                this.modalSave.emit(null);
                this.reloadPage();
                this._router.navigate(['/app/main/appointment/appointments']);
                this.modal.toggle();
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
    reloadPage(): void {
        //this._router.navigate(['/app/main/appointment/appointments']);
        this.paginator.changePage(this.paginator.getPage());
    }
    
}
