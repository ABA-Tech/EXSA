import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { CalendarIcon } from 'primeng/icons';
import { ProgressBarModule } from 'primeng/progressbar';
import { TagModule } from 'primeng/tag';
// import { DropdownModule } from 'primeng/dropdown'
import { SelectModule } from 'primeng/select';
;

@Component({
  selector: 'app-facture',
  templateUrl: './facture.component.html',
  styleUrls: ['./facture.component.scss'],
  imports: [CommonModule,TagModule, ProgressBarModule, DialogModule,
    DatePickerModule, SelectModule,
    ConfirmDialogModule, ReactiveFormsModule, FormsModule]
})
export class FactureComponent {
    reglementDialogVisible = false;

    facture = {
    reference: 'FAC-2026-001',
    nomClient: 'Client X',
    statut: 'PARTIELLEMENT_PAYEE',
    totalXaf: 250000
    };

    reglements = [
    {
        referenceReglement: 'PAY-001',
        modeReglement: 'Mobile Money',
        montantXaf: 100000,
        statutReglement: 'CONFIRME',
        dateReglement: new Date()
    }
    ];

    newReglement = {
    montantXaf: 0,
    modeReglement: '',
    referenceReglement: '',
    dateReglement: new Date()
    };

    modesReglement = [
    { label: 'Cash', value: 'Cash' },
    { label: 'Mobile Money', value: 'Mobile Money' },
    { label: 'Virement', value: 'Virement' }
    ];

    get totalRegle() {
    return this.reglements.reduce((sum, r) => sum + Number(r.montantXaf || 0), 0);
    }

    get resteAPayer() {
    return Math.max(0, this.facture.totalXaf - this.totalRegle);
    }

    get pourcentageRegle() {
    return this.facture.totalXaf > 0
        ? Math.min(100, Math.round((this.totalRegle / this.facture.totalXaf) * 100))
        : 0;
    }

    openReglementDialog() {
    this.newReglement = {
        montantXaf: this.resteAPayer,
        modeReglement: '',
        referenceReglement: '',
        dateReglement: new Date()
    };
    this.reglementDialogVisible = true;
    }

    saveReglement() {
    this.reglements = [
        ...this.reglements,
        { ...this.newReglement, statutReglement: 'CONFIRME' }
    ];
    this.reglementDialogVisible = false;
    }
}
