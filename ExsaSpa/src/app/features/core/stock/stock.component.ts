import { Component, inject, OnInit, signal } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SelectModule } from 'primeng/select';
import { Table, TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ArticleStock, ArticleStockService } from '../../services/stock.service';
import { DialogModule } from 'primeng/dialog';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TagModule } from 'primeng/tag';
import { Observable } from 'rxjs';
import { ConfirmationService } from 'primeng/api';
import { ProgressBarModule } from 'primeng/progressbar';
import { ReferentielService } from '../../services/employe.service';
import { MouvementStockComponent } from "./mouvement-stock/mouvement-stock.component";
import { HistoriqueStockComponent } from "./historique-stock/historique-stock.component";
import { MouvementStock, MouvementStockService } from '../../services/mouvement-stock.service';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.scss'],
  imports: [
    CommonModule,
    ButtonModule,
    ToolbarModule,
    TableModule,
    IconFieldModule,
    InputIconModule,
    RadioButtonModule,
    InputTextModule,
    InputNumberModule,
    DatePickerModule,
    SelectModule,
    DialogModule,
    ConfirmDialogModule,
    FormsModule,
    TagModule,
    ProgressBarModule,
    MouvementStockComponent,
    HistoriqueStockComponent
],
  providers: [ConfirmationService, ReferentielService]
})
export class StockComponent implements OnInit {
    articleStockService = inject(ArticleStockService);
    referentielService = inject(ReferentielService);
    mouvementStockService = inject(MouvementStockService);
    article!: ArticleStock;
    ouvrirDialog:boolean = false;
    editerMvt:boolean = false;
    submitted: boolean = false;
    articleList = signal<ArticleStock[]>([]);
    typeMvtList = signal<any[]>([]);
    showHistorique = signal<boolean>(false);
    listMvtStock = signal<MouvementStock[]>([]);

    constructor() { }

    ngOnInit() {
        this.loadArticle();
        this.loadTypeMvt();
    }

    loadArticle() {
        this.articleStockService.getAll()
            .subscribe(x=>{
                this.articleList.set(x);
                this.ouvrirDialog = false;
                this.showHistorique.set(false);
            })
    }

    loadTypeMvt() {
        this.referentielService.getDataFromEndpoint('GetTypeMouvements').subscribe(x=>{
            this.typeMvtList.set(x);
        })
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    AjoutArticle() {
        this.article = {};
        this.article.stockMinimum = 1;
        this.article.unite = '';
        this.ouvrirDialog = true;
        this.showHistorique.set(false);
    }

    hideDialog() {
        this.ouvrirDialog = false;
        this.article = {};
    }

    enregistrerArticle() {
        if(!this.article.idArticle) {
            this.articleStockService.create(this.article)
                .subscribe({
                    next: (res)=>{
                        this.loadArticle();
                    },
                    error: (err) => {
                        console.log(err)
                    }
                })
        }else {
            this.articleStockService.update(this.article.idArticle, this.article)
                .subscribe({
                    next: (res)=>{
                        this.loadArticle()
                    },
                    error: (err) => {
                        console.log(err)
                    }
                })
        }
    }

    getSeverity(status: boolean) {
        switch (status) {
            case true:
                return 'Actif';
            default:
                return 'Inactif';
        }
    }

    getStatus(status: boolean) {
        switch (status) {
            case true:
                return 'success';
            default:
                return 'info';
        }
    }

    deleteArticle(article: ArticleStock) {

    }

    editArticle(article: ArticleStock) {
        this.article = article;
        this.ouvrirDialog = true;
    }

    getPercentage(article:ArticleStock) {
        if(!article.stockMinimum || article.stockMinimum == 0) article.stockMinimum = 1;

        return (article.stockActuel!/article.stockMinimum!) * 100
    }

    getStylePercentage(article: ArticleStock) {
        var percentage = this.getPercentage(article)
        return "width: "+percentage+"%;"
    }

    getProgressColor(article:ArticleStock) {
        var percentage = this.getPercentage(article)
        if(percentage<=20) return 'red';
        else if(percentage <= 50) return 'orange';
        else if(percentage <= 60) return 'teal';
        else if(percentage <= 70) return 'cyan';
        else return 'green';
    }

    exportCSV() {
    }


    loadMvtData(article:any = null) {

        console.log(article);
        this.mouvementStockService.getAll(article ? {idArticle:article.idArticle} : {})
            .subscribe(x=>{
                this.listMvtStock.set(x)
            })

        setTimeout(()=>{
            this.showHistorique.set(true);
        }, 500)
    }
    afficherHistorique(article:ArticleStock) {
        this.article =article;
        this.showHistorique.set(false);
        console.log(article);

        this.loadMvtData(article);
    }
}
