import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { TitlesServiceProxy, CreateOrEditTitleDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditTitleModal',
    templateUrl: './create-or-edit-title-modal.component.html',
})
export class CreateOrEditTitleModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    title: CreateOrEditTitleDto = new CreateOrEditTitleDto();

    constructor(
        injector: Injector,
        private _titlesServiceProxy: TitlesServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(titleId?: string): void {
        if (!titleId) {
            this.title = new CreateOrEditTitleDto();
            this.title.id = titleId;

            this.active = true;
            this.modal.show();
        } else {
            this._titlesServiceProxy.getTitleForEdit(titleId).subscribe((result) => {
                this.title = result.title;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._titlesServiceProxy
            .createOrEdit(this.title)
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
