import { AlertDto } from '@/app/features/services/dashboard.service';
import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-alerts-widget',
  imports: [CommonModule],
//   templateUrl: './kpis-widget.html',
//   styleUrl: './kpis-widget.scss',
    template: `
        <div class="">
            <ul class="p-0 m-0 list-none">
                <li class="flex items-center py-2 border-b border-surface" *ngFor="let alert of alerts">
                    <div class="w-12 h-12 flex items-center justify-center bg-purple-100 dark:bg-purple-400/10 rounded-full mr-4 shrink-0">
                        <i [class]="'pi pi-' + alert.icon1 + ' text-purple-500'"></i>
                    </div>
                    <span class="text-surface-900 dark:text-surface-0 leading-normal">
                        <div><small><i [class]="'pi pi-' + alert.icon2"></i> {{ alert.nom }}</small></div>
                        <span class="text-primary font-bold"> {{ alert.libelle }} </span>
                        {{ alert.description }}
                    </span>
                </li>
            </ul>
        </div>
        `
})
export class AlertsWidget {

    @Input() alerts:AlertDto[] = [];
    invoices = [
        {
            category: 'Facturation',
            reference: 'FAC-2025-00087',
            status: 'en retard 28 jours',
            amount: 680000,
            action: 'Relancer'
        },
        {
            category: 'Paiement',
            reference: 'PAY-2025-00012',
            status: 'en attente',
            amount: 125000,
            action: 'Voir'
        },
        {
            category: 'Commande',
            reference: 'CMD-2025-00045',
            status: 'validée',
            amount: 980000,
            action: 'Ouvrir'
        }
    ];
}
