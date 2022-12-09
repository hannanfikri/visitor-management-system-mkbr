import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { AppointmentRoutingModule } from './appointment-routing.module';
import { AppointmentsComponent } from './appointments.component';


import { CreateOrEditAppointmentModalComponent } from './create-or-edit-appointment-modal.component';
import { ViewAppointmentModalComponent } from './view-appointment-modal.component';

@NgModule({
    declarations: [ AppointmentsComponent, CreateOrEditAppointmentModalComponent, ViewAppointmentModalComponent],
    imports: [AppSharedModule, AppointmentRoutingModule, AdminSharedModule],
})
export class AppointmentModule {}
