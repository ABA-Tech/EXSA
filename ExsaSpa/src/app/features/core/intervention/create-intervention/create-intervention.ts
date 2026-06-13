import { Component, inject, Input, Output, EventEmitter, input, output } from '@angular/core';
import { Dialog, DialogModule } from "primeng/dialog";
import { SelectModule } from "primeng/select";
import { DatePickerModule } from "primeng/datepicker";
import { ToggleSwitchModule } from "primeng/toggleswitch";
import { Intervention, InterventionService } from '@/app/features/services/intervention.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-intervention',
  imports: [
    CommonModule,
    FormsModule,
    DialogModule,
    SelectModule,
    DatePickerModule,
    ToggleSwitchModule],
  templateUrl: './create-intervention.html',
  styleUrl: './create-intervention.scss',
})
export class CreateIntervention {
    interventionService = inject(InterventionService);
    interventionDialog = input.required<boolean>();
    @Input() intervention: Intervention = {};
    submitted: boolean = false;
    @Input() typeInterventionList: any[] = [];
    avecMontantConvenu: boolean = false;
    @Output()  evtFermeture = new EventEmitter<boolean>();
    interventionDialogChange = output<boolean>();

    getTotalTTC(): number {
        if (!this.intervention.montantConvenuXaf) return 0;
        return Math.round(this.intervention.montantConvenuXaf * 1.1925 * 100) / 100;
    }

    hideDialog() {
        this.submitted = false;
        this.intervention = {};
        this.evtFermeture.emit(false);
        this.interventionDialogChange.emit(false);
    }

    saveIntervention() {
        this.submitted = true;

        if (!this.avecMontantConvenu) {
            this.intervention.montantConvenuXaf = undefined;
        }

        if (this.intervention.idIntervention) {
            // this.intervention.dateDebut = this.splitDate(this.intervention.dateDebut as string);
            // this.intervention.dateFin = this.splitDate(this.intervention.dateFin as string);
            // this.intervention.datePlanifiee = this.splitDate(this.intervention.datePlanifiee as string);
            // this.intervention.dateValidation = this.splitDate(this.intervention.dateValidation as string);
 
            this.interventionService.update(this.intervention.idIntervention, this.intervention).subscribe({
                next: (data) => {
                    this.avecMontantConvenu = false;
                    this.evtFermeture.emit(true);
                    this.interventionDialogChange.emit(false);
                    // this.loadData();
                },
                error: (err) => {
                    console.error('Error updating intervention:', err);
                }
            });
        }
        else {
            this.intervention.statut = 'CREEE';
            this.interventionService.create(this.intervention).subscribe({
                next: (data) => {
                    this.avecMontantConvenu = false;
                    this.evtFermeture.emit(true);
                    this.interventionDialogChange.emit(false);
                    // this.loadData();
                },
                error: (err) => {
                    console.error('Error creating intervention:', err);
                }
            });
        }
    }


    splitDate(dateString: string |undefined|null): string {
        if (!dateString) return '';

        var dateStr = new Date(dateString).toLocaleDateString('fr-FR');
        var res = dateStr.includes(" ") ? dateStr.split(" ")[0].split("/") : (dateStr.includes("T") ? dateStr.split("T")[0].split("/") : dateStr.split("/"));

        return `${res[2]}-${res[1]}-${res[0]}`;
    }
}
