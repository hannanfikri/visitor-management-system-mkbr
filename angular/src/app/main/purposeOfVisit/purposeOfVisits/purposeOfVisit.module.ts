import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { PurposeOfVisitRoutingModule } from './purposeOfVisit-routing.module';
import { PurposeOfVisitsComponent } from './purposeOfVisits.component';
import { CreateOrEditPurposeOfVisitModalComponent } from './create-or-edit-purposeOfVisit-modal.component';
import { ViewPurposeOfVisitModalComponent } from './view-purposeOfVisit-modal.component';

@NgModule({
    declarations: [PurposeOfVisitsComponent, CreateOrEditPurposeOfVisitModalComponent, ViewPurposeOfVisitModalComponent],
    imports: [AppSharedModule, PurposeOfVisitRoutingModule, AdminSharedModule],
})
export class PurposeOfVisitModule {}
