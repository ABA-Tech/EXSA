import { CommonModule } from '@angular/common';
import { Component, inject, Input, input, OnInit, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { CalendarIcon } from 'primeng/icons';
import { ProgressBarModule } from 'primeng/progressbar';
import { TagModule } from 'primeng/tag';
// import { DropdownModule } from 'primeng/dropdown'
import { SelectModule } from 'primeng/select';
import { Intervention } from '@/app/features/services/intervention.service';
import { Facture, FactureService, Reglement } from '@/app/features/services/facture.service';
import { ButtonModule } from "primeng/button";
;

@Component({
  selector: 'app-facture',
  templateUrl: './facture.component.html',
  styleUrls: ['./facture.component.scss'],
  imports: [CommonModule, TagModule, ProgressBarModule, DialogModule,
    DatePickerModule, SelectModule,
    ConfirmDialogModule, ReactiveFormsModule, FormsModule, ButtonModule]
})
export class FactureComponent {
    @Input() intervention! : Intervention;
    factureService = inject(FactureService);

    ngOnInit() {
        this.loadFacture();
        // this.loadReglements();
    }

    invoice = signal<Facture>({});
    reglementsFacture = signal<Reglement[]>([]);
    reglementDialogVisible = false;

    newReglement: Reglement = {
     montantXaf: 0,
     modeReglement: '',
     referenceReglement: '',
     dateReglement: new Date(),
     idFacture: this.invoice().idFacture!,
     dateCreation: new Date(),
     dateModification: new Date()
    };

    modesReglement = [
        { label: 'Cash', value: 'Cash' },
        { label: 'Mobile Money', value: 'Mobile Money' },
        { label: 'Virement', value: 'Virement' }
    ];

    get totalRegle() {
    return this.reglementsFacture().reduce((sum, r) => sum + Number(r.montantXaf || 0), 0);
    }

    get resteAPayer() {
    return Math.max(0, this.invoice().sousTotalXaf! - this.totalRegle);
    }

    get pourcentageRegle() {
    return this.invoice().sousTotalXaf! > 0
        ? Math.min(100,  Math.round((this.totalRegle / this.invoice().sousTotalXaf!) * 100))
        : 0;
    }

    openReglementDialog() {
        this.newReglement = {
            montantXaf: this.resteAPayer,
            modeReglement: '',
            referenceReglement: '',
            dateReglement: new Date(),
            idFacture: this.invoice().idFacture!,
            dateCreation: new Date(),
            dateModification: new Date()
        };
        this.reglementDialogVisible = true;
    }

    saveReglement() {
        this.factureService.saveReglement(this.invoice().idFacture!, this.newReglement).subscribe({
            next: (reglement) => {
                this.loadFacture();
                // this.reglementsFacture.update((reglements) => [...reglements, reglement]);
                this.reglementDialogVisible = false;
            }
        });
    }

    loadFacture() {
        if (this.intervention.idIntervention) {
            this.factureService.getFactureByIntervention(this.intervention.idIntervention).subscribe({
                next: (facture) => {
                    if(facture) {
                        this.invoice.set(facture);
                        this.loadReglements();
                    }
                    else {
                        alert("Merci de saisir le montant sur l'intervention avant de générer la facture.")
                    }
                }
            });
        }
    }

    loadReglements() {
        var idFacture = this.invoice().idFacture;
        if (idFacture) {
            this.factureService.getReglementsByFacture(idFacture).subscribe({
                next: (reglements) => {
                    this.reglementsFacture.set(reglements);
                }
            });
        }
    }

}
