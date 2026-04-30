import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";

export interface Intervention {
  idIntervention?: string;
  idLocal?: string;
  idLocataire?: string;
  reference?: string;
  titre?: string;
  description?: string;
  type?: string;
  priorite?: number;
  statut?: string;
  nomClient?: string;
  adresse?: string;
  latitude?: number;
  longitude?: number;
  datePlanifiee?: string;
  dateDebut?: string;
  dateFin?: string;
  dateValidation?: string;
  idValidateur?: string;
  urlSignature?: string;
  notes?: string;
  idCreateur?: string;
  dateCreation?: string;
  dateModification?: string;
  estSupprime?: boolean;
  statutIntervention?: RefStatutIntervention;
}

export interface AffectationIntervention {
  idAffectation?: string;
  idIntervention?: string;
  idTechnicien?: string;
  dateAffectation?: string;
  estPrincipal?: boolean;
}


export interface RefStatutIntervention {
    code: string;
    libelle: string;
    ordre: number;
}

@Injectable()
export class InterventionService extends ApiService<Intervention> {

    constructor() {
        super('Interventions');
    }

    CreateAffectation(Affectations: AffectationIntervention[]): Observable<AffectationIntervention[]> {
        return this.httpClient.post<AffectationIntervention[]>(`${this.baseUrl}/Interventions/CreateAffectation`, Affectations);
    }

    GetAffectations(idIntervention: string): Observable<AffectationIntervention[]> {
        return this.httpClient.get<AffectationIntervention[]>(`${this.baseUrl}/Interventions/GetAffectations/${idIntervention}`);
    }

    RemoveAffectation(affectations: AffectationIntervention): Observable<void> {
        return this.httpClient.post<void>(`${this.baseUrl}/Interventions/RemoveAffectation`, affectations);
    }

    UpdateStatutIntervention(idIntervention: string, statutIntervention: string): Observable<AffectationIntervention[]> {
        return this.httpClient.put<AffectationIntervention[]>(`${this.baseUrl}/Interventions/UpdateStatutIntervention/${idIntervention}?statutIntervention=${statutIntervention}`, null);
    }

}
