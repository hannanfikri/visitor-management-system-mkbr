import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { BlacklistRoutingModule } from './blacklist-routing.module';
import { BlacklistsComponent } from './blacklists.component';
import { CreateOrEditBlacklistModalComponent } from './create-or-edit-blacklist-modal.component';
import { ViewBlacklistModalComponent } from './view-blacklist-modal.component';

@NgModule({
    declarations: [BlacklistsComponent, CreateOrEditBlacklistModalComponent, ViewBlacklistModalComponent],
    imports: [AppSharedModule, BlacklistRoutingModule, AdminSharedModule],
})
export class BlacklistModule {}
