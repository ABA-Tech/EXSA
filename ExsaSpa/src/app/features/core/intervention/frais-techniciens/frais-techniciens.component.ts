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

type ExpenseKind = 'ration' | 'transport';

interface DayColumn {
  key: string;
  date: Date;
}

interface ExpenseEntry {
  ration: number | null;
  transport: number | null;
}

interface Technician {
  initials: string;
  name: string;
  entries: Record<string, ExpenseEntry>;
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

//   days: DayColumn[] = [];

//   technicians: Technician[] = [
//     {
//       initials: 'AJ',
//       name: 'Abouna Jean',
//       entries: {}
//     },
//     {
//       initials: 'MP',
//       name: 'Mbarga Paul',
//       entries: {}
//     },
//     {
//       initials: 'NS',
//       name: 'Ngo Simone',
//       entries: {}
//     }
//   ];




    data!: RationTransportGridDto;

    @Output() saveCell = new EventEmitter<RationTransportGridDto>();
    @Output() cellChange = new EventEmitter<CelluleRationTransportDto>();

    sousTotalTechMap = new Map<string, SousTotalTechnicienDto>();




  constructor(private referentielService: ReferentielService,
            private interventionService: InterventionService,
            private messageService: MessageService
  ) {
    // this.addDay();

    // const key = this.days[0].key;

    // this.technicians[0].entries[key].ration = 0;
    // this.technicians[0].entries[key].transport = null;

    // this.technicians[1].entries[key].ration = null;
    // this.technicians[1].entries[key].transport = 500;

    // this.technicians[2].entries[key].ration = 500;
    // this.technicians[2].entries[key].transport = null;
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

//   addDay(): void {
//     if (!this.selectedDate) return;

//     const key = this.toKey(this.selectedDate);

//     if (this.days.some(d => d.key === key)) {
//       return;
//     }

//     this.days.push({
//       key,
//       date: new Date(this.selectedDate)
//     });

//     this.days.sort((a, b) => a.date.getTime() - b.date.getTime());

//     for (const technician of this.technicians) {
//       technician.entries[key] ??= {
//         ration: null,
//         transport: null
//       };
//     }
//   }

//   removeDay(key: string): void {
//     this.days = this.days.filter(d => d.key !== key);

//     for (const technician of this.technicians) {
//       delete technician.entries[key];
//     }
//   }

//   totalTechnician(technician: Technician, kind?: ExpenseKind): number {
//     return Object.values(technician.entries).reduce((sum, entry) => {
//       if (kind) {
//         return sum + this.amount(entry[kind]);
//       }

//       return sum + this.amount(entry.ration) + this.amount(entry.transport);
//     }, 0);
//   }

//   totalDay(dayKey: string, kind: ExpenseKind): number {
//     return this.technicians.reduce((sum, technician) => {
//       return sum + this.amount(technician.entries[dayKey]?.[kind]);
//     }, 0);
//   }

//   totalKind(kind: ExpenseKind): number {
//     return this.technicians.reduce((sum, technician) => {
//       return sum + this.totalTechnician(technician, kind);
//     }, 0);
//   }

//   grandTotal(): number {
//     return this.totalKind('ration') + this.totalKind('transport');
//   }

//   hasData(): boolean {
//     return this.grandTotal() > 0;
//   }

//   save(): void {
//     const payload = this.technicians.map(technician => ({
//       name: technician.name,
//       entries: technician.entries
//     }));

//     console.log('Payload à envoyer API', payload);
//   }

//   formatXaf(value: number): string {
//     return `${new Intl.NumberFormat('fr-FR').format(value)} XAF`;
//   }

//   dayTitle(date: Date): string {
//     const weekday = new Intl.DateTimeFormat('fr-FR', {
//       weekday: 'short'
//     }).format(date).replace('.', '');

//     const day = new Intl.DateTimeFormat('fr-FR', {
//       day: 'numeric'
//     }).format(date);

//     const month = new Intl.DateTimeFormat('fr-FR', {
//       month: 'short'
//     }).format(date).replace('.', '');

//     return `${this.capitalize(weekday)} ${day}\n${month}`;
//   }

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
        return this.sousTotalTechMap.get(idEmploye);
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

    private recalculateTotals(): void {
        if (!this.data) return;

        for (const jour of this.data.jours!) {
        jour.sousTotalRationsJour = jour.cellules.reduce(
            (sum, cell) => sum + (cell.montantRation ?? 0),
            0
        );

        jour.sousTotalTransportJour = jour.cellules.reduce(
            (sum, cell) => sum + (cell.montantTransport ?? 0),
            0
        );

        jour.sousTotalJour = jour.sousTotalRationsJour + jour.sousTotalTransportJour;
        }

        for (const tech of this.data.techniciens!) {
        const cellulesTech = this.data.jours!
            .flatMap(jour => jour.cellules)
            .filter(cell => cell.idEmploye === tech.idEmploye);

        let total = this.sousTotalTechMap.get(tech.idEmploye);

        if (!total) {
            total = {
            idEmploye: tech.idEmploye,
            sousTotalRations: 0,
            sousTotalTransport: 0,
            sousTotal: 0,
            nbJoursAvecRation: 0,
            nbJoursAvecTransport: 0
            };

            this.data.sousTotauxTechniciens!.push(total);
            this.sousTotalTechMap.set(tech.idEmploye, total);
        }

        total.sousTotalRations = cellulesTech.reduce(
            (sum, cell) => sum + (cell.montantRation ?? 0),
            0
        );

        total.sousTotalTransport = cellulesTech.reduce(
            (sum, cell) => sum + (cell.montantTransport ?? 0),
            0
        );

        total.sousTotal = total.sousTotalRations + total.sousTotalTransport;

        total.nbJoursAvecRation = cellulesTech.filter(cell => cell.montantRation !== null).length;
        total.nbJoursAvecTransport = cellulesTech.filter(cell => cell.montantTransport !== null).length;
        }

        this.data.grandTotal = this.data.jours!.reduce(
        (sum, jour) => sum + jour.sousTotalJour,
        0
        );
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
                    console.log(res)
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
