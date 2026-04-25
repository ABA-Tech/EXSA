import { Routes } from '@angular/router';
import { EmployeeList } from './rh/employee-list/employee-list';

export default [
    { path: 'employes', component: EmployeeList },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
