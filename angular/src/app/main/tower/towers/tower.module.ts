import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { TowerRoutingModule } from './tower-routing.module';
import { TowersComponent } from './towers.component';
import { CreateOrEditTowerModalComponent } from './create-or-edit-tower-modal.component';
import { ViewTowerModalComponent } from './view-tower-modal.component';

@NgModule({
    declarations: [TowersComponent, CreateOrEditTowerModalComponent, ViewTowerModalComponent],
    imports: [AppSharedModule, TowerRoutingModule, AdminSharedModule],
})
export class TowerModule {}
