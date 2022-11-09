import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { TitleRoutingModule } from './title-routing.module';
import { TitlesComponent } from './titles.component';
import { CreateOrEditTitleModalComponent } from './create-or-edit-title-modal.component';
import { ViewTitleModalComponent } from './view-title-modal.component';

@NgModule({
    declarations: [TitlesComponent, CreateOrEditTitleModalComponent, ViewTitleModalComponent],
    imports: [AppSharedModule, TitleRoutingModule, AdminSharedModule],
})
export class TitleModule {}
