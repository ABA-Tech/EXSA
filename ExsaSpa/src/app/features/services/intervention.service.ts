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
}

export interface AffectationIntervention {
  idAffectation?: string;
  idIntervention?: string;
  idTechnicien?: string;
  dateAffectation?: string;
  estPrincipal?: boolean;
}

@Injectable()
export class InterventionService extends ApiService<Intervention> {

    constructor() {
        super('Interventions');
    }

    CreateAffectation(Affectations: AffectationIntervention[]): Observable<AffectationIntervention[]> {
        return this.httpClient.post<AffectationIntervention[]>(`${this.baseUrl}/Interventions/CreateAffectation`, Affectations);
    }
}
