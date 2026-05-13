import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Intervention } from './intervention.service';
import { ArticleStock } from './stock.service';

export interface MouvementStock {
  idMouvement?: string;
  idArticle?: string;
  idIntervention?: string | null;

  typeMouvement?: string;
  quantite?: number;

  idOperateur?: string;
  dateMouvement?: string | Date;
  articleNavigation?: ArticleStock | null;
  intervention?: Intervention | null;
  typeMouvementNavigation?: any;
}

@Injectable({
  providedIn: 'root',
})
export class MouvementStockService extends ApiService<MouvementStock> {
  constructor() {
    super('MouvementStocks');
  }
}
