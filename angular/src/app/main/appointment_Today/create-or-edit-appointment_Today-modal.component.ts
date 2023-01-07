import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto, StatusType } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AppConsts } from '@shared/AppConsts';
import { FileItem, FileUploader, FileUploaderOptions } from 'ng2-file-upload';
import { base64ToFile, ImageCroppedEvent } from 'ngx-image-cropper';
import { IAjaxResponse, TokenService } from 'abp-ng2-module';

@Component({
    selector: 'createOrEditAppointmentModal',
    templateUrl: './create-or-edit-appointment_Today-modal.component.html',
})
export class CreateOrEditAppointmentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('uploadPictureInputLabel') uploadPictureInputLabel: ElementRef;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    tempGuid: any;
    
    public uploader: FileUploader;
    public temporaryPictureUrl: string;
    public maxPictureBytesUserFriendlyValue = 5;
    imageChangedEvent: any = "";
    private _uploaderOptions: FileUploaderOptions = {};
    public uploadedFile: File;
    imageBlob: any;
    image: any;

    uploadUrl: string;
    uploadedFiles: any[] = [];
    

    keys = Object.keys(StatusType);
    statusType: Array<string> = [];
    statusenum: typeof StatusType = StatusType;

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
    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _dateTimeService: DateTimeService,
        private _tokenService: TokenService,
    ) {
        super(injector);
    }

    show(appointmentId?: string): void {
        this.initializeModal();
        this.modal.show();
        if (!appointmentId) {
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
                this.displayImage(this.appointment.imageId);
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
        this.imageChangedEvent = '';
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
    //Get list of status
    getStatusEnum(): void {
        this.statusType = [];
        for (let s in StatusType) {
            if (isNaN(Number(s))) {
                this.statusType.push(s);
            }
        };
    }

    //Upload Picture

    initializeModal(): void {
        this.active = true;
        this.temporaryPictureUrl = '';
        this.initFileUploader();
    }

    fileChangeEvent(event: any): void {
        if (event.target.files[0].size > 5242880) {
            //5MB
            this.message.warn(this.l('ProfilePicture_Warn_SizeLimit', this.maxPictureBytesUserFriendlyValue));
            return;
        }

        this.uploadPictureInputLabel.nativeElement.innerText = event.target.files[0].name;

        this.imageChangedEvent = event;
    }

    imageCroppedFile(event: ImageCroppedEvent) {
        this.uploader.clearQueue();
        this.uploader.addToQueue([<File>base64ToFile(event.base64)]);
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/Appointment/UploadFiles';

        //event for edit
        this.isEditing = false;
        this.pendingValue = "";
        this.valueChangeEvents = new EventEmitter();
    }

    initFileUploader(): void {
        this.uploader = new FileUploader({ url: AppConsts.remoteServiceBaseUrl + '/Appointment/UploadAppointmentPicture' });
        this._uploaderOptions.autoUpload = false;
        this._uploaderOptions.authToken = 'Bearer ' + this._tokenService.getToken();
        this._uploaderOptions.removeAfterUpload = true;
        this.uploader.onAfterAddingFile = (file) => {
            file.withCredentials = false;
        };

        this.uploader.onBuildItemForm = (fileItem: FileItem, form: any) => {
            this.guid();
            form.append('FileType', fileItem.file.type);
            form.append('FileName', 'AppointmentPicture');
            form.append('FileToken', this.tempGuid);
        };

        this.uploader.onSuccessItem = (item, response, status) => {
            const resp = <IAjaxResponse>JSON.parse(response);
            if (resp.success) {
                this.updatePicture(resp.result.fileToken);
            }
            else {
                this.message.error(resp.error.message);
            }
        };
        this.uploader.setOptions(this._uploaderOptions);
    }

    updatePicture(fileToken: string): void {
        //const input = new UpdatePictureInput();
        this.appointment.fileToken = fileToken;
        this.appointment.x = 0;
        this.appointment.y = 0;
        this.appointment.width = 0;
        this.appointment.height = 0;
        this.saving = true;
        // this._appointmentsServiceProxy.updatePictureForAppointment(input)
        //     .pipe(
        //         finalize(() => {
        //             this.saving = false;
        //         })
        //     )
        //     .subscribe((result) => {
        //         this.appointment.imageId = result.toString();
        //     })
    }

    guid(): string {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }

        this.tempGuid = s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
        return this.tempGuid;
    }

    displayImage(imageId: string): void {
        this._appointmentsServiceProxy.getFilePictureByIdOrNull(imageId)
            .subscribe((result) => {
                this.imageBlob = result;
                this.image = 'data:image/jpg;base64,' + this.imageBlob;
            });
    }

    upload(): void {
        this.uploader.uploadAll();
        this.notify.info(this.l('UploadSuccessfully'));
    }

}