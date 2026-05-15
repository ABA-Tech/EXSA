import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { Intervention } from './intervention.service';

export interface Vehicule {
    idVehicule?: string;
    idLocataire?: string;

    immatriculation?: string;
    marque?: string;
    modele?: string;
    annee?: number;
    typeVehicule?: string;

    couleur?: string;
    kilometrageActuel: number;

    dateAcquisition?: Date;

    prixAcquisitionXaf?: number;

    assuranceCompagnie?: string;
    assuranceNumero?: string;

    assuranceExpiration?: Date;
    vignetteExpiration?: Date;
    visiteTechniqueExpiration?: Date | null;

    statut?: string;

    urlPhoto?: string;
    notes?: string;

    idCreateur?: string;

    dateCreation?: Date;
    dateModification?: Date;

    estSupprime?: boolean;
    fichierPhoto?: File;
}

export interface DepenseVehicule {

    idDepense?: string;

    idLocataire?: string;

    idVehicule?: string;

    idIntervention?: string | null;

    idSaisiePar?: string;

    typeDepense?: string;

    montantXaf?: number;

    dateDepense?: Date | string;

    description?: string;

    kilometrageAuMoment?: number | null;

    urlJustificatif?: string;

    dateCreation?: Date | string;

    dateModification?: Date | string | null;

    /**
     * Upload fichier justificatif
     */
    fichierJustificatif?: File;
    vehicule?: Vehicule;
    intervention?: Intervention;
}

@Injectable({
    providedIn: 'root'
})
export class VehiculeService extends ApiService<Vehicule> {

    constructor() {
        super('Vehicules');
    }

    getFormData(vehicule:Vehicule) {

        const formData = new FormData();

        if (vehicule.idVehicule) {
            formData.append('idVehicule', vehicule.idVehicule);
        }

        if (vehicule.idLocataire) {
            formData.append('idLocataire', vehicule.idLocataire);
        }

        if (vehicule.immatriculation) {
            formData.append('immatriculation', vehicule.immatriculation);
        }

        if (vehicule.marque) {
            formData.append('marque', vehicule.marque);
        }

        if (vehicule.modele) {
            formData.append('modele', vehicule.modele);
        }

        if (vehicule.annee !== undefined && vehicule.annee !== null) {
            formData.append('annee', vehicule.annee.toString());
        }

        if (vehicule.typeVehicule) {
            formData.append('typeVehicule', vehicule.typeVehicule);
        }

        if (vehicule.couleur) {
            formData.append('couleur', vehicule.couleur);
        }

        if (
            vehicule.kilometrageActuel !== undefined &&
            vehicule.kilometrageActuel !== null
        ) {
            formData.append(
                'kilometrageActuel',
                vehicule.kilometrageActuel.toString()
            );
        }

        if (vehicule.dateAcquisition) {
            formData.append(
                'dateAcquisition',
                this.formatDate(
                    new Date(vehicule.dateAcquisition)
                )
            );
        }

        if (
            vehicule.prixAcquisitionXaf !== undefined &&
            vehicule.prixAcquisitionXaf !== null
        ) {
            formData.append(
                'prixAcquisitionXaf',
                vehicule.prixAcquisitionXaf.toString()
            );
        }

        if (vehicule.assuranceCompagnie) {
            formData.append(
                'assuranceCompagnie',
                vehicule.assuranceCompagnie
            );
        }

        if (vehicule.assuranceNumero) {
            formData.append(
                'assuranceNumero',
                vehicule.assuranceNumero
            );
        }

        if (vehicule.assuranceExpiration) {
            formData.append(
                'assuranceExpiration',
                this.formatDate(
                    new Date(vehicule.assuranceExpiration)
                )
            );
        }

        if (vehicule.vignetteExpiration) {
            formData.append(
                'vignetteExpiration',
                this.formatDate(
                    new Date(vehicule.vignetteExpiration)
                )
            );
        }

        if (vehicule.visiteTechniqueExpiration) {
            formData.append(
                'visiteTechniqueExpiration',
                this.formatDate(
                    new Date(vehicule.visiteTechniqueExpiration)
                )
            );
        }

        if (vehicule.statut) {
            formData.append('statut', vehicule.statut);
        }

        if (vehicule.urlPhoto) {
            formData.append('urlPhoto', vehicule.urlPhoto);
        }

        if (vehicule.notes) {
            formData.append('notes', vehicule.notes);
        }

        if (vehicule.idCreateur) {
            formData.append('idCreateur', vehicule.idCreateur);
        }

        if (vehicule.idVehicule) {
            formData.append(
                'IdVehicule',
                vehicule.idVehicule.toString()
            );
        }

        if (
            vehicule.estSupprime !== undefined &&
            vehicule.estSupprime !== null
        ) {
            formData.append(
                'EstSupprime',
                vehicule.estSupprime.toString()
            );
        }

        /**
         * PHOTO
         */
            console.log(vehicule.fichierPhoto)
        if (vehicule.fichierPhoto) {
            formData.append(
                'FichierPhoto',
                vehicule.fichierPhoto
            );
        }

        return formData;
    }

    AjouterVehicule(vehicule: Vehicule) {

        return this.httpClient.post(
            `${this.baseUrl}/vehicules`,
            this.getFormData(vehicule)
        );
    }

    ModifierVehicule(id: number | string, vehicule: Vehicule) {

        return this.httpClient.put(
            `${this.baseUrl}/vehicules/${id}`,
            this.getFormData(vehicule)
        );
    }

    private formatDate(date: Date): string {

        const year = date.getFullYear();

        const month = String(date.getMonth() + 1)
            .padStart(2, '0');

        const day = String(date.getDate())
            .padStart(2, '0');

        return `${year}-${month}-${day}`;
    }

    // getTypeDepenses() {
    //     this.httpClient.get(`${this.baseUrl}/vehicules`)
    // }

}
