import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";
import { Utilisateur } from "./employe.service";
import { RationTransportGridDto } from "../models/RationTransportGridDto";

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
  technicien?: Utilisateur;
}

export interface RefStatutIntervention {
    code: string;
    libelle: string;
    ordre: number;
}

export interface UploadPhoto {
    files?: File[];
    typePhoto?: string;
    IdIntervention?: string;
    datePrise?: string;
}

export interface PhotoIntervention {
  idPhoto?: string;
  idIntervention?: string;
  urlBlob?: string;
  typePhoto?: string;
  datePrise?: string;
  idUploadeur?: string;
  latitude?: number;
  longitude?: number;
  utilisateur?: Utilisateur;
}

export interface TypeDepenseIntervention {
    code?: string;
    libelle?: string;
}

export interface SaisieDepense{
  idIntervention?: string;
  techniciens?: AffectationIntervention[];
  date?: string;
  typeDepenseIntervention?: TypeDepenseIntervention;
  typeDepense?: string;
  montant?: number | null;
  reference?:string;
}

export interface PathDepenseInterventionDto {
  montant?: number | null;
  typeDepense?: string;
  date?: Date;
  note?: string;
  idTechnicien?: string;
  idDepense: string | null;
}

@Injectable()
export class InterventionService extends ApiService<Intervention> {

    constructor() {
        super('Interventions');
    }

    CreateDepenseIntervention(depense: SaisieDepense): Observable<boolean[]> {
        return this.httpClient.post<boolean[]>(`${this.baseUrl}/Interventions/SaisieDepenseIntervention`, depense);
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

    UploadInterventionPhoto(uploadPhoto: UploadPhoto): Observable<void> {
        const formData = new FormData();
        uploadPhoto.files?.forEach(file => {
            formData.append('Files', file);
        });

        // Ajouter les autres champs
        formData.append('IdIntervention', uploadPhoto.IdIntervention ?? "");

        formData.append('DatePrise', uploadPhoto.datePrise ?? "");
        formData.append('TypePhoto', uploadPhoto?.typePhoto ?? "");
        return this.httpClient.post<void>(`${this.baseUrl}/Interventions/uploadPhotos`, formData);
    }

    GetPhotosIntervention(idIntervention: string): Observable<PhotoIntervention[]> {
        return this.httpClient.get<PhotoIntervention[]>(`${this.baseUrl}/Interventions/GetPhotosIntervention/${idIntervention}`);
    }

    DeletePhoto(idPhoto: string): Observable<any> {
        return this.httpClient.delete<any>(`${this.baseUrl}/Interventions/RemovePhoto/${idPhoto}`);
    }

    ChargerGrilleDepenses(idIntervention: string): Observable<RationTransportGridDto> {
        return this.httpClient.get<RationTransportGridDto>(`${this.baseUrl}/Interventions/GetGrilleRationTransport/${idIntervention}`);
    }

    PathDepenseIntervention(idDepenseIntervention: string, depenseIntervention: any): Observable<boolean> {
        return this.httpClient.patch<boolean>(`${this.baseUrl}/Interventions/PatchDepenseIntervention/${idDepenseIntervention}`, depenseIntervention);
    }
}
