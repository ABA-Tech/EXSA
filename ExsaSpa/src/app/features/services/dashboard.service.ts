import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { forkJoin, map, Observable } from 'rxjs';

export interface GlobalDashboardDto {
    kpis: DashboardKpiDto;

    interventionsParStatut: DashboardChartItemDto[];

    depensesVehiculesParType: DashboardChartItemDto[];

    dernieresInterventions: RecentInterventionDto[];

    articlesCritiques: ArticleCritiqueDto[];

    alertesVehicules: VehiculeAlerteDto[];

    dernieresDepensesVehicules: RecentDepenseVehiculeDto[];

    facturesEnRetard: FactureAlerteDto[];
}

export interface DashboardKpiDto {
    interventionsTotal: number;

    interventionsOuvertes: number;

    interventionsEnCours: number;

    interventionsTerminees: number;

    interventionsUrgentes: number;

    articlesTotal: number;

    articlesEnAlerte: number;

    valeurStockXaf: number;

    vehiculesTotal: number;

    vehiculesAlertes: number;

    depensesVehiculesMoisXaf: number;

    employesTotal: number;

    employesActifs: number;

    facturesTotal: number;

    facturesEnRetard: number;

    chiffreAffairesMoisXaf: number;
}

export interface DashboardChartItemDto {
    label: string;

    value: number;
}

export interface RecentInterventionDto {
    idIntervention: string;

    reference: string;

    titre: string;

    description?: string | null;

    statut: string;

    priorite: number;

    nomClient?: string | null;

    datePlanifiee?: string | null;

    dateCreation: string;
}

export interface ArticleCritiqueDto {
    idArticle: string;

    nom: string;

    reference?: string | null;

    unite: string;

    stockActuel: number;

    stockMinimum: number;

    prixUnitaireXaf?: number | null;
}

export interface VehiculeAlerteDto {
    idVehicule: string;

    immatriculation: string;

    marque: string;

    modele: string;

    statut: string;

    assuranceExpiration?: string | null;

    vignetteExpiration?: string | null;

    visiteTechniqueExpiration?: string | null;
}

export interface RecentDepenseVehiculeDto {
    idDepense: string;

    idVehicule: string;

    typeDepense: string;

    montantXaf: number;

    dateDepense: string;

    description?: string | null;

    vehiculeImmatriculation?: string | null;
}

export interface FactureAlerteDto {
    idFacture: string;

    reference: string;

    nomClient: string;

    statut: string;

    totalXaf: number;

    dateEcheance?: string | null;
}



// Nouvelle version donc nouveaux modèles

export interface DashboardDto {
  kpis: KpiDto[];
  alerts: AlertDto[];
}

export interface KpiDto {
  label: string;
  color: string;
  value: string;
  icon: string;
  variation: string;
  description: string;
}

export interface AlertDto {
  nom: string;
  libelle: string;
  description: string;
  icon1: string;
  icon2: string;
}


@Injectable({
    providedIn: 'root'
})
export class DashboardService {
    private readonly http = inject(HttpClient);

    private readonly baseUrl = 'https://localhost:7118/api';

    getGlobalDashboard(): Observable<GlobalDashboardDto> {
        return this.http.get<GlobalDashboardDto>(
            `${this.baseUrl}/Dashboard/global`
        );
    }

    getTmpDashboard(): Observable<DashboardDto> {
        return this.http.get<DashboardDto>(
            `${this.baseUrl}/Dashboard/temp`
        );
    }
}
