// src/app/features/depenses-vehicules/services/depense-vehicule.service.ts

import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { DepenseVehicule } from './vehicule.service';

export interface PageResponse<T> {
  content: T[];
  totalElements: number;
  page?: number;
  size?: number;
}

export interface DepenseVehiculeQuery {
  page?: number;
  size?: number;
  sort?: string;
  search?: string;
  idLocataire?: string;
  idVehicule?: string;
  typeDepense?: string;
}

@Injectable({
  providedIn: 'root',
})
export class DepenseVehiculeService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'https://localhost:7118/api';

  private readonly baseUrl = `${this.apiUrl}/Vehicules`;

  findAll(query: DepenseVehiculeQuery = {}): Observable<PageResponse<DepenseVehicule>> {
    let params = new HttpParams();

    Object.entries(query).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        params = params.set(key, String(value));
      }
    });

    return this.http
      .get<PageResponse<DepenseVehicule> | DepenseVehicule[]>(`${this.baseUrl}/depenses`, { params })
      .pipe(
        map((response) => {
          // Compatible avec API paginée ou API qui retourne simplement un tableau.
          if (Array.isArray(response)) {
            return {
              content: response,
              totalElements: response.length,
            };
          }

          return response;
        })
      );
  }

  findById(idDepense: string): Observable<DepenseVehicule> {
    return this.http.get<DepenseVehicule>(`${this.baseUrl}/${idDepense}`);
  }

  create(depense: DepenseVehicule): Observable<DepenseVehicule> {

    return this.http.post<DepenseVehicule>(`${this.baseUrl}/createDepenses`, this.toFormData(depense));
  }

  update(idDepense: string, depense: DepenseVehicule): Observable<DepenseVehicule> {
    return this.http.put<DepenseVehicule>(
      `${this.baseUrl}/depenses/${idDepense}`,
      this.toFormData(depense)
    );
  }

  delete(idDepense: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${idDepense}`);
  }

  private toFormData(depense: DepenseVehicule): FormData {
    const formData = new FormData();

    this.appendIfDefined(formData, 'idDepense', depense.idDepense);
    this.appendIfDefined(formData, 'idLocataire', depense.idLocataire);
    this.appendIfDefined(formData, 'idVehicule', depense.idVehicule);
    this.appendIfDefined(formData, 'idIntervention', depense.idIntervention);
    this.appendIfDefined(formData, 'idSaisiePar', depense.idSaisiePar);
    this.appendIfDefined(formData, 'typeDepense', depense.typeDepense);
    this.appendIfDefined(formData, 'montantXaf', depense.montantXaf);
    this.appendIfDefined(formData, 'description', depense.description);
    this.appendIfDefined(formData, 'kilometrageAuMoment', depense.kilometrageAuMoment);
    this.appendIfDefined(formData, 'urlJustificatif', depense.urlJustificatif);

    if (depense.dateDepense) {
      formData.append('dateDepense', this.formatDateForApi(depense.dateDepense));
    }

    if (depense.fichierJustificatif) {
      formData.append(
        'fichierJustificatif',
        depense.fichierJustificatif,
        depense.fichierJustificatif.name
      );
    }

    return formData;
  }

  private appendIfDefined(formData: FormData, key: string, value: unknown): void {
    if (value !== undefined && value !== null && value !== '') {
      formData.append(key, String(value));
    }
  }

  private formatDateForApi(value: Date | string): string {
    if (typeof value === 'string') {
      return value;
    }

    const year = value.getFullYear();
    const month = String(value.getMonth() + 1).padStart(2, '0');
    const day = String(value.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }
}
