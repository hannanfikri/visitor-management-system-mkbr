import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetAppointmentForViewDto, AppointmentDto, StatusType } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAppointmentModal',
    templateUrl: './view-appointment_Yesterday-modal.component.html',
})
export class ViewAppointmentModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAppointmentForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetAppointmentForViewDto();
        this.item.appointment = new AppointmentDto();
    }

    show(item: GetAppointmentForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
