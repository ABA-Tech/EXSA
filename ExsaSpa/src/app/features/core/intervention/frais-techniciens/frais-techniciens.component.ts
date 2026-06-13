import { PathDepenseInterventionDto } from './../../../services/intervention.service';
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, input, Output, output, signal, SimpleChanges } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { DatePickerModule } from 'primeng/datepicker';
import { AffectationIntervention, Intervention, InterventionService, SaisieDepense } from '@/app/features/services/intervention.service';
import { Employee, ReferentielService } from '@/app/features/services/employe.service';
import { CardModule } from 'primeng/card';
import { ChipModule } from 'primeng/chip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DataViewModule } from 'primeng/dataview';
import { DialogModule } from 'primeng/dialog';
import { FieldsetModule } from 'primeng/fieldset';
import { FileUploadModule } from 'primeng/fileupload';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { PickListModule } from 'primeng/picklist';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { SelectModule } from 'primeng/select';
import { StepperModule } from 'primeng/stepper';
import { TagModule } from 'primeng/tag';
import { TextareaModule } from 'primeng/textarea';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { PanelModule } from 'primeng/panel';
import { Checkbox } from "primeng/checkbox";
import { MultiSelectModule } from 'primeng/multiselect';
import { FluidModule } from 'primeng/fluid';
import { MessageService } from 'primeng/api';
import { CelluleRationTransportDto, JourRationTransportDto, RationTransportGridDto, SousTotalTechnicienDto } from '@/app/features/models/RationTransportGridDto';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

interface ExpenseEntry {
  ration: number | null;
  transport: number | null;
}
interface FormDepenseIntervention {
  date: string;
  ration: number | null;
  transport: number | null;
}

interface SaisieDepenseIntervention {
  idAffectation?: string;
  nomTechnicien?: string;
  role?: string;
  date?: string;
  ration?: number | null;
  transport?: number | null;
  totalJournalier?: number;
  abscent?: boolean;
}

@Component({
  selector: 'app-frais-techniciens',
  //standalone: true,

  imports: [
    CommonModule,
    TableModule,
    FormsModule,
    ButtonModule,
    RippleModule,
    ToastModule,
    ToolbarModule,
    RatingModule,
    InputTextModule,
    TextareaModule,
    SelectModule,
    RadioButtonModule,
    InputNumberModule,
    ReactiveFormsModule,
    DialogModule,
    TagModule,
    InputIconModule,
    IconFieldModule,
    DatePickerModule,
    ConfirmDialogModule,
    FieldsetModule,
    StepperModule,
    PickListModule,
    CardModule,
    ChipModule,
    FileUploadModule,
    PanelModule,
    DataViewModule,
    MultiSelectModule,
    FluidModule
],
  templateUrl: './frais-techniciens.component.html',
  styleUrl: './frais-techniciens.component.scss'
})
export class FraisTechniciensComponent {
  selectedDate: Date | null = new Date(2026, 4, 2); // 02/05/2026

    grilleDepenses = signal<RationTransportGridDto>({});
    intervention = input.required<Intervention>();
    techniciens = input<AffectationIntervention[]>();
    typeDepense = signal<AffectationIntervention[]>([]);
    jours = input<Date[]>([]);
    readonly = input<boolean>(false);

    refreshIntervention = output<Intervention>();

    submitted: boolean =false;
    isFormulaire: boolean =false;

    nouvelleLigneDepense: FormDepenseIntervention = {
        date: '',
        ration: 0,
        transport: 0
    };
    saisieDepense: SaisieDepense = {}

    nouvelleSaisieJournaliere= signal<SaisieDepenseIntervention[]>([]);


    saisieCellule$ = new Subject<CelluleRationTransportDto>();
    saisieCelluleTypeDepense:string = "";


    data!: RationTransportGridDto;

    @Output() saveCell = new EventEmitter<RationTransportGridDto>();
    @Output() cellChange = new EventEmitter<CelluleRationTransportDto>();

    sousTotalTechMap = new Map<string, SousTotalTechnicienDto>();




  constructor(private referentielService: ReferentielService,
            private interventionService: InterventionService,
            private messageService: MessageService
  ) {
  }

