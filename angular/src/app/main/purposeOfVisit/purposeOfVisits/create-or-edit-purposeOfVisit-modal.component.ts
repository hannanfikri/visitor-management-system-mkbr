import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { PurposeOfVisitsServiceProxy, CreateOrEditPurposeOfVisitDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditPurposeOfVisitModal',
    templateUrl: './create-or-edit-purposeOfVisit-modal.component.html',
})
export class CreateOrEditPurposeOfVisitModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    purposeOfVisit: CreateOrEditPurposeOfVisitDto = new CreateOrEditPurposeOfVisitDto();

    constructor(
        injector: Injector,
        private _purposeOfVisitsServiceProxy: PurposeOfVisitsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(purposeOfVisitId?: string): void {
        if (!purposeOfVisitId) {
            this.purposeOfVisit = new CreateOrEditPurposeOfVisitDto();
            this.purposeOfVisit.id = purposeOfVisitId;

            this.active = true;
            this.modal.show();
        } else {
            this._purposeOfVisitsServiceProxy.getpurposeOfVisitForEdit(purposeOfVisitId).subscribe((result) => {
                this.purposeOfVisit = result.purposeOfVisit;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._purposeOfVisitsServiceProxy
            .createOrEdit(this.purposeOfVisit)
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
