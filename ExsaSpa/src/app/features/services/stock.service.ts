import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { MouvementStock } from "./mouvement-stock.service";


export interface ArticleStock {
  idArticle?: string;
  idLocataire?: string | null;

  nom?: string;
  reference?: string | null;
  unite?: string;

  stockActuel?: number;
  stockMinimum?: number;
  prixUnitaire?: number | null;

  dateCreation?: string | Date | null;
  dateModification?: string | Date | null;
  mouvementStocks?: MouvementStock[];
}

@Injectable({
  providedIn: 'root',
})
export class ArticleStockService extends ApiService<ArticleStock> {
  constructor() {
    super('ArticleStock');
  }
}
