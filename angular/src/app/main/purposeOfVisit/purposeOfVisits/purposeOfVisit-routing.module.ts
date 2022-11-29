import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PurposeOfVisitsComponent } from './purposeOfVisits.component';

const routes: Routes = [
    {
        path: '',
        component: PurposeOfVisitsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PurposeOfVisitRoutingModule {}
