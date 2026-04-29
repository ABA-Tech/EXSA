import { Routes } from '@angular/router';
import { EmployeeList } from './rh/employee-list/employee-list';
import { InterventionComponent } from './core/intervention/intervention.component';

export default [
    { path: 'employes', component: EmployeeList },
    { path: 'interventions', component: InterventionComponent },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
