import { Injector, Output } from '@angular/core';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { PortalsServiceProxy, CreateOrEditAppointmentDto, AppointmentsServiceProxy, StatusType } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ReCaptchaV3Service } from 'ngx-captcha';
import { combineLatest } from 'rxjs';
import { finalize } from 'rxjs/operators';



@Component({
    selector: 'app-form-wizard',
    templateUrl: './form-wizard.component.html'
})
export class FormWizardComponent extends AppComponentBase implements OnInit, AfterViewInit {

    @ViewChild('wizard', { static: true }) el: ElementRef;
    //@ViewChild('portalModalMessage', { static: true }) portalModalMessage: PortalModalMessageComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    //file upload
    @ViewChild('uploadProfilePictureInputLabel') uploadProfilePictureInputLabel: ElementRef;

    active = false;
    saving = false;

    uploadUrl: string;
    uploadedFiles: any[] = [];

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

    detailItems: any[] = [{ title: 'user01', name: "user01" }, { title: 'user02', name: 'user02' }];
    //event for edit
    public isEditing: boolean;
    public pendingValue: string;
    public value!: string;
    public valueChangeEvents: EventEmitter<string>;

    //end

    constructor(
        injector: Injector,
        private _router: Router,
        private route: ActivatedRoute,
        private _portalAppService: PortalsServiceProxy,
        private _test: AppointmentsServiceProxy,
        private _reCaptchaV3Service: ReCaptchaV3Service) {
        super(injector);

        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/Appointment/UploadFiles';

        //event for edit
        this.isEditing = false;
        this.pendingValue = "";
        this.valueChangeEvents = new EventEmitter();
    }

    show(appointmentId?: string): void {
        this.getPOVArray();
        this.getTowerArray();
        this.getCompanyArray();
        this.getDepartmentArray();
        this.getLevelArray();

        this.appointment = new CreateOrEditAppointmentDto();
        this.appointment.id = appointmentId;
        this.active = true;
        this.modal.show();

    }


    save(): void {
        this.saving = true;
        this._portalAppService
            .createOrEdit(this.appointment)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.modalSave.emit(null);
            });
    }

    ngOnInit(appointmentId?: string): void {
        //this.show();

       // this.getPOVArray();

         this.getArray();
        // this.getTowerArray();
        // this.getCompanyArray();
        // this.getDepartmentArray();
        // this.getLevelArray();

        //this.appointment = new CreateOrEditAppointmentDto();
      //  this.appointment.id = appointmentId;


    }
    ngAfterViewInit() { }


    ngOnDestroy() { }

    cancel() { }

    //ListPurposeOfVisit
    getPOVArray(): void {
        this._portalAppService.getPurposeOfVisit().pipe().subscribe((result) => {
            this.arrPOV = [];
            this.arrPOV.push(result);
        })

        this._portalAppService.getTitle().pipe().subscribe((result) => {
            this.arrTitle = [];
            this.arrTitle.push(result);
        })
    }

    getArray(): void {
        
        combineLatest
        (
            this._test.getTitle(), this._test.getTower(),this._test.getPurposeOfVisit(),
            this._test.getLevel(),this._test.getCompanyName(),this._test.getDepartmentName()
        )
            .subscribe(([valueTitle,valueTower,valuePOV,valueLevel,valueCompany,valueDepartment]) => 
            {
                this.arrTitle.push(valueTitle),
                this.arrTower.push(valueTower),
                this.arrPOV.push(valuePOV),
                this.arrLevel.push(valueLevel),
                this.arrCompany.push(valueCompany),
                this.arrDepartment.push(valueDepartment);
            })
    }
    //List tower
    getTowerArray(): void {
        this._portalAppService.getTower().subscribe((result) => {
            this.arrTower = [];
            this.arrTower.push(result);
        })
    }
    //List level
    getLevelArray(): void {
        this._portalAppService.getLevel().subscribe((result) => {
            this.arrLevel = [];
            this.arrLevel.push(result);
        })
    }
    //List company name
    getCompanyArray(): void {
        this._portalAppService.getCompanyName()
            .pipe(

        ).subscribe((result) => {
            this.arrCompany.push(result);
        })
    }
    //List Department Name
    getDepartmentArray(): void {
        this._portalAppService.getDepartmentName().subscribe((result) => {
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

    onUpload(event): void {
        for (const file of event.files) {
            this.uploadedFiles.push(file);
        }
    }

    onBeforeSend(event): void { }
}