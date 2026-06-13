import { Component, signal, ViewChild, OnInit, computed, inject } from '@angular/core';
import { AffectationIntervention, Intervention, InterventionService, PhotoIntervention, UploadPhoto } from '../../services/intervention.service';
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
import { ConfirmationService, MessageService } from 'primeng/api';
import { FieldsetModule } from 'primeng/fieldset';
import { StepperModule } from 'primeng/stepper';
import { PickListModule } from 'primeng/picklist';
import { CardModule } from 'primeng/card';
import { ChipModule } from 'primeng/chip';
import { FileUploadModule } from 'primeng/fileupload';
import { DataViewModule } from 'primeng/dataview';
import { FraisTechniciensComponent } from "./frais-techniciens/frais-techniciens.component";
import { RationTransportGridDto } from '../../models/RationTransportGridDto';
import { FactureComponent } from "./facture/facture.component";
import { CreateIntervention } from "./create-intervention/create-intervention";

interface Column {
    field: string;
    header: string;
    customExportHeader?: string;
}

interface ExportColumn {
    title: string;
    dataKey: string;
}

interface UploadEvent {
    originalEvent: Event;
    files: File[];
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
    PickListModule,
    CardModule,
    ChipModule,
    FileUploadModule,
    DataViewModule,
    FraisTechniciensComponent,
    FactureComponent,
    CreateIntervention
],
  templateUrl: './intervention.component.html',
  styleUrl: './intervention.component.scss',
  providers: [InterventionService, ReferentielService, ConfirmationService, EmployeService,MessageService]
})
export class InterventionComponent {

    interventionList = signal<Intervention[]>([]);
    inerventionDialog = signal<boolean>(false);
    intervention: Intervention = {};
    submitted: boolean = false;
    displayDetails: boolean = false;
    displayDepense= signal<boolean>(false);
    displayFacture= signal<boolean>(false);
    typeInterventionList: any[] = [];
    statutInterventionList: any[] = [];
    typePhotoInterventionList: any[] = [];
    photoUpload!: UploadPhoto;

    uploadedFiles: any[] = [];
    private messageService = inject(MessageService);


    grilleDepense = signal<RationTransportGridDto>({});
    sourceEmployeesData = signal<any[]>([]);
    sourceEmployees = signal<any[]>([]);
    targetEmployees = signal<any[]>([]);
    affectationList = signal<AffectationIntervention[]>([]);

    photoInterventionList = signal<PhotoIntervention[]>([]);


    @ViewChild('dt') dt!: Table;
    exportColumns!: ExportColumn[];
    cols!: Column[];

    minNumStep = signal(1);
    maxNumStep = signal(3);
    currentNumStep = signal(1);


    selectedPhotoIndex = 0;



    constructor(private interventionService: InterventionService,
            private referentielService: ReferentielService,
            private employeService: EmployeService
    ) {}

    ngOnInit() {
        this.loadData();
        this.photoUpload = {};
    }

    getInterventions() {
        this.interventionService.getAll().subscribe({
            next: (data) => {
                this.interventionList.set(data);
            },
            error: (err) => {
                console.error('Error fetching interventions:', err);
            }
        });
    }

    getLibelleTypeIntervention(code: string | undefined): string {
        const type = this.typeInterventionList.find(t => t.code === code);
        return type ? type.libelle : code || '';
    }

    getLibelleStatutIntervention(code: string | undefined): string {
        const statut = this.statutInterventionList.find(s => s.code === code);
        return statut ? statut.libelle : code || '';
    }

    loadData() {
        this.getInterventions();

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

        this.referentielService.getDataFromEndpoint('GetTypePhotos').subscribe({
            next: (data) => {
                this.typePhotoInterventionList = data;
            },
            error: (err) => {
                console.error('Error fetching type photo interventions:', err);
            }
        });

        this.displayDetails = false;
        this.displayDepense.set(false);
    }

     editIntervention(intervention: Intervention) {
        this.intervention = { ...intervention };

        this.intervention.dateDebut = intervention.dateDebut?.includes("T") ? intervention.dateDebut?.toString().split("T")[0] : intervention.dateDebut;
        this.intervention.dateFin = intervention.dateFin?.includes("T") ? intervention.dateFin?.toString().split("T")[0] : intervention.dateFin;
        this.intervention.datePlanifiee = intervention.datePlanifiee?.includes("T") ? intervention.datePlanifiee?.toString().split("T")[0] : intervention.datePlanifiee;
        this.intervention.dateValidation = intervention.dateValidation?.includes("T") ? intervention.dateValidation?.toString().split("T")[0] : intervention.dateValidation;
        console.log(this.intervention.dateDebut)
        this.inerventionDialog.set(true);
    }



