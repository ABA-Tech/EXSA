import { Routes } from '@angular/router';
import { AppLayout } from './app/layout/component/app.layout';
import { Dashboard } from './app/pages/dashboard/dashboard';
import { Documentation } from './app/pages/documentation/documentation';
import { Landing } from './app/pages/landing/landing';
import { Notfound } from './app/pages/notfound/notfound';
import { authGuard } from './app/core/auth/auth.guard';
import { DashboardComponent } from './app/core/dashboard/dashboard.component';

export const appRoutes: Routes = [
    {
        path: '',
        component: AppLayout,
        children: [
            { path: '', canActivate: [authGuard], component: DashboardComponent },
            { path: 'uikit', loadChildren: () => import('./app/pages/uikit/uikit.routes') },
            { path: 'documentation', component: Documentation },
            {
                path: 'pages',
                loadChildren: () => import('./app/pages/pages.routes')
            },
            {
                path: 'rh',
                canActivate: [authGuard],
                loadChildren: () => import('./app/features/features.routes')
            }
            // on prendra les différents features par route.
        ]
    },
    {
        path: 'login',
        loadComponent: () =>
            import('./app/features/auth/login/login.component')
                .then(m => m.LoginComponent)
    },
    {
        path: 'access-denied',
        loadComponent: () =>
            import('./app/features/auth/access-denied/access-denied.component')
                .then(m => m.AccessDeniedComponent)
    },
    { path: 'landing', component: Landing },
    { path: 'notfound', component: Notfound },
    { path: 'auth', loadChildren: () => import('./app/pages/auth/auth.routes') },
    { path: '**', redirectTo: '/notfound' }
];
