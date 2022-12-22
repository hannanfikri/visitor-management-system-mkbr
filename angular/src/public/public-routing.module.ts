import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormWizardComponent } from './Pages/appointment/form-wizard/form-wizard.component';
import { ViewDetailsComponent} from './Pages/appointment/view-details/view-details.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'new', component: FormWizardComponent },
                    { path: 'details', component: ViewDetailsComponent},
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
