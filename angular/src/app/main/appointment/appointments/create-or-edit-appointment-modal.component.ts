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

@Component({
    selector: 'createOrEditAppointmentModal',
    templateUrl: './create-or-edit-appointment-modal.component.html',
})
export class CreateOrEditAppointmentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    //statusenum : Array<any> = []
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

    sampleDateTime: DateTime;
    dateFormat = 'dd-LL-yyyy HH:mm:ss';
    r: any;


    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _dateTimeService: DateTimeService,
        //public datepipe: DatePipe
    ) {
        super(injector);
    }

    getStatusEnum(): void {
        this.statusType = [];
        for (let s in StatusType) {
            if (isNaN(Number(s))) {
                this.statusType.push(s);
            }
        };
        // this.statusType = [];
        // for(let s in this.keys){
        //     this.statusType.push(s);
        // }
    }

    // Subscribe from an observable and push into array
    getDepartmentNameArray(): void {
        this._appointmentsServiceProxy.getDepartmentName().subscribe((result) => {
            this.arrDepartment = [];
            this.arrDepartment.push(result);
        })
    }

    show(appointmentId?: string): void {
        this.getDepartmentNameArray();
        this.getStatusEnum();
        if (!appointmentId) {
            this.GetEmptyArray();
            this.getPOVArray();

            this.getTitleArray();
            this.getTowerArray();
            this.getCompanyArray();
            this.getDepartmentArray();

            this.getLevelArray();

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

    //test
    submitDateTime(): void {
        this._appointmentsServiceProxy.getDateTime(this.sampleDateTime).subscribe((data) => {
            var dateString = this.getDateString(data.date);
            this.message.info(dateString, this.l('PostedValue'));
        });
    }
    getDateString(date: DateTime): string {
        var dateString = this._dateTimeService.formatDate(date, this.dateFormat);
        if (abp.clock.provider.supportsMultipleTimezone) {
            dateString += '(' + abp.timing.timeZoneInfo.iana.timeZoneId + ')';
        }

        return dateString;
    }
}