    showDialog() {
        this.inerventionDialog.set(true);
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.intervention = {};
        this.submitted = false;
        this.inerventionDialog.set(true);
    }

    exportCSV() {
        this.dt.exportCSV();
    }

    getSeverity(status: string | undefined): 'success' | 'secondary' | 'info' | 'warn' | 'danger' | 'contrast' | undefined | null {
        switch (status) {
            case 'CREEE':
                return 'info';
            case 'EN_ATTENTE':
                return 'warn';
            case 'EN_COURS':
                return 'danger';
            case 'VALIDEE':
                return 'success';
            case 'FACTUREE':
                return 'secondary';
            case 'TERMINEE':
                return 'contrast';
            case 'AFFECTATION':
                return 'contrast';
            default:
                return undefined;
        }
    }

     deleteIntervention(intervention: Intervention) {
        this.intervention = { ...intervention };

        this.inerventionDialog.set(true);
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
        this.inerventionDialog.set(false);
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

    getPhotoType(typeCode: string | undefined): string {
        const type = this.typePhotoInterventionList.find(t => t.code === typeCode);
        return type ? type.libelle : typeCode || '';
    }

    getInterventionDetails(intervention: Intervention) {
        this.displayDetails = true;
        this.displayDepense.set(false);
        this.intervention = { ...intervention };

        this.intervention.dateDebut = intervention.dateDebut?.includes("T") ? intervention.dateDebut?.toString().split("T")[0] : intervention.dateDebut;
        this.intervention.dateFin = intervention.dateFin?.includes("T") ? intervention.dateFin?.toString().split("T")[0] : intervention.dateFin;
        this.intervention.datePlanifiee = intervention.datePlanifiee?.includes("T") ? intervention.datePlanifiee?.toString().split("T")[0] : intervention.datePlanifiee;
        this.intervention.dateValidation = intervention.dateValidation?.includes("T") ? intervention.dateValidation?.toString().split("T")[0] : intervention.dateValidation;
        // this.intervention.dateDebut = new Date(intervention.dateDebut!).toLocaleDateString('fr-FR');
        // this.intervention.dateFin = new Date(intervention.dateFin!).toLocaleDateString('fr-FR');
        // this.intervention.datePlanifiee = new Date(intervention.datePlanifiee!).toLocaleDateString('fr-FR');
        // this.intervention.dateValidation = new Date(intervention.dateValidation!).toLocaleDateString('fr-FR');
        this.currentNumStep.set(intervention.statutIntervention ? intervention.statutIntervention.ordre : 1);

        this.employeService.getAll().subscribe({
            next: (data) => {
                this.sourceEmployeesData.set(data);
                this.getAffectations(intervention.idIntervention!);
            },
            error: (err) => {
                console.error('Error fetching employees:', err);
            }
        });
        this.getPhotosIntervention();
    }

    getAffectations(interventionId: string) {
        this.interventionService.GetAffectations(interventionId).subscribe({
            next: (data) => {
                if(data) {
                    this.affectationList.set(data);
                    console.log(data);

                    const assignedEmployeeIds = data.map(a => a.idTechnicien);
                    this.targetEmployees.set(this.sourceEmployeesData().filter(emp => assignedEmployeeIds.includes(emp.idUtilisateur)));
                    this.sourceEmployees.set(this.sourceEmployeesData().filter(emp => !assignedEmployeeIds.includes(emp.idUtilisateur)));
                }
            },
            error: (err) => {
                console.error('Error fetching intervention details:', err);
            }
        });
    }

    updateStatutIntervention() {
        this.interventionService.UpdateStatutIntervention(this.intervention.idIntervention!, this.intervention.statut!).subscribe({
            next: (data) => {
                if(data) {
                    this.getInterventions();
                    //this.getInterventionDetails(this.intervention);
                }
            },
            error: (err) => {
                console.error('Error updating intervention status:', err);
            }
        });
    }

    etapeSaisie(move: number) {
        if(move> 0) {
            this.submitted = true;
            if (this.intervention.idIntervention) {
                // this.intervention.dateDebut = this.formatDate(this.intervention.dateDebut);
                // this.intervention.dateFin = this.formatDate(this.intervention.dateFin);
                // this.intervention.datePlanifiee = this.formatDate(this.intervention.datePlanifiee);
                // this.intervention.dateValidation = this.formatDate(this.intervention.dateValidation);

                // return;
                this.interventionService.update(this.intervention.idIntervention, this.intervention).subscribe({
                    next: (data) => {
                        if(data) {
                            this.currentNumStep.set(this.currentNumStep() + move);
                            this.intervention.statut = 'AFFECTATION';
                            this.updateStatutIntervention();
                        }
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
                    if(data) {
                        this.currentNumStep.set(this.currentNumStep() + move);
                        this.intervention.statut = 'EN_ATTENTE';
                        this.updateStatutIntervention();
                    }
                },
                error: (err) => {
                    console.error('Error creating affectations:', err);
                }
            });
        }else {
            this.intervention.statut = 'CREEE';
            this.updateStatutIntervention();
            this.currentNumStep.set(this.currentNumStep() + move);
        }
    }

    etapeTechnicienDemarre(move: number) {
        if(move> 0) {
            this.intervention.statut = 'EN_COURS';
            this.updateStatutIntervention();
        }
        else {
            this.intervention.statut = 'AFFECTATION';
            this.updateStatutIntervention();
        }
        this.currentNumStep.set(this.currentNumStep() + move);
    }

    buildAffectations(interventionId: string, employeeIds: string[]): AffectationIntervention[] {
        return employeeIds.map(empId => ({
            idIntervention: interventionId,
            idTechnicien: empId,
            dateAffectation: this.formatDate(new Date().toLocaleDateString('fr-FR')),
            estPrincipal: false
        }));
    }

    removeAffectation(affectation: AffectationIntervention) {
        this.interventionService.RemoveAffectation(affectation).subscribe({
            next: () => {
                this.getAffectations(this.intervention.idIntervention!);
                // this.affectationList.set(this.affectationList().filter(a => a.idTechnicien !== affectation.idTechnicien));
                // this.targetEmployees.set(this.targetEmployees().filter((emp: any) => emp.idUtilisateur !== affectation.idTechnicien));
                // this.sourceEmployees.set([...this.sourceEmployees(), affectation.idTechnicien].map(id => this.sourceEmployeesData().find(emp => emp.idUtilisateur === id)));
            },
            error: (err) => {
                console.error('Error removing affectation:', err);
            }
        });
    }

    onMoveToSource(event: any) {
        const movedItems = event.items;
        const movedItemIds = movedItems.map((item: any) => item.idUtilisateur);
        var affectationsToRemove = this.affectationList().filter(a => movedItemIds.includes(a.idTechnicien!))[0];
        console.log(affectationsToRemove);
        this.removeAffectation(affectationsToRemove);
    }

    onMoveAllToSource(event: any) {
        for (let item of this.targetEmployees()) {
            this.removeAffectation(item);
        }
    }

    onUpload(event: any) {
        this.photoUpload.files = event.files;
        this.EnregistrerPhoto();
    }

    EnregistrerPhoto() {
        if(this.photoUpload.datePrise && this.photoUpload.typePhoto && this.photoUpload.files && this.photoUpload.files.length > 0) {
            this.photoUpload.IdIntervention = this.intervention.idIntervention;
            this.interventionService.UploadInterventionPhoto(this.photoUpload).subscribe({
                next: (res) => {
                    this.messageService.add({ severity: 'success', summary: 'Succès', detail: 'Photo(s) uploadée(s) avec succès.' });
                    this.getPhotosIntervention();
                },
                error: (err) => {
                    this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Erreur lors de l\'upload de la photo.' });
                }
            });
        }
    }

