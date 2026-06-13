import { Injectable } from "@angular/core";
import { Intervention } from "./intervention.service";
import { ApiService } from "./api.service";

export interface Facture {
  idFacture?: string;
  idLocataire?: string;
  idIntervention?: string;
  reference?: string;
  statut?: string;
  nomClient?: string;
  sousTotalXaf?: number;
  tauxTva?: number;
  totalXaf?: number;
  dateEcheance?: Date;
  datePaiement?: Date;
  urlPdf?: string;
  dateCreation?: Date;
  dateModification?: Date;
  idInterventionNavigation?: Intervention;
}

export interface Reglement {
  idReglement?: string;
  idFacture?: string;
  montantXaf?: number;
  modeReglement?: string;
  referenceReglement?: string | null;
  dateReglement?: Date | null;
  dateCreation?: Date | null;
  dateModification?: Date | null;
}

@Injectable({
  providedIn: 'root',
})
export class FactureService extends ApiService<Facture> {
  constructor() {
    super('Factures');
  }

  getFactureByIntervention(idIntervention: string) {
    return this.httpClient.get<Facture>(`${this.baseUrl}/${this.endpoint}/intervention/${idIntervention}`);
  }

  getReglementsByFacture(idFacture: string) {
    return this.httpClient.get<Reglement[]>(`${this.baseUrl}/${this.endpoint}/${idFacture}/reglements`);
  }

  saveReglement(idFacture: string, reglement: Reglement) {
    return this.httpClient.post<Reglement>(`${this.baseUrl}/${this.endpoint}/${idFacture}/reglements`, reglement);
  }
}
