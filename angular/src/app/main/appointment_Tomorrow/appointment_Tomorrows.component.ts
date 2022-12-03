import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentsServiceProxy, AppointmentDto, GetDepartmentForViewDto, GetAppointmentForViewDto, StatusType, UpdateProfilePictureInput, UpdateProfilePictureInputs } from '@shared/service-proxies/service-proxies';
import { IAjaxResponse, NotifyService, TokenService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAppointmentModalComponent } from './create-or-edit-appointment_Tomorrow-modal.component';

import { ViewAppointmentModalComponent } from './view-appointment_Tomorrow-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { FileUploader, FileUploaderOptions, FileItem } from 'ng2-file-upload';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ImageCroppedEvent, base64ToFile } from 'ngx-image-cropper';

@Component({
    templateUrl: './appointment_Tomorrows.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class AppointmentsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditAppointmentModal', { static: true })
    createOrEditAppointmentModal: CreateOrEditAppointmentModalComponent;
    @ViewChild('viewAppointmentModalComponent', { static: true }) viewAppointmentModal: ViewAppointmentModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    //file upload
    @ViewChild('changeProfilePictureModal', { static: true }) modal: ModalDirective;
    @ViewChild('uploadProfilePictureInputLabel') uploadProfilePictureInputLabel: ElementRef;

    @Output() modalSave: EventEmitter<number> = new EventEmitter<number>();

    advancedFiltersAreShown = false;
    filterText = '';
    fullNameFilter = '';

    sampleDateTime: DateTime;
    dateFormat = 'dd-LL-yyyy HH:mm:ss';

    _entityTypeFullName = 'Visitor.Appointment.Appointment';
    entityHistoryEnabled = false;

    //file upload
    public active = false;
    public uploader: FileUploader;
    public temporaryPictureUrl: string;
    public saving = false;
    public maxProfilPictureBytesUserFriendlyValue = 5;
    public useGravatarProfilePicture = false;

    imageChangedEvent: any = '';
    userId: number = null;

    private _uploaderOptions: FileUploaderOptions = {};

    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService,
        private _tokenService:TokenService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return (
            this.isGrantedAny('Pages.Administration.AuditLogs') &&
            customSettings.EntityHistory &&
            customSettings.EntityHistory.isEnabled &&
            _filter(
                customSettings.EntityHistory.enabledEntities,
                (entityType) => entityType === this._entityTypeFullName
            ).length === 1
        );
    }

    getAppointments(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._appointmentsServiceProxy
            .getAllTomorrow(
                this.filterText,
                this.fullNameFilter,
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            )
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createAppointment(): void {
        this.createOrEditAppointmentModal.show();
    }

    showHistory(appointment: AppointmentDto): void {
        this.entityTypeHistoryModal.show({
            entityId: appointment.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteAppointment(appointment: AppointmentDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._appointmentsServiceProxy.delete(appointment.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
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

    show(userId?: number): void {
        this.initializeModal();
        this.modal.show();
        this.userId = userId;
    }

    close(): void {
        this.active = false;
        this.imageChangedEvent = '';
        this.uploader.clearQueue();
        this.modal.hide();
    }

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
        const input = new UpdateProfilePictureInput();
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

    save(): void {
        if (this.useGravatarProfilePicture) {
            this.updateProfilePictureToUseGravatar();
        } else {
            this.uploader.uploadAll();
        }
    }

    canUseGravatar(): boolean {
        return this.setting.getBoolean('App.UserManagement.AllowUsingGravatarProfilePicture');
    }
}
