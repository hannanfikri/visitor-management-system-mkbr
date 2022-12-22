import { Injector, Output } from '@angular/core';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { PortalsServiceProxy, CreateOrEditAppointmentDto, AppointmentsServiceProxy, StatusType , GetAppointmentForViewDto } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ReCaptchaV3Service } from 'ngx-captcha';
import { combineLatest } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { ViewDetailsComponent } from '../view-details/view-details.component';




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
    @ViewChild('viewAppointmentModalComponent', { static: true }) viewAppointmentModal: ViewDetailsComponent;

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
        private _reCaptchaV3Service: ReCaptchaV3Service, private router: Router) {
        super(injector);

        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/Appointment/UploadFiles';

        //event for edit
        this.isEditing = false;
        this.pendingValue = "";
        this.valueChangeEvents = new EventEmitter();
    }

    show(appointmentId?: string): void {

        this.appointment = new CreateOrEditAppointmentDto();
        this.appointment.id = appointmentId;
        

    }
    test(appointmentId?: string):void{
        this.appointment.id = appointmentId;
        this.viewDetails();
    }


    save(appointmentId: string): void {
        this.test();
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
                appointmentId= this.appointment.id;
                
            });
    }

    fetchAppointmentId(): void {
        this.appointment.id;
    }

    ngOnInit(appointmentId?: string): void {

         this.getArray();
         this.active = true;


    }
    ngAfterViewInit() { }


    ngOnDestroy() { }

    cancel() { }
    

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

    onUpload(event): void {
        for (const file of event.files) {
            this.uploadedFiles.push(file);
        }
    }

    onBeforeSend(event): void { }

    viewDetails() {
        this._router.navigate(['/view-details']);
      }
}