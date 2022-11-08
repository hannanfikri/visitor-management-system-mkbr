import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { TowersServiceProxy, CreateOrEditTowerDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditTowerModal',
    templateUrl: './create-or-edit-tower-modal.component.html',
})
export class CreateOrEditTowerModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    tower: CreateOrEditTowerDto = new CreateOrEditTowerDto();

    constructor(
        injector: Injector,
        private _towersServiceProxy: TowersServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(towerId?: string): void {
        if (!towerId) {
            this.tower = new CreateOrEditTowerDto();
            this.tower.id = towerId;

            this.active = true;
            this.modal.show();
        } else {
            this._towersServiceProxy.gettowerForEdit(towerId).subscribe((result) => {
                this.tower = result.tower;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._towersServiceProxy
            .createOrEdit(this.tower)
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
