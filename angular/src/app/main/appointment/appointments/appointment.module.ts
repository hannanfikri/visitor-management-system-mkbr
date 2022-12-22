import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { AppointmentRoutingModule } from './appointment-routing.module';
import { AppointmentsComponent } from './appointments.component';


import { CreateOrEditAppointmentModalComponent } from './create-or-edit-appointment-modal.component';
import { ViewAppointmentModalComponent } from './view-appointment-modal.component';
import{CheckIn}from './check-in.component'
import { CheckOut } from './check-out.component';

@NgModule({
    declarations: [ AppointmentsComponent, CreateOrEditAppointmentModalComponent, ViewAppointmentModalComponent , CheckIn , CheckOut],
    imports: [AppSharedModule, AppointmentRoutingModule, AdminSharedModule],
})
export class AppointmentModule {}
