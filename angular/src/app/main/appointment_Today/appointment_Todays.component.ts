import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentsServiceProxy, AppointmentDto, GetDepartmentForViewDto, GetAppointmentForViewDto, StatusType} from '@shared/service-proxies/service-proxies';
import { IAjaxResponse, NotifyService, TokenService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAppointmentModalComponent } from './create-or-edit-appointment_Today-modal.component';

import { ViewAppointmentModalComponent } from './view-appointment_Today-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './appointment_Todays.component.html',
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


    advancedFiltersAreShown = false;
    filterText = '';
    fullNameFilter = '';
    identityCardFilter = '';
    phoneNoFilter = '';
    emailFilter = '';
    titleFilter = '';
    companyNameFilter = '';
    officerToMeetFilter = '';
    purposeOfVisitFilter = '';
    departmentFilter = '';
    towerFilter = '';
    levelFilter = '';


    _entityTypeFullName = 'Visitor.Appointment.Appointment';
    entityHistoryEnabled = false;

    
    constructor(
        injector: Injector,
        private _appointmentsServiceProxy: AppointmentsServiceProxy,
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
            .getAllToday(
                this.filterText,
                this.fullNameFilter,
                this.identityCardFilter,
                this.phoneNoFilter,
                this.emailFilter,
                this.titleFilter,
                this.companyNameFilter,
                this.officerToMeetFilter,
                this.purposeOfVisitFilter,
                this.departmentFilter,
                this.towerFilter,
                this.levelFilter,
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
}
