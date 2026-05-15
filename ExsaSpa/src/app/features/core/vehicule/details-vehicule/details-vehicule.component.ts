import { DepenseVehiculeService } from '@/app/features/services/depense-vehicule.service';
import { DepenseVehicule, Vehicule } from '@/app/features/services/vehicule.service';
import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnInit, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from "primeng/button";
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

@Component({
  selector: 'app-details-vehicule',
  templateUrl: './details-vehicule.component.html',
  styleUrls: ['./details-vehicule.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TableModule,
    ToolbarModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    TagModule,
    ConfirmDialogModule,
    ToastModule,
    InputNumberModule,
    SelectModule,
    DatePickerModule,
    IconFieldModule,
    InputIconModule
    ]
})
export class DetailsVehiculeComponent implements OnInit {
    depenseVehiculeService = inject(DepenseVehiculeService);

    @Input() vehicule!: Vehicule;

    depenses= signal<DepenseVehicule[]>([]);

    totalDepenses = 0;

    moyenneDepenses = 0;

    ageVehicule = 0;


  constructor() { }

  ngOnInit() {
    this.loadDepensesVehicule()

    this.ageVehicule =
        new Date().getFullYear() -
        (this.vehicule.annee ?? 0);

    this.moyenneDepenses =
        this.totalDepenses / 12;
  }

  loadDepensesVehicule() {
    this.depenseVehiculeService.findAll().subscribe({
        next: (res)=>{
            this.depenses.set(res.content);
            this.totalDepenses =
            this.depenses().reduce(
                (a, b) => a + (b.montantXaf ?? 0),
                0
            );
        }
    })
  }

  formatDate(value: Date | string | null | undefined): string {
    if (!value) {
        return '';
    }

    return new Date(value).toLocaleDateString('fr-FR');
}

}
