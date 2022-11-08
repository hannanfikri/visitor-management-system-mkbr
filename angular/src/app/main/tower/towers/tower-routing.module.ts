import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TowersComponent } from './Towers.component';

const routes: Routes = [
    {
        path: '',
        component: TowersComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TowerRoutingModule {}
