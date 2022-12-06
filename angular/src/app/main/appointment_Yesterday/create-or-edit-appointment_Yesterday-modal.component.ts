import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto, DepartmentDto, GetDepartmentForViewDto, StatusType } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { DatePipe } from '@angular/common'

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { result } from 'lodash-es';
import { key } from 'localforage';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'createOrEditAppointmentModal',
    templateUrl: './create-or-edit-appointment_Yesterday-modal.component.html',
})
export class CreateOrEditAppointmentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();


    active = false;
    saving = false;
    //statusenum : Array<any> = []

    uploadUrl: string;
    uploadedFiles: any[] = [];

    keys = Object.keys(StatusType);
    statusType: Array<string> = [];
    statusenum: typeof StatusType = StatusType;
    // statusenum = StatusType;
    appointment: CreateOrEditAppointmentDto = new CreateOrEditAppointmentDto();
    arrPOV: Array<any> = [];
    arrTitle: Array<any> = [];
    arrTower: Array<any> = [];
    arrLevel: Array<any> = [];
    arrCompany: Array<any> = [];
    arrDepartment: Array<any> = [];
    fv: string = "0x0A";
    myDefaultValue: number = 1;


    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _dateTimeService: DateTimeService,
    ) {
        super(injector);
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/Appointment/UploadFiles';
    }

    

    show(appointmentId?: string): void {
     this.modal.show();
        if (!appointmentId) {
            this.GetEmptyArray();
            this.getPOVArray();

            this.getTitleArray();
            this.getTowerArray();
            this.getCompanyArray();
            this.getDepartmentArray();
            this.getLevelArray();
            this.getStatusEnum();

            this.appointment = new CreateOrEditAppointmentDto();
            this.appointment.id = appointmentId;
            this.active = true;
            this.modal.show();
        } else {
            this._appointmentsServiceProxy.getAppointmentForEdit(appointmentId).subscribe((result) => {
                this.appointment = result.appointment;
                this.getPOVArray();
                this.getLevelArray();
                this.getTitleArray();
                this.getTowerArray();
                this.getCompanyArray();
                this.getDepartmentArray();
                this.getStatusEnum();
                this.GetEmptyArray();

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;
        this._appointmentsServiceProxy
            .createOrEdit(this.appointment)
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
        this.uploadedFiles = [];
        this.active = false;
        this.modal.hide();
    }
    setDate(): void {
        let date: Date = new Date();
        console.log("Date = " + date);

    }

    ngOnInit(): void { }

    //ListPurposeOfVisit
    getPOVArray(): void {
        this._appointmentsServiceProxy.getPurposeOfVisit().subscribe((result) => {
            this.arrPOV = [];
            this.arrPOV.push(result);
        })
    }
    //List title
    getTitleArray(): void {
        this._appointmentsServiceProxy.getTitle().subscribe((result) => {
            this.arrTitle = [];
            this.arrTitle.push(result);
        })
    }
    //List tower
    getTowerArray(): void {
        this._appointmentsServiceProxy.getTower().subscribe((result) => {
            this.arrTower = [];
            this.arrTower.push(result);
        })
    }
    //List level
    getLevelArray(): void {
        this._appointmentsServiceProxy.getLevel().subscribe((result) => {
            this.arrLevel = [];
            this.arrLevel.push(result);
        })
    }
    //List company name
    getCompanyArray(): void {
        this._appointmentsServiceProxy.getCompanyName().subscribe((result) => {
            this.arrCompany = [];
            this.arrCompany.push(result);
        })
    }
    //List Department Name
    getDepartmentArray(): void {
        this._appointmentsServiceProxy.getDepartmentName().subscribe((result) => {
            this.arrDepartment = [];
            this.arrDepartment.push(result)
        })
    }
    GetEmptyArray(): void {
        this.arrDepartment = [];
        this.arrCompany = [];
        this.arrLevel = [];
        this.arrTower = [];
        this.arrTitle = [];
        this.arrPOV = [];
    }
    getStatusEnum(): void {
        this.statusType = [];
        for (let s in StatusType) {
            if (isNaN(Number(s))) {
                this.statusType.push(s);
            }
        };
    }

    onUpload(event): void {
        for (const file of event.files) {
            this.uploadedFiles.push(file);
        }
    }

    onBeforeSend(event): void {
        event.xhr.setRequestHeader('Authorization', 'Bearer' + abp.auth.getToken());
    }
}