import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetBlacklistForViewDto, BlacklistDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewBlacklistModal',
    templateUrl: './view-blacklist-modal.component.html',
})
export class ViewBlacklistModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetBlacklistForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetBlacklistForViewDto();
        this.item.blacklist = new BlacklistDto();
    }

    show(item: GetBlacklistForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
