import { Component, OnInit, signal, inject } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';
import { Vehicule, VehiculeService } from '../../services/vehicule.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { ReferentielService } from '../../services/employe.service';
import { FileUploadModule } from "primeng/fileupload";
import { DetailsVehiculeComponent } from "./details-vehicule/details-vehicule.component";
import { DepensesVehiculeComponent } from "./depenses-vehicule/depenses-vehicule.component";

@Component({
  selector: 'app-vehicule',
  templateUrl: './vehicule.component.html',
  styleUrls: ['./vehicule.component.scss'],
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
    InputIconModule,
    FileUploadModule,
    DetailsVehiculeComponent,
    DepensesVehiculeComponent
],
  providers: [
    ReferentielService,
    ConfirmationService,
    VehiculeService
  ]
})
export class VehiculeComponent implements OnInit {
    vehicules = signal<Vehicule[]>([]);
    vehiculeDialog = false;

    submitted = false;

    vehicule: Vehicule = {} as Vehicule;

    cols: any[] = [];

    typeVehicules = [];

    statuts = [];

    selectedPhoto!: File;
    photoPreview: string | ArrayBuffer | null = null;
    afficherDetais = signal<boolean>(false);
    afficherdepenses = signal<boolean>(false);

    constructor(
        private vehiculeService: VehiculeService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private referentielService: ReferentielService
    ) {}

    ngOnInit(): void {
        this.loadVehicules();
        this.loadStatutsVehicule();
        this.loadTypeVehicule();

        this.cols = [
            { field: 'immatriculation', header: 'Immatriculation' },
            { field: 'marque', header: 'Marque' },
            { field: 'modele', header: 'Modèle' },
            { field: 'annee', header: 'Année' }
        ];
    }

    loadStatutsVehicule() {
        this.referentielService.getDataFromEndpoint('GetStatusVehiculeAsync').subscribe({
            next: (res) => {
                this.statuts = res;
            }
        })
    }

    loadTypeVehicule() {
        this.referentielService.getDataFromEndpoint('GetTypeVehiculesAsync').subscribe({
            next: (res) => {
                this.typeVehicules = res;
            }
        })
    }

    loadVehicules() {
        this.vehiculeService.getAll().subscribe({
            next: (res) => {
                this.vehicules.set(res);
            }
        });
    }

    openNew() {
        this.vehicule = {} as Vehicule;
        this.submitted = false;
        this.vehiculeDialog = true;
    }

    hideDialog() {
        this.vehiculeDialog = false;
        this.submitted = false;
    }

    saveVehicule() {

        this.submitted = true;

        if (!this.vehicule.immatriculation ||
            !this.vehicule.marque ||
            !this.vehicule.modele) {
            return;
        }

        if (this.vehicule.idVehicule) {

            this.vehiculeService
                .ModifierVehicule(this.vehicule.idVehicule, this.vehicule)
                .subscribe({
                    next: () => {

                        this.loadVehicules();

                        this.messageService.add({
                            severity: 'success',
                            summary: 'Succès',
                            detail: 'Véhicule modifié'
                        });

                        this.vehiculeDialog = false;
                    }
                });

        } else {

            this.vehiculeService.AjouterVehicule(this.vehicule)
                .subscribe({
                    next: () => {

                        this.loadVehicules();

                        this.messageService.add({
                            severity: 'success',
                            summary: 'Succès',
                            detail: 'Véhicule créé'
                        });

                        this.vehiculeDialog = false;
                    }
                });
        }
    }

    editVehicule(vehicule: Vehicule) {
        this.vehicule = { ...vehicule
            ,

        dateAcquisition: vehicule.dateAcquisition
            ? new Date(vehicule.dateAcquisition)
            : undefined,

        assuranceExpiration: vehicule.assuranceExpiration
            ? new Date(vehicule.assuranceExpiration)
            : undefined,

        vignetteExpiration: vehicule.vignetteExpiration
            ? new Date(vehicule.vignetteExpiration)
            : undefined,

        visiteTechniqueExpiration: vehicule.visiteTechniqueExpiration
            ? new Date(vehicule.visiteTechniqueExpiration)
            : undefined
         };
        this.vehiculeDialog = true;
    }

    deleteVehicule(vehicule: Vehicule) {

        this.confirmationService.confirm({
            message: 'Voulez-vous supprimer ce véhicule ?',
            header: 'Confirmation',
            icon: 'pi pi-exclamation-triangle',

            accept: () => {

                this.vehiculeService.delete(vehicule.idVehicule!)
                    .subscribe({
                        next: () => {

                            this.loadVehicules();

                            this.messageService.add({
                                severity: 'success',
                                summary: 'Succès',
                                detail: 'Véhicule supprimé'
                            });
                        }
                    });
            }
        });
    }

    onGlobalFilter(table: any, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    getSeverity(statut: string):'success' | 'secondary' | 'info' | 'warn' | 'danger' | 'contrast' | undefined | null {

        switch (statut) {

            case 'ACTIF':
                return 'success';

            case 'EN PANNE':
                return 'danger';

            default:
                return 'warn';
        }
    }

    onPhotoSelected(event: any) {
        const file = event.target.files[0];
        console.log(file);

        this.vehicule.fichierPhoto = file;
        if (file) {

            this.selectedPhoto = file;

            const reader = new FileReader();

            reader.onload = () => {
                this.photoPreview = reader.result;
            };

            reader.readAsDataURL(file);
        }
    }

    onUpload(event: any) {
        this.vehicule.fichierPhoto = event.files[0];
        console.log(this.vehicule.fichierPhoto);
    }

    getVehicleDetails(vehicule: Vehicule) {
        this.vehicule = vehicule;
        this.afficherDetais.set(false);
        this.afficherdepenses.set(false);

        setTimeout(()=>{
            this.afficherDetais.set(true);
        }, 600)
    }

    detaisDepenses() {
        this.afficherdepenses.set(false);
        this.afficherDetais.set(false);
        setTimeout(()=>{
            this.afficherdepenses.set(true);
        }, 600)
    }
}