    etapeDemarrage(move: number, etat: string = '') {
        if(move> 0) {
            this.intervention.statut = etat;
            this.currentNumStep.set(this.currentNumStep() + move);

            this.updateStatutIntervention();
        } else {
            // this.intervention.statut = 'EN_ATTENTE';
            this.intervention.statut = etat;
            this.currentNumStep.set(this.currentNumStep() + move);
            this.updateStatutIntervention();
        }
    }

    getPhotosIntervention() {
        this.interventionService.GetPhotosIntervention(this.intervention.idIntervention!).subscribe({
            next: (data) => {
                this.photoInterventionList.set(data);
            },
            error: (err) => {
                console.error('Error fetching intervention photos:', err);
            }
        });
    }

    deletePhoto(photo: PhotoIntervention) {
        this.interventionService.DeletePhoto(photo.idPhoto!).subscribe({
            next: () => {
                this.messageService.add({ severity: 'success', summary: 'Succès', detail: 'Photo supprimée avec succès.' });
                this.getPhotosIntervention();
            },
            error: (err) => {
                this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Erreur lors de la suppression de la photo.' });
            }
        });
    }

    getDepenses(intervention: Intervention) {
        this.intervention = {}
        this.displayDepense.set(false);
        this.intervention = { ...intervention };
        // this.loadGridDepense();
        this.displayDetails = false;
        this.displayFacture.set(false);
        this.inerventionDialog.set(false);
        this.getAffectations(this.intervention.idIntervention!);
        setTimeout(() => {
            this.displayDepense.set(true);
        }, 500);
    }


