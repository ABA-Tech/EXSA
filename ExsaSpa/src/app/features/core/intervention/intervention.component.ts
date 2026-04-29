import { Component, signal, ViewChild, OnInit } from '@angular/core';
import { AffectationIntervention, Intervention, InterventionService } from '../../services/intervention.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { SelectModule } from 'primeng/select';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TextareaModule } from 'primeng/textarea';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { EmployeService, ReferentielService } from '../../services/employe.service';
import { ConfirmationService } from 'primeng/api';
import { FieldsetModule } from 'primeng/fieldset';
import { StepperModule } from 'primeng/stepper';
import { PickListModule } from 'primeng/picklist';


interface Column {
    field: string;
    header: string;
    customExportHeader?: string;
}

interface ExportColumn {
    title: string;
    dataKey: string;
}


@Component({
  selector: 'app-intervention',
  imports: [
    CommonModule,
    TableModule,
    FormsModule,
    ButtonModule,
    RippleModule,
    ToastModule,
    ToolbarModule,
    RatingModule,
    InputTextModule,
    TextareaModule,
    SelectModule,
    RadioButtonModule,
    InputNumberModule,
    ReactiveFormsModule,
    DialogModule,
    TagModule,
    InputIconModule,
    IconFieldModule,
    DatePickerModule,
    ConfirmDialogModule,
    FieldsetModule,
    StepperModule,
    PickListModule
  ],
  templateUrl: './intervention.component.html',
  styleUrl: './intervention.component.scss',
  providers: [InterventionService, ReferentielService, ConfirmationService, EmployeService]
})
export class InterventionComponent {

    interventionList = signal<Intervention[]>([]);
    inerventionDialog: boolean = false;
    intervention!: Intervention;
    submitted: boolean = false;
    displayDetails: boolean = false;
    typeInterventionList: any[] = [];
    statutInterventionList: any[] = [];

    sourceEmployees = signal<any[]>([]);
    targetEmployees = signal<any[]>([]);

    @ViewChild('dt') dt!: Table;
    exportColumns!: ExportColumn[];
    cols!: Column[];

    minNumStep = signal(1);
    maxNumStep = signal(3);
    currentNumStep = signal(1);

    constructor(private interventionService: InterventionService,
            private referentielService: ReferentielService,
            private employeService: EmployeService
    ) {}

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.interventionService.getAll().subscribe({
            next: (data) => {
                this.interventionList.set(data);
            },
            error: (err) => {
                console.error('Error fetching interventions:', err);
            }
        });

        this.referentielService.getDataFromEndpoint('GetTypeInterventions').subscribe({
            next: (data) => {
                this.typeInterventionList = data;

            },
            error: (err) => {
                console.error('Error fetching type interventions:', err);
            }
        });

        this.referentielService.getDataFromEndpoint('GetStatutInterventions').subscribe({
            next: (data) => {
                this.statutInterventionList = data;
            },
            error: (err) => {
                console.error('Error fetching statut interventions:', err);
            }
        });

