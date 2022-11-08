import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [

                    {
                        path: 'appointment/appointments',
                        loadChildren: () => import('./appointment/appointments/appointment.module').then((m) => m.AppointmentModule),
                        data: { permission: 'Pages.Appointments' },
                    },

                    {
                        path: 'department/departments',
                        loadChildren: () => import('./department/departments/department.module').then((m) => m.DepartmentModule),
                        data: { permission: 'Pages.Departments' },
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
