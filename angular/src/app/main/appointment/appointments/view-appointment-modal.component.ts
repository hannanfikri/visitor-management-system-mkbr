import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetAppointmentForViewDto, AppointmentDto, StatusType, AppointmentsServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'viewAppointmentModal',
    templateUrl: './view-appointment-modal.component.html',
})
export class ViewAppointmentModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    imageUrl: any;
    imageBlob: any;
    imageReader = new FileReader();
    image: any;

    item: GetAppointmentForViewDto;

    constructor(injector: Injector, private _appointmentsServiceProxy: AppointmentsServiceProxy, private _sanitizer: DomSanitizer) {
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
            //this.image = this.imageReader.readAsDataURL(this.imageBlob);
            this.image = 'data:image/jpg;base64,' + this.imageBlob;
        });
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
}
