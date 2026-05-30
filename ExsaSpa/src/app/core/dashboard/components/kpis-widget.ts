import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-kpis-widget',
  imports: [CommonModule],
//   templateUrl: './kpis-widget.html',
//   styleUrl: './kpis-widget.scss',
    template: `
        @for(kpi of kpis; track kpi.label) {
            <div class="col-span-12 lg:col-span-6 xl:col-span-3">
                <div class="card mb-0">
                    <div class="flex justify-between mb-4">
                        <div>
                            <span class="block text-muted-color font-medium mb-4">{{ kpi.label }}</span>
                            <div class="text-surface-900 dark:text-surface-0 font-medium text-xl">{{ kpi.value }}</div>
                        </div>
                        <div class="flex items-center justify-center" [class]="['bg-' + kpi.color + '-100', 'dark:bg-' + kpi.color + '-400/10', 'rounded-border']" style="width: 2.5rem; height: 2.5rem">
                            <i [class]="'pi pi-' + kpi.icon" [style]="{'color': 'var(--' + kpi.color + '-500)'}" class="text-xl!"></i>
                        </div>
                    </div>
                    <span class="text-primary font-medium">24 new </span>
                    <span class="text-muted-color">since last visit</span>
                </div>
            </div>
        }
        `
})
export class KpisWidget {

    @Input() kpis:any[] = [];

}
