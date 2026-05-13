import { CommonModule } from '@angular/common';
import { Component, Input, OnInit, inject, input, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Table, TableModule } from 'primeng/table';
import { SelectModule } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { ArticleStock } from '@/app/features/services/stock.service';
import { MouvementStock, MouvementStockService } from '@/app/features/services/mouvement-stock.service';
import { IconFieldModule } from "primeng/iconfield";
import { InputIconModule } from "primeng/inputicon";

type MouvementType = 'ENTREE' | 'SORTIE';

@Component({
  selector: 'app-historique-stock',
  templateUrl: './historique-stock.component.html',
  styleUrls: ['./historique-stock.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    SelectModule,
    InputTextModule,
    TagModule,
    IconFieldModule,
    InputIconModule
]
})
export class HistoriqueStockComponent implements OnInit{

    mouvementStockService = inject(MouvementStockService);
    listMvtStock = input.required<MouvementStock[]>();
    @Input() article!:ArticleStock;

    constructor() {

    }

    ngOnInit(): void {
    }

    onGlobalFilter(_t19: Table<MouvementStock>,$event: Event) {
    }

    getTypeLabel(type: MouvementType): string {
        return type === 'ENTREE' ? 'Entrée' : 'Sortie';
    }

    getServerity(type: MouvementType) {
        return type.toUpperCase() === 'SORTIE' ? 'danger' : 'success';
    }

    getTypeIcon(type: MouvementType): string {
        return type === 'SORTIE' ? 'pi pi-arrow-up' : 'pi pi-arrow-down';
    }
}
