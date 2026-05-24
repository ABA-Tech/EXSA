import { LayoutService } from '@/app/layout/service/layout.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit, effect, inject, signal } from '@angular/core';
import { ChartModule } from 'primeng/chart';

@Component({
  selector: 'app-intervention-polar-area',
  imports: [CommonModule, ChartModule],
  template: `
    <!-- <div class="card flex justify-center">
        <p-chart type="polarArea" [data]="data" [options]="options" class="w-full md:w-[30rem]" />
    </div> -->
    <div class="col-span-12 xl:col-span-6">
        <div class="card flex flex-col items-center">
            <div class="font-semibold text-xl mb-6">Polar Area</div>
            <p-chart type="polarArea" [data]="polarData()" [options]="polarOptions()"></p-chart>
        </div>
    </div>
    `,

})
export class InterventionPolarArea  implements OnInit{

    layoutService = inject(LayoutService);
    polarData = signal<any>(null);
    polarOptions = signal<any>(null);

    chartEffect = effect(() => {
        this.layoutService.layoutConfig().darkTheme;
        setTimeout(() => this.initChart(), 150);
    })

    ngOnInit() {
        this.initChart();
    }

    initChart() {

        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

        this.polarData.set({
            datasets: [
                {
                    data: [11, 16, 7, 3],
                    backgroundColor: [documentStyle.getPropertyValue('--p-indigo-500'), documentStyle.getPropertyValue('--p-purple-500'), documentStyle.getPropertyValue('--p-teal-500'), documentStyle.getPropertyValue('--p-orange-500')],
                    label: 'My dataset'
                }
            ],
            labels: ['Indigo', 'Purple', 'Teal', 'Orange']
        });

        this.polarOptions.set({
            plugins: {
                legend: {
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                r: {
                    grid: {
                        color: surfaceBorder
                    },
                    ticks: {
                        display: false,
                        color: textColorSecondary
                    }
                }
            }
        });
    }
}
