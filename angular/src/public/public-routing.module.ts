import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormWizardComponent } from './Pages/appointment/form-wizard/form-wizard.component';
import { ViewDetailsComponent } from './Pages/appointment/view-details/view-details.component';
import { PublicComponent } from './public.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: PublicComponent,
                children: [
                    { path: 'new', component: FormWizardComponent },
                    { path: 'appointment-details', component: ViewDetailsComponent },
                    { path: '**', redirectTo: 'new' }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class PublicRoutingModule { }
