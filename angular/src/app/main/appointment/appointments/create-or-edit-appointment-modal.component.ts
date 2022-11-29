import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto, DepartmentDto, GetDepartmentForViewDto, StatusType, UpdateProfilePictureInputs } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { DatePipe } from '@angular/common'

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { result } from 'lodash-es';
import { key } from 'localforage';

import { FileUploader, FileUploaderOptions, FileItem } from 'ng2-file-upload';
import { ImageCroppedEvent, base64ToFile } from 'ngx-image-cropper';
import { IAjaxResponse, TokenService } from 'abp-ng2-module';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'createOrEditAppointmentModal',
    templateUrl: './create-or-edit-appointment-modal.component.html',
})
export class CreateOrEditAppointmentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    //file upload
    @ViewChild('uploadProfilePictureInputLabel') uploadProfilePictureInputLabel: ElementRef;

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

    //file upload
    public uploader: FileUploader;
    public temporaryPictureUrl: string;
    public maxProfilPictureBytesUserFriendlyValue = 5;
    public useGravatarProfilePicture = false;
    imageChangedEvent: any = '';
    userId: number = null;
    private _uploaderOptions: FileUploaderOptions = {};


    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _dateTimeService: DateTimeService,
        //public datepipe: DatePipe
        private _tokenService:TokenService
    ) {
        super(injector);
    }

    

    show(appointmentId?: string): void {
        this.initializeModal();
     this.modal.show();
         this.userId = this.userId;
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
        this.uploader.uploadAll();
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
             this.uploader.clearQueue();
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
        // this.statusType = [];
        // for(let s in this.keys){
        //     this.statusType.push(s);
        // }
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
    //test for image upload
    initializeModal(): void {
        this.active = true;
        this.temporaryPictureUrl = '';
        this.useGravatarProfilePicture = this.setting.getBoolean('App.UserManagement.UseGravatarProfilePicture');
        if (!this.canUseGravatar()) {
            this.useGravatarProfilePicture = false;
        }
        this.initFileUploader();
    }

    // show(userId?: number): void {
    //     this.initializeModal();
    //     this.modal.show();
    //     this.userId = userId;
    // }

    // close(): void {
    //     this.active = false;
    //     this.imageChangedEvent = '';
    //     this.uploader.clearQueue();
    //     this.modal.hide();
    // }

    fileChangeEvent(event: any): void {
        if (event.target.files[0].size > 5242880) {
            //5MB
            this.message.warn(this.l('ProfilePicture_Warn_SizeLimit', this.maxProfilPictureBytesUserFriendlyValue));
            return;
        }

        this.uploadProfilePictureInputLabel.nativeElement.innerText = event.target.files[0].name;

        this.imageChangedEvent = event;
    }

    imageCroppedFile(event: ImageCroppedEvent) {
        this.uploader.clearQueue();
        this.uploader.addToQueue([<File>base64ToFile(event.base64)]);
    }

    initFileUploader(): void {
        this.uploader = new FileUploader({ url: AppConsts.remoteServiceBaseUrl + '/Profile/UploadProfilePicture' });
        this._uploaderOptions.autoUpload = false;
        this._uploaderOptions.authToken = 'Bearer ' + this._tokenService.getToken();
        this._uploaderOptions.removeAfterUpload = true;
        this.uploader.onAfterAddingFile = (file) => {
            file.withCredentials = false;
        };

        this.uploader.onBuildItemForm = (fileItem: FileItem, form: any) => {
            form.append('FileType', fileItem.file.type);
            form.append('FileName', 'ProfilePicture');
            form.append('FileToken', this.guid());
        };

        this.uploader.onSuccessItem = (item, response, status) => {
            const resp = <IAjaxResponse>JSON.parse(response);
            if (resp.success) {
                this.updateProfilePicture(resp.result.fileToken);
            } else {
                this.message.error(resp.error.message);
            }
        };

        this.uploader.setOptions(this._uploaderOptions);
    }

    updateProfilePicture(fileToken: string): void {
        const input = new UpdateProfilePictureInputs();
        input.fileToken = fileToken;
        input.x = 0;
        input.y = 0;
        input.width = 0;
        input.height = 0;

        if (this.userId) {
            input.userId = this.userId;
        }

        this.saving = true;
        this._appointmentsServiceProxy
            .updateProfilePicture(input)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                abp.setting.values['App.UserManagement.UseGravatarProfilePicture'] =
                    this.useGravatarProfilePicture.toString();
                abp.event.trigger('profilePictureChanged');
                this.modalSave.emit(this.userId);
                this.close();
            });
    }

    updateProfilePictureToUseGravatar(): void {
        const input = new UpdateProfilePictureInputs();
        input.useGravatarProfilePicture = this.useGravatarProfilePicture;

        this.saving = true;
        this._appointmentsServiceProxy
            .updateProfilePicture(input)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                abp.setting.values['App.UserManagement.UseGravatarProfilePicture'] =
                    this.useGravatarProfilePicture.toString();
                abp.event.trigger('profilePictureChanged');
                this.close();
            });
    }

    guid(): string {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }

        return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
    }

    // save(): void {
    //     if (this.useGravatarProfilePicture) {
    //         this.updateProfilePictureToUseGravatar();
    //     } else {
    //         this.uploader.uploadAll();
    //     }
    // }

    canUseGravatar(): boolean {
        return this.setting.getBoolean('App.UserManagement.AllowUsingGravatarProfilePicture');
    }
}