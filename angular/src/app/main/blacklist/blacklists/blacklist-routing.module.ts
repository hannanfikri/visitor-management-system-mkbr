import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BlacklistsComponent } from './blacklists.component';

const routes: Routes = [
    {
        path: '',
        component: BlacklistsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class BlacklistRoutingModule {}