    loadGridDepense() {
        this.interventionService.ChargerGrilleDepenses(this.intervention.idIntervention!)
            .subscribe(x=>{
                this.grilleDepense.set(x);
            })
    }

    dateDiff(date1: Date | any, date2: Date | any): string {
        const msParJour = 1000 * 60 * 60 * 24;

        if(date1.includes(" ") || date1.includes("T")){
            var dateStr = date1.includes(" ") ? date1.split(" ")[0].split("/") : (date1.includes("T") ? date1.split("T")[0].split("/") : date1.split("/"));
            date1 = `${dateStr[2]}-${dateStr[1]}-${dateStr[0]}`;
            var dateStr2 = date2.includes(" ") ? date2.split(" ")[0].split("/") : (date2.includes("T") ? date2.split("T")[0].split("/") : date2.split("/"));
            date2 = `${dateStr2[2]}-${dateStr2[1]}-${dateStr2[0]}`;
        }
        else if(date1.includes("/") && date2.includes("/")){
            var dateStr = date1.split("/");
            date1 = `${dateStr[2]}-${dateStr[1]}-${dateStr[0]}`;
            var dateStr2 = date2.split("/");
            date2 = `${dateStr2[2]}-${dateStr2[1]}-${dateStr2[0]}`;
        }

        let totalJours = Math.round(Math.abs(new Date(date2).getTime() - new Date(date1).getTime()) / msParJour);

        const annees = Math.floor(totalJours / 365);
        totalJours %= 365;
        const mois = Math.floor(totalJours / 30);
        totalJours %= 30;
        const semaines = Math.floor(totalJours / 7);
        const jours = totalJours % 7;

        const parts: string[] = [];
        if (annees)   parts.push(`${annees} an${annees > 1 ? 's' : ''}`);
        if (mois)     parts.push(`${mois} mois`);
        if (semaines) parts.push(`${semaines} semaine${semaines > 1 ? 's' : ''}`);
        if (jours)    parts.push(`${jours} jour${jours > 1 ? 's' : ''}`);

        return parts.length
            ? parts.slice(0, -1).join(', ') + (parts.length > 1 ? ' et ' : '') + parts.at(-1)!
            : "Aujourd'hui";
    }


    prevPhoto() {
        const list = this.photoInterventionList();
        if (!list.length) return;
        this.selectedPhotoIndex = (this.selectedPhotoIndex - 1 + list.length) % list.length;
    }

    nextPhoto() {
        const list = this.photoInterventionList();
        if (!list.length) return;
        this.selectedPhotoIndex = (this.selectedPhotoIndex + 1) % list.length;
    }


    getFracture(intervention: Intervention) {
        this.intervention = {}
        this.displayDepense.set(false);
        this.displayFacture.set(false);
        this.intervention = { ...intervention };
        // this.loadGridDepense();
        this.displayDetails = false;
        this.inerventionDialog.set(false);
        this.getAffectations(this.intervention.idIntervention!);
        setTimeout(() => {
            this.displayFacture.set(true);
        }, 500);
    }


    onInterventionDialogClose(loadData: boolean) {
        this.inerventionDialog.set(false);
        if(loadData) {
            this.loadData();
        }
    }
}
