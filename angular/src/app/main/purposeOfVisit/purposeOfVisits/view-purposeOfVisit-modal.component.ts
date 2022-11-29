import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetPurposeOfVisitForViewDto, PurposeOfVisitDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPurposeOfVisitModal',
    templateUrl: './view-purposeOfVisit-modal.component.html',
})
export class ViewPurposeOfVisitModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPurposeOfVisitForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetPurposeOfVisitForViewDto();
        this.item.purposeOfVisit = new PurposeOfVisitDto();
    }

    show(item: GetPurposeOfVisitForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
