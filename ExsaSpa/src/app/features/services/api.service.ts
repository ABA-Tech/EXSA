import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export abstract class ApiService<T> {

    protected httpClient =  inject(HttpClient);
    readonly baseUrl = 'https://localhost:7118/api';

    constructor(protected endpoint:string) { }
    getAll(params?: any): Observable<T[]> {
        return this.httpClient.get<T[]>(`${this.baseUrl}/${this.endpoint}`, {
        params: this.buildParams(params)
        });
    }

    getById(id: number | string): Observable<T> {
        return this.httpClient.get<T>(`${this.baseUrl}/${this.endpoint}/${id}`);
    }

    create(data: T): Observable<T> {
        return this.httpClient.post<T>(`${this.baseUrl}/${this.endpoint}`, data);
    }

    update(id: number | string, data: T): Observable<T> {
        return this.httpClient.put<T>(`${this.baseUrl}/${this.endpoint}/${id}`, data);
    }

    delete(id: number | string): Observable<void> {
        return this.httpClient.delete<void>(`${this.baseUrl}/${this.endpoint}/${id}`);
    }

    private buildParams(params: any): HttpParams {
        let httpParams = new HttpParams();

        if (!params) return httpParams;

        Object.keys(params).forEach(key => {
        if (params[key] != null) {
            httpParams = httpParams.set(key, params[key]);
        }
        });

        return httpParams;
    }
}
