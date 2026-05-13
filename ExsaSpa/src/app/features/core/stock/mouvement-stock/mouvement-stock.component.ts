import { Intervention, InterventionService } from '@/app/features/services/intervention.service';
import { MouvementStock, MouvementStockService } from '@/app/features/services/mouvement-stock.service';
import { ArticleStock } from '@/app/features/services/stock.service';
import { CommonModule } from '@angular/common';
import { Component, inject, Input, input, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SelectModule } from 'primeng/select';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToolbarModule } from 'primeng/toolbar';

@Component({
  selector: 'app-mouvementStock',
  templateUrl: './mouvement-stock.component.html',
  styleUrls: ['./mouvement-stock.component.scss'],
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
    MultiSelectModule
  ],
  providers: [InterventionService]
})
export class MouvementStockComponent implements OnInit {

    mvtStockService = inject(MouvementStockService);
    interventionService = inject(InterventionService);
    typeMvtStock = input.required<any[]>();
    interventionList = signal<Intervention[]>([]);
    articleList = input.required<ArticleStock[]>();
    @Input() ouvrirDialog:boolean = false;
    submitted: boolean = false;
    @Input() mvtStock: MouvementStock = {};

    constructor() { }

    ngOnInit() {
        this.interventionService.getAll({status:'EN_COURS'}).subscribe(res=>{
            this.interventionList.set(res.map(x=>({
                ...x,
                titre: `${x.reference} - ${x.titre}`
            })));
        })
    }

    hideDialog() {
        this.ouvrirDialog = false;
        this.mvtStock = {};
    }
    enregistrerMvt() {

        if (this.mvtStock.articleNavigation) this.mvtStock.idArticle = this.mvtStock.articleNavigation.idArticle;
        if (!this.mvtStock.idArticle || !this.mvtStock.quantite || !this.mvtStock.dateMouvement || !this.mvtStock.typeMouvement)
        {
            this.submitted = true;
            return;
        }

        if(!this.mvtStock.idMouvement) {
            this.mvtStockService.create(this.mvtStock)
                .subscribe({
                    next: (res)=>{

                    },
                    error: (err) => {
                        console.log(err)
                    }
                })
        }else {
            this.mvtStock.idIntervention = null;
            this.mvtStockService.update(this.mvtStock.idMouvement, this.mvtStock)
                .subscribe({
                    next: (res)=>{

                    },
                    error: (err) => {
                        console.log(err)
                    }
                })
        }
    }

    getIcone(code:string) {
        if(code === 'ENTREE') return 'down'
        else return 'up'
    }
}
