import { Injectable, signal } from '@angular/core';
import { ApiService } from './api.service';

export interface Employee {
    idEmploye:number;
    nomEmploye: string;
    numeroEmploye: string;
    salaireBaseXaf: string;
    typeContrat: string;
    dateEmbauche:string;
    numeroCnps: string;
    estActif: boolean;
    dateCreation: string;
    dateModification:string;
}

@Injectable()
export class EmployeService extends ApiService<Employee> {

    constructor() {
        super('employes');
    }

}
