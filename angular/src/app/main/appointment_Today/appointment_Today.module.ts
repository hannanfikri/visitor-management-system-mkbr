import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { AppointmentRoutingModule } from './appointment_Today-routing.module';
import { AppointmentsComponent } from './appointment_Todays.component';
import { CreateOrEditAppointmentModalComponent } from './create-or-edit-appointment_Today-modal.component';
import { ViewAppointmentModalComponent } from './view-appointment_Today-modal.component';
import{CheckIn}from './check-in.component'
import { CheckOut } from './check-out.component';

@NgModule({
    declarations: [AppointmentsComponent, CreateOrEditAppointmentModalComponent, ViewAppointmentModalComponent, CheckIn,CheckOut],
    imports: [AppSharedModule, AppointmentRoutingModule, AdminSharedModule],
})
export class AppointmentTodayModule {}
