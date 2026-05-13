import { Routes } from '@angular/router';
import { EmployeeList } from './rh/employee-list/employee-list';
import { InterventionComponent } from './core/intervention/intervention.component';
import { StockComponent } from './core/stock/stock.component';

export default [
    { path: 'stocks', component: StockComponent },
    { path: 'employes', component: EmployeeList },
    { path: 'interventions', component: InterventionComponent },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