        this.displayDetails = false;
    }

     editIntervention(intervention: Intervention) {
        this.intervention = { ...intervention };
        this.intervention.dateDebut = new Date(intervention.dateDebut!).toLocaleDateString('fr-FR');
        this.intervention.dateFin = new Date(intervention.dateFin!).toLocaleDateString('fr-FR');
        this.intervention.datePlanifiee = new Date(intervention.datePlanifiee!).toLocaleDateString('fr-FR');
        this.intervention.dateValidation = new Date(intervention.dateValidation!).toLocaleDateString('fr-FR');

        this.inerventionDialog = true;
    }

    saveIntervention() {
        this.submitted = true;
        if (this.intervention.idIntervention) {
            this.intervention.dateDebut = this.splitDate(this.intervention.dateDebut as string);
            this.intervention.dateFin = this.splitDate(this.intervention.dateFin as string);
            this.intervention.datePlanifiee = this.splitDate(this.intervention.datePlanifiee as string);
            this.intervention.dateValidation = this.splitDate(this.intervention.dateValidation as string);

            this.interventionService.update(this.intervention.idIntervention, this.intervention).subscribe({
                next: (data) => {
                    this.inerventionDialog = false;
                    this.loadData();
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
                    this.inerventionDialog = false;
                    this.loadData();
                },
                error: (err) => {
                    console.error('Error creating intervention:', err);
                }
            });
        }
    }


    showDialog() {
        this.inerventionDialog = true;
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.intervention = {};
        this.submitted = false;
        this.inerventionDialog = true;
    }

    exportCSV() {
        this.dt.exportCSV();
    }

    getSeverity(status: string): string {
        switch (status) {
            case 'Planifiée':
                return 'info';
            case 'En cours':
                return 'warning';
            case 'Terminée':
                return 'success';
            case 'Annulée':
                return 'danger';
            default:
                return 'secondary';
        }
    }

     deleteIntervention(intervention: Intervention) {
        this.intervention = { ...intervention };

        console.log(this.intervention);



        this.inerventionDialog = true;
    }

    getStatus(status: string) {
        switch (status) {
            case 'Planifiée':
                return 'info';
            case 'En cours':
                return 'warning';
            default:
                return 'success';
        }
    }

    hideDialog() {
        this.inerventionDialog = false;
        this.submitted = false;
    }

    splitDate(dateString: string |undefined|null): string {
        if (!dateString) return '';

        var dateStr = new Date(dateString).toLocaleDateString('fr-FR');
        var res = dateStr.includes(" ") ? dateStr.split(" ")[0].split("/") : (dateStr.includes("T") ? dateStr.split("T")[0].split("/") : dateStr.split("/"));

        return `${res[2]}-${res[1]}-${res[0]}`;
    }

    formatDate(dateString: string |undefined|null): string {
        if (!dateString || typeof dateString.toString() !== 'string') {
            return '';
        }

        var dateStr = dateString.toString();
        var res = dateStr.includes(" ") ? dateStr.split(" ")[0].split("/") : (dateStr.includes("T") ? dateStr.split("T")[0].split("/") : dateStr.split("/"));
        if(res.length != 3){
            var dateStr = new Date(dateString).toLocaleDateString('fr-FR');
            res = dateStr.includes(" ") ? dateStr.split(" ")[0].split("/") : (dateStr.includes("T") ? dateStr.split("T")[0].split("/") : dateStr.split("/"));
        }

        return `${res[2]}-${res[1]}-${res[0]}`;
    }

    getInterventionDetails(intervention: Intervention) {
        this.displayDetails = true;
        this.intervention = { ...intervention };
        this.intervention.dateDebut = new Date(intervention.dateDebut!).toLocaleDateString('fr-FR');
        this.intervention.dateFin = new Date(intervention.dateFin!).toLocaleDateString('fr-FR');
        this.intervention.datePlanifiee = new Date(intervention.datePlanifiee!).toLocaleDateString('fr-FR');
        this.intervention.dateValidation = new Date(intervention.dateValidation!).toLocaleDateString('fr-FR');

        this.employeService.getAll().subscribe({
            next: (data) => {
                this.sourceEmployees.set(data);

                // this.targetEmployees.set(data.filter((emp: any) => emp.idIntervention === intervention.idIntervention));
            },
            error: (err) => {
                console.error('Error fetching employees:', err);
            }
        });
    }

    etapeSaisie(move: number) {
        if(move> 0) {
            this.submitted = true;
            if (this.intervention.idIntervention) {
                this.intervention.dateDebut = this.formatDate(this.intervention.dateDebut);
                this.intervention.dateFin = this.formatDate(this.intervention.dateFin);
                this.intervention.datePlanifiee = this.formatDate(this.intervention.datePlanifiee);
                this.intervention.dateValidation = this.formatDate(this.intervention.dateValidation);

                // return;
                this.interventionService.update(this.intervention.idIntervention, this.intervention).subscribe({
                    next: (data) => {
                        if(data)
                            this.currentNumStep.set(this.currentNumStep() + move);
                    },
                    error: (err) => {
                        console.error('Error updating intervention:', err);
                    }
                });
            }
        }
        else {
             this.currentNumStep.set(this.currentNumStep() + move);
             this.getInterventionDetails(this.intervention);
        }
    }

    etapeAfectation(move: number) {

        if(move> 0 && this.targetEmployees().length > 0) {
            const employeeIds = this.targetEmployees().map(emp => emp.idUtilisateur);
            this.interventionService.CreateAffectation(this.buildAffectations(this.intervention.idIntervention!, employeeIds)).subscribe({
                next: (data) => {
                    if(data)
                        this.currentNumStep.set(this.currentNumStep() + move);
                },
                error: (err) => {
                    console.error('Error creating affectations:', err);
                }
            });
        }else {
             this.currentNumStep.set(this.currentNumStep() + move);
        }
    }

    buildAffectations(interventionId: string, employeeIds: string[]): AffectationIntervention[] {
        return employeeIds.map(empId => ({
            idIntervention: interventionId,
            idTechnicien: empId,
            dateAffectation: this.formatDate(new Date().toLocaleDateString('fr-FR')),
            estPrincipal: false
        }));
    }
}
