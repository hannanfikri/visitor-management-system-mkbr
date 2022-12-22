import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetAppointmentForViewDto, AppointmentDto, StatusType, AppointmentsServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAppointmentModal',
    templateUrl: './view-appointment_Tomorrow-modal.component.html',
})
export class ViewAppointmentModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAppointmentForViewDto;

    imageUrl: any;
    imageBlob: any;
    imageReader = new FileReader();
    image: any;

    constructor(injector: Injector,private _appointmentsServiceProxy: AppointmentsServiceProxy) {
        super(injector);
        this.item = new GetAppointmentForViewDto();
        this.item.appointment = new AppointmentDto();
    }

    show(item: GetAppointmentForViewDto): void {
        this.item = item;
        this.active = true;        
        this.displayImage(this.item.appointment.imageId);
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
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
}