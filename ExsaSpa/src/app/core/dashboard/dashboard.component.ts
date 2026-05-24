import { CurrencyPipe, DatePipe, DecimalPipe } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';

import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ChartModule } from 'primeng/chart';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';

import {
    DashboardService,
    GlobalDashboardDto,
    RecentInterventionDto
} from '../../features/services/dashboard.service';
import { KpisWidget } from "./components/kpis-widget";
import { InterventionPolarArea } from "./components/intervention-polar-area";
import { AlertsWidget } from "./components/alerts-widget";

@Component({
    selector: 'app-dashboard',
    standalone: true,
    imports: [
    ButtonModule,
    CardModule,
    ChartModule,
    CurrencyPipe,
    DatePipe,
    DecimalPipe,
    ProgressSpinnerModule,
    TableModule,
    TagModule,
    KpisWidget,
    InterventionPolarArea,
    AlertsWidget
],
    template: `
        <div class="grid grid-cols-12 gap-8">
            <app-kpis-widget [kpis]="kpis" class="contents" />
            <app-intervention-polar-area class="col-span-12 lg:col-span-8 xl:col-span-8" />
            <app-alerts-widget class="col-span-12 lg:col-span-4 xl:col-span-4" />
            <!-- <div class="col-span-12 xl:col-span-6">
                <app-recent-sales-widget />
                <app-best-selling-widget />
            </div>
            <div class="col-span-12 xl:col-span-6">
                <app-revenue-stream-widget />
                <app-notifications-widget />
            </div> -->
        </div>

    `
})
export class DashboardComponent implements OnInit {


    kpis = [
        { label: 'Véhicules', value: 152, icon: 'pi pi-shopping-cart', color: 'blue' },
        { label: 'Revenue', value: '$2.100', icon: 'pi pi-dollar', color: 'orange' },
        { label: 'Customers', value: 28441, icon: 'pi pi-users', color: 'cyan' },
        { label: 'Comments', value: '152 Unread', icon: 'pi pi-comment', color: 'purple' }
    ]
























    private readonly dashboardService = inject(DashboardService);

    readonly loading = signal(false);
    readonly dashboard = signal<GlobalDashboardDto | null>(null);
    readonly hasError = signal(false);

    readonly interventionChartData = computed(() => {
        const data = this.dashboard()?.interventionsParStatut ?? [];

        return {
            labels: data.map(x => x.label),
            datasets: [
                {
                    data: data.map(x => x.value)
                }
            ]
        };
    });

    readonly depenseChartData = computed(() => {
        const data = this.dashboard()?.depensesVehiculesParType ?? [];

        return {
            labels: data.map(x => x.label),
            datasets: [
                {
                    label: 'Montant XAF',
                    data: data.map(x => x.value)
                }
            ]
        };
    });

    readonly chartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'bottom'
            }
        }
    };

    readonly barChartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: false
            }
        }
    };

    ngOnInit(): void {
        this.loadDashboard();
    }

    loadDashboard(): void {
        this.loading.set(true);
        this.hasError.set(false);

        this.dashboardService.getGlobalDashboard()
            .pipe(finalize(() => this.loading.set(false)))
            .subscribe({
                next: result => {
                    this.dashboard.set(result);
                },
                error: error => {
                    console.error('Erreur chargement dashboard global', error);
                    this.dashboard.set(null);
                    this.hasError.set(true);
                }
            });
    }

    getInterventionSeverity(
        item: RecentInterventionDto
    ): 'success' | 'info' | 'warn' | 'danger' | 'secondary' {
        const statut = this.normalize(item.statut);

        if (['TERMINE', 'TERMINEE', 'CLOTURE', 'CLOTUREE', 'CLOSED'].includes(statut)) {
            return 'success';
        }

        if (['EN_COURS', 'IN_PROGRESS'].includes(statut)) {
            return 'info';
        }

        if (['ANNULE', 'ANNULEE', 'CANCELLED'].includes(statut)) {
            return 'danger';
        }

        if (Number(item.priorite ?? 0) >= 3) {
            return 'danger';
        }

        return 'secondary';
    }

    private normalize(value: string | null | undefined): string {
        return (value ?? '')
            .trim()
            .toUpperCase()
            .replaceAll('-', '_')
            .replaceAll(' ', '_');
    }
}
