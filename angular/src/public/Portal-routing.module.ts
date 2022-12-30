import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormWizardComponent } from './Pages/appointment/form-wizard/form-wizard.component';
import { ViewDetailsComponent } from './Pages/appointment/view-details/view-details.component';
import { PortalComponent } from './Portal.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: PortalComponent,
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
export class PortalRoutingModule { }