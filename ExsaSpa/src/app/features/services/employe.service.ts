import { inject, Injectable, signal } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Employee {
    idEmploye?:string;
    nomEmploye?: string;
    numeroEmploye?: string;
    salaireBaseXaf?: string;
    typeContrat?: string;
    dateEmbauche?:string;
    numeroCnps?: string;
    estActif?: boolean;
    dateCreation?: string;
    dateModification?: string;
    idUtilisateur?: string;
    idLocataire?: string;
    utilisateur?: Utilisateur;
}

export class Utilisateur {
    idUtilisateur?:string;
    nomComplet?: string;
    estActif?: boolean;
    dateCreation?: string;
    dateModification?: string;
    idLocataire?: string;
    email?: string;
    passwordHash?: string;
    telephone?: string;
    role?: string;
}


export interface CreateEmployeeDto {
    idLocataire?: string;
    idUtilisateur?: string;
    idEmploye?:string;
    numeroEmploye?: string;
    salaireBaseXaf?: string;
    typeContrat?: string;
    dateEmbauche?:string;
    numeroCnps?: string;
    estActif?: boolean;
    dateCreation?: string;
    dateModification?: string;
    nomComplet?: string;
    email?: string;
    passwordHash?: string;
    telephone?: string;
    role?: string;
}

@Injectable()
export class EmployeService extends ApiService<Employee> {

    constructor() {
        super('employes');
    }

    createEmployee(data: CreateEmployeeDto): Observable<Employee> {
        return this.httpClient.post<Employee>(`${this.baseUrl}/Employes/CreateEmployeUser`, data);
    }

    UpdateEmployee(data: CreateEmployeeDto): Observable<Employee> {
        return this.httpClient.post<Employee>(`${this.baseUrl}/Employes/UpdateEmployeUser`, data);
    }
}


@Injectable()
export class ReferentielService {

    protected httpClient =  inject(HttpClient);
    readonly baseUrl = 'https://localhost:7118/api/Referentiels';

    getDataFromEndpoint(endpoint: string): Observable<any> {
        return this.httpClient.get(`${this.baseUrl}/${endpoint}`);
    }

}
