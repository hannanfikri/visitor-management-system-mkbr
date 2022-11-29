import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CompaniesServiceProxy, CreateOrEditCompanyDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditCompanyModal',
    templateUrl: './create-or-edit-company-modal.component.html',
})
export class CreateOrEditCompanyModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    company: CreateOrEditCompanyDto = new CreateOrEditCompanyDto();

    constructor(
        injector: Injector,
        private _companiesServiceProxy: CompaniesServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(companyId?: string): void {
        if (!companyId) {
            this.company = new CreateOrEditCompanyDto();
            this.company.id = companyId;

            this.active = true;
            this.modal.show();
        } else {
            this._companiesServiceProxy.getCompanyForEdit(companyId).subscribe((result) => {
                this.company = result.company;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._companiesServiceProxy
            .createOrEdit(this.company)
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
