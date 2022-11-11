import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { LevelRoutingModule } from './level-routing.module';
import { LevelsComponent } from './Levels.component';
import { CreateOrEditLevelModalComponent } from './create-or-edit-Level-modal.component';
import { ViewLevelModalComponent } from './view-Level-modal.component';

@NgModule({
    declarations: [LevelsComponent, CreateOrEditLevelModalComponent, ViewLevelModalComponent],
    imports: [AppSharedModule, LevelRoutingModule, AdminSharedModule],
})
export class LevelModule {}
