import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TitlesComponent } from './titles.component';

const routes: Routes = [
    {
        path: '',
        component: TitlesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TitleRoutingModule {}
