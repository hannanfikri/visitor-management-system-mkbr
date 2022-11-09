import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'status/statuses',
                        loadChildren: () => import('./status/statuses/status.module').then((m) => m.StatusModule),
                        data: { permission: 'Pages.Statuses' },
                    },
                    {
                        path: 'purposeOfVisit/purposeOfVisits',
                        loadChildren: () => import('./purposeOfVisit/purposeOfVisits/purposeOfVisit.module').then((m) => m.PurposeOfVisitModule),
                        data: { permission: 'Pages.PurposeOfVisits' },
                    },
                    {
                        path: 'tower/towers',
                        loadChildren: () => import('./tower/towers/tower.module').then((m) => m.TowerModule),
                        data: { permission: 'Pages.Towers' },
                    },
                    {
                        path: 'appointment/appointments',
                        loadChildren: () => import('./appointment/appointments/appointment.module').then((m) => m.AppointmentModule),
                        data: { permission: 'Pages.Appointments' },
                    },

                    {
                        path: 'dashboard',
                        loadChildren: () => import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
                        data: { permission: 'Pages.Tenant.Dashboard' },
                    },
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class MainRoutingModule {}
