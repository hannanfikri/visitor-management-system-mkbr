import { Injector, Output } from '@angular/core';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetAppointmentForViewDto, AppointmentDto } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ReCaptchaV3Service } from 'ngx-captcha';
import { combineLatest } from 'rxjs';
import { finalize } from 'rxjs/operators';



@Component({
    selector: 'app-view-details',
    templateUrl: './view-details.component.html'
})
export class ViewDetailsComponent extends AppComponentBase implements OnInit, AfterViewInit {
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    uploadUrl: string;
    uploadedFiles: any[] = [];

    item: GetAppointmentForViewDto;
    
    constructor(injector: Injector) {        
        super(injector);
        this.item = new GetAppointmentForViewDto();
        this.item.appointment = new AppointmentDto();
    }

    show(item: GetAppointmentForViewDto): void {
        this.item = item;
        this.active = true;
        //this.modal.show();
    }
    

    ngOnInit(appointmentId?: string): void {
        this.show(this.item);
    }
    ngAfterViewInit() { }


    ngOnDestroy() { }

    cancel() { }

    onBeforeSend(event): void { }
}