  ngOnInit() {
    this.loadGridDepense();
    this.referentielService.getDataFromEndpoint("GetTypesDepenseInterventions").subscribe(x=>{
        this.typeDepense.set(x)
    })

    this.saisieCellule$
        .pipe(
            debounceTime(800) // attend 800ms après la dernière frappe
        ).subscribe((cell)=>{
            this.enregistrerCellule(cell);
        })
  }





  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.rebuildSousTotalTechMap();
    }
  }





  addDepense() {
    this.isFormulaire = true;
  }

  private amount(value: number | null | undefined): number {
    return value ?? 0;
  }

  private toKey(date: Date): string {
    const year = date.getFullYear();
    const month = `${date.getMonth() + 1}`.padStart(2, '0');
    const day = `${date.getDate()}`.padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  private capitalize(value: string): string {
    return value.charAt(0).toUpperCase() + value.slice(1);
  }

  propagerNouvelleLigne(): void {
        if (!this.nouvelleLigneDepense.date) {
            return;
        }

        var datas = this.techniciens()
        var saisieJournaliere = []
        if(datas && datas.length > 0) {
            for (const technician of datas) {
                saisieJournaliere.push({
                    idAffectation: technician.idAffectation,
                    nomTechnicien: technician.technicien?.nomComplet ?? 'Inconnu',
                    role: technician.technicien?.role ?? 'Inconnu',
                    date: this.nouvelleLigneDepense.date,
                    ration: this.nouvelleLigneDepense.ration,
                    transport: this.nouvelleLigneDepense.transport,
                    totalJournalier: (this.nouvelleLigneDepense.ration ?? 0) + (this.nouvelleLigneDepense.transport ?? 0)
                });
            }

            this.nouvelleSaisieJournaliere.set(saisieJournaliere);
        }
    }


    reset(): void {
        this.submitted = false;
        this.saisieDepense = {};
        this.isFormulaire = false;
    }

    loadDepensesIntervention() {

    }

    enregistrerNouvelleSaisie() {
        this.submitted = true;
        if(this.saisieDepense.date
            && this.saisieDepense.montant
            && this.saisieDepense.techniciens
            && this.saisieDepense.reference
            && this.saisieDepense.typeDepenseIntervention
        ) {
            const date = new Date(this.saisieDepense.date);
            date.setMinutes(
                date.getMinutes() - date.getTimezoneOffset()
            );
            this.saisieDepense.date = date.toISOString().split('T')[0];
            this.saisieDepense.typeDepense = this.saisieDepense.typeDepenseIntervention?.code
            this.saisieDepense.idIntervention = this.intervention().idIntervention;

            this.interventionService.CreateDepenseIntervention(this.saisieDepense).subscribe({
                next: (res) => {
                    if(res) {
                        this.messageService.add({ severity: 'success', summary: 'Succès', detail: 'Enregistré avec succès.' });
                        this.loadDepensesIntervention();
                        this.loadGridDepense();
                        this.isFormulaire = false;
                    } else {
                        this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Erreur lors de l\'enregistrement; Attention aux doublons.' });
                    }
                },
                error: (err) => {
                    this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Erreur lors de l\'enregistrement; Attention aux doublons.' });
                }
            })
        }
    }

    loadGridDepense() {
        this.interventionService.ChargerGrilleDepenses(this.intervention().idIntervention!)
            .subscribe(x=>{
                this.grilleDepenses.set(x);
            })
    }




    onMontantChange(cell: CelluleRationTransportDto, typeDepense:string): void {
        this.saisieCelluleTypeDepense = typeDepense;
        this.saisieCellule$.next(cell)
        // this.recalculateTotals();
        // this.cellChange.emit(cell);
    }

    getSousTotalTech(idEmploye: string): SousTotalTechnicienDto | undefined {
        return this.grilleDepenses().sousTotauxTechniciens?.find((t) => t.idEmploye === idEmploye);
    }

    onSave(): void {
        this.saveCell.emit(this.data);
    }

    trackByDate(_: number, jour: JourRationTransportDto): string {
        return jour.date;
    }

    trackByCell(_: number, cell: CelluleRationTransportDto): string {
        return cell.idEmploye;
    }

    trackByTech(_: number, tech: { idEmploye: string }): string {
        return tech.idEmploye;
    }

    private rebuildSousTotalTechMap(): void {
        this.sousTotalTechMap.clear();

        for (const total of this.data.sousTotauxTechniciens ?? []) {
        this.sousTotalTechMap.set(total.idEmploye, total);
        }
    }


    enregistrerCellule(cell: CelluleRationTransportDto) {
        var pathDepense: PathDepenseInterventionDto = {
            idDepense :  this.saisieCelluleTypeDepense == 'RATION' ? cell.idDepenseRation : cell.idDepenseTransport,
            idTechnicien : cell.idEmploye,
            montant : (this.saisieCelluleTypeDepense == 'RATION' ?  cell.montantRation : cell.montantTransport) ?? 0,
            typeDepense: this.saisieCelluleTypeDepense,
        };

        if (!pathDepense.idDepense) {
            pathDepense.idDepense = cell.idDepenseRation ?? cell.idDepenseTransport;
        }

        this.interventionService.PathDepenseIntervention(this.intervention().idIntervention!, pathDepense)
                .subscribe({
                    next: (res)=>{
                    if(res) {
                        this.messageService.add({ severity: 'success', summary: 'Succès', detail: 'Enregistré avec succès.' });
                        this.loadGridDepense();
                    } else {
                        this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Erreur lors de l\'enregistrement;' });
                    }
                },
            error: (x)=>{
                    this.messageService.add({ severity: 'error', summary: 'Erreur', detail: 'Erreur lors de l\'enregistrement;\n Merci d\'utiliser le formulaire pour cette saisie' });
            }})
    }
}
