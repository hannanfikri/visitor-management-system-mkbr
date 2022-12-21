﻿import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppointmentsServiceProxy, CreateOrEditAppointmentDto, DepartmentDto, GetDepartmentForViewDto, StatusType, UpdatePictureInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';
import { DatePipe } from '@angular/common'

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { result } from 'lodash-es';
import { key } from 'localforage';
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
    imageChangedEvent: any="";
    private _uploaderOptions: FileUploaderOptions = {};
    public uploadedFile: File;
    imageBlob: any;
    image: any;

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
        private _tokenService: TokenService,
    ) {
        super(injector);
        //this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/Appointment/UploadFiles';
    }

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
            //form.append('uploadFile', this.uploadedFile);
            //this.appointment.imageId = this.tempGuid;
        };

        // onSuccessItem run after item is success eg: after fx this.uploader.uploadAll()
        
        // this.uploader.onBeforeUploadItem = (fileItem: FileItem) => {
        //     fileItem._onSuccess = (response, status) => {
        //         const resp = <IAjaxResponse>JSON.parse(response);
        //         //this.appointment.imageId = resp.result.fileToken;
        //         this.updatePicture(resp.result.fileToken);
        //     }
        // }

        // this.uploader.uploadItem = (fileItem: FileItem) => {
        //     this.guid();
        //     this.updatePicture(this.tempGuid);
        // }

        // this.uploader.onCompleteItem = (item, response, status) => {
        //     const resp = <IAjaxResponse>JSON.parse(response);
        //     if (resp.success) {
        //         //this.appointment.imageId = resp.result.id;
        //     }
        // }

        this.uploader.onSuccessItem = (item, response, status) => {
            const resp = <IAjaxResponse>JSON.parse(response);
            if (resp.success) {
                this.updatePicture(resp.result.fileToken);

                //this.appointment.imageId = resp.result.fileToken;
                //this.idPicture = resp.result.fileToken;
                //this.appointment.imageId = resp.result.fileToken;
                
            }
            else {
                this.message.error(resp.error.message);
            }
        };
        this.uploader.setOptions(this._uploaderOptions);
    }

    updatePicture(fileToken: string): void {
        const input = new UpdatePictureInput();
        input.fileToken = fileToken;
        input.x = 0;
        input.y = 0;
        input.width = 0;
        input.height = 0;
        this.saving = true;
        this._appointmentsServiceProxy.updatePictureForAppointment(input)
        .pipe(
            //tap(result => this.appointment.imageId = result.toString())
            finalize(() => {
                this.saving = false;
            })
        )
        .subscribe((result) => {
            //this.active = true;
            this.appointment.imageId = result.toString();
            //abp.event.trigger('pictureChanged');
        })
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
            //this.image = this.imageReader.readAsDataURL(this.imageBlob);
            this.image = 'data:image/jpg;base64,' + this.imageBlob;
        });
    }

    upload(): void {
        this.uploader.uploadAll();
        this.notify.info(this.l('UploadSuccessfully'));
    }

    show(appointmentId?: string): void {
        this.initializeModal();
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

    // onUpload(event): void {
    //     for (const file of event.files) {
    //         this.uploadedFiles.push(file);
    //     }
    // }

    // onBeforeSend(event): void {
    //     event.xhr.setRequestHeader('Authorization', 'Bearer' + abp.auth.getToken());
    // }
}