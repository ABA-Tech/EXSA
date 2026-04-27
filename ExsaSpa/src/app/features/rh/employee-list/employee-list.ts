import { Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { Product, ProductService } from '@/app/pages/service/product.service';
import { CreateEmployeeDto, Employee, EmployeService, ReferentielService } from '../../services/employe.service';
import { DatePickerModule } from 'primeng/datepicker';

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
  selector: 'app-employee-list',
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
    ConfirmDialogModule
  ],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.scss',
  providers: [EmployeService, ConfirmationService,ReferentielService]
})
export class EmployeeList {

    employes = signal<Employee[]>([]);
    employeDialog: boolean = false;
    employe!: Employee;
    createEmpDto!: CreateEmployeeDto;
    employeForm!: FormGroup;
    typeContrats!: any[];
    listeRoles!: any[];



    visible: boolean = false;


    productDialog: boolean = false;

    products = signal<Product[]>([]);

    product!: Product;

    selectedProducts!: Product[] | null;

    submitted: boolean = false;

    statuses!: any[];

    @ViewChild('dt') dt!: Table;

    exportColumns!: ExportColumn[];

    cols!: Column[];

    constructor(
        private employeService: EmployeService,
        private refService: ReferentielService
    ) {}

    exportCSV() {
        this.dt.exportCSV();
    }

    ngOnInit() {
        this.loadEmployeData();
    }

    loadEmployeData() {
        this.employeService.getAll().subscribe(e=>{
            console.log(e);

            this.employes.set(e);
        });

        this.refService.getDataFromEndpoint('GetTypeContrats').subscribe((data: any) => {
            this.typeContrats = data;
        });

        this.refService.getDataFromEndpoint('GetRoles').subscribe((data: any) => {
            this.listeRoles = data;
        });
    }

    editEmploye(employe: Employee) {

        this.createEmpDto = {
            nomComplet: employe.utilisateur?.nomComplet,
            numeroEmploye: employe.numeroEmploye,
            telephone: employe.utilisateur?.telephone,
            email: employe.utilisateur?.email,
            salaireBaseXaf: employe.salaireBaseXaf,
            typeContrat: employe.typeContrat,
            dateEmbauche: employe.dateEmbauche ? (new Date(employe.dateEmbauche)).toLocaleString('fr-FR') : '',
            numeroCnps: employe.numeroCnps,
            estActif: employe.estActif,
            dateCreation: employe.dateCreation,
            dateModification: employe.dateModification,
            idLocataire: employe.idLocataire,
            idUtilisateur: employe.idUtilisateur,
            idEmploye: employe.idEmploye,
            role: employe.utilisateur?.role
         };
        this.employeDialog = true;
    }

    saveEmploye() {
        this.submitted = true;
        console.log(this.createEmpDto);
        if (!this.createEmpDto.nomComplet
            || !this.createEmpDto.numeroEmploye
            || !this.createEmpDto.salaireBaseXaf
            || !this.createEmpDto.typeContrat
            || !this.createEmpDto.dateEmbauche
            || !this.createEmpDto.numeroCnps
            || !this.createEmpDto.email
            || !this.createEmpDto.telephone
            || !this.createEmpDto.role
        ) {
            return;
        }

        var res = this.createEmpDto.dateEmbauche.split(" ")[0].split("/");
        this.createEmpDto.dateEmbauche = `${res[2]}-${res[1]}-${res[0]}`;
        if (this.createEmpDto.idEmploye) {
            this.employeService.UpdateEmployee(this.createEmpDto).subscribe({
                next: (res) => {
                    this.loadEmployeData();
                    this.employeDialog = false;
                }
            });
        } else {
            this.employeService.createEmployee(this.createEmpDto).subscribe({
                next: (res) => {
                    this.loadEmployeData();
                    this.employeDialog = false;
                }
            });
        }


    }

    showDialog() {
        this.employeDialog = true;
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.createEmpDto = {};
        this.employe = {};
        this.submitted = false;
        this.employeDialog = true;
    }


    getSeverity(status: boolean) {
        switch (status) {
            case true:
                return 'Actif';
            default:
                return 'Inactif';
        }
    }

    getStatus(status: boolean) {
        switch (status) {
            case true:
                return 'success';
            default:
                return 'info';
        }
    }

    deleteEmploye(employe: Employee) {
        this.employe = { ...employe };
        this.employeDialog = true;
    }

    hideDialog() {
        this.employeDialog = false;
        this.submitted = false;
    }

    findIndexById(id: string): number {
        let index = -1;
        for (let i = 0; i < this.products().length; i++) {
            if (this.products()[i].id === id) {
                index = i;
                break;
            }
        }

        return index;
    }

    createId(): string {
        let id = '';
        var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        for (var i = 0; i < 5; i++) {
            id += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        return id;
    }

}
