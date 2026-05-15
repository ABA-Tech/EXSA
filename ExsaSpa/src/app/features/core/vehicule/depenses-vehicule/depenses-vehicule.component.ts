import { map } from 'rxjs/operators';
import { DepenseVehiculeService } from '@/app/features/services/depense-vehicule.service';
import { ReferentielService } from '@/app/features/services/employe.service';
import { DepenseVehicule, Vehicule, VehiculeService } from '@/app/features/services/vehicule.service';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MessageService, ConfirmationService } from 'primeng/api';
import { ButtonModule } from "primeng/button";
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { FileUploadModule } from 'primeng/fileupload';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TextareaModule } from 'primeng/textarea';
import { ToastModule } from 'primeng/toast';
import { TooltipModule } from 'primeng/tooltip';
import { debounceTime, distinctUntilChanged, finalize } from 'rxjs';
import { Intervention, InterventionService } from '@/app/features/services/intervention.service';

@Component({
  selector: 'app-depenses-vehicule',
  templateUrl: './depenses-vehicule.component.html',
  styleUrls: ['./depenses-vehicule.component.scss'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TableModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    InputNumberModule,
    DatePickerModule,
    TextareaModule,
    SelectModule,
    FileUploadModule,
    ToastModule,
    ConfirmDialogModule,
    TagModule,
    TooltipModule,
    ],
    providers: [
        MessageService,
        ConfirmationService,
        InterventionService
    ]
})
export class DepensesVehiculeComponent implements OnInit {

  private readonly fb = inject(FormBuilder);
  private readonly depenseService = inject(DepenseVehiculeService);
  private readonly messageService = inject(MessageService);
  private readonly confirmationService = inject(ConfirmationService);
  private readonly vehiculeService = inject(VehiculeService);
  private readonly referentielService = inject(ReferentielService);
  private interventionService = inject(InterventionService);

  depenses: DepenseVehicule[] = [];
  interventionList: Intervention[] = [];
  vehicules=signal<Vehicule[]>([]);
  selectedDepense?: DepenseVehicule;

  loading = false;
  saving = false;
  submitted = false;

  dialogVisible = false;
  detailDialogVisible = false;

  mode: 'create' | 'edit' = 'create';

  first = 0;
  rows = 10;
  totalRecords = 0;

  lastLazyEvent?: TableLazyLoadEvent;

  readonly rowsPerPageOptions = [10, 20, 50, 100];

  typeDepenseOptions: any[] = [];

  readonly searchForm = this.fb.nonNullable.group({
    search: [''],
  });

  readonly form = this.fb.group({
    idDepense: [''],
    // idLocataire: ['', [Validators.required]],
    idVehicule: ['', [Validators.required]],
    idIntervention: [''],
    idSaisiePar: [''],

    typeDepense: ['', [Validators.required]],
    montantXaf: [0, [Validators.required, Validators.min(1)]],
    dateDepense: [new Date(), [Validators.required]],
    description: [''],
    kilometrageAuMoment: [null as number | null, [Validators.min(0)]],

    urlJustificatif: [''],
    DepenseVehicules: [''],
    fichierJustificatif: [null as File | null],
  });

  ngOnInit(): void {
    this.loadDepenses();
    this.searchForm.controls.search.valueChanges
      .pipe(debounceTime(400), distinctUntilChanged())
      .subscribe(() => {
        this.first = 0;
        this.loadDepenses({
          ...this.lastLazyEvent,
          first: 0,
          rows: this.rows,
        });
      });
    this.loadVehicules();
    this.loadTypeDepenses();
    this.loadInterventionsEnCours();
  }

  get dialogTitle(): string {
    return this.mode === 'create'
      ? 'Nouvelle dépense véhicule'
      : 'Modifier la dépense véhicule';
  }

  get selectedFileName(): string | null {
    return this.form.controls.fichierJustificatif.value?.name ?? null;
  }

  loadTypeDepenses() {
    this.referentielService.getDataFromEndpoint("GetTypeDepenseVehiculeAsync").subscribe({
        next: (res)=> {
            this.typeDepenseOptions = res.map((x:any)=>({
                ...x,
                label: x.icone + ' - ' + x.libelle
            }))

            console.log(res);

        }
    })
  }

  loadInterventionsEnCours() {
    this.interventionService.getAll({status:'EN_COURS'}).subscribe(res=>{
        this.interventionList = res.map(x=>({
            ...x,
            titre: `${x.reference} - ${x.titre}`
        }));
    })
  }

  loadVehicules() {
    this.vehiculeService.getAll().subscribe({
        next:(res)=>{
            this.vehicules.set(res);
        }
    })
  }

  loadDepenses(event?: TableLazyLoadEvent): void {
    this.loading = true;

    this.lastLazyEvent = event ?? this.lastLazyEvent;

    const first = event?.first ?? this.first ?? 0;
    const rows = event?.rows ?? this.rows ?? 10;

    this.first = first;
    this.rows = rows;

    const page = Math.floor(first / rows);
    const sortField = Array.isArray(event?.sortField)
      ? event?.sortField[0]
      : event?.sortField;

    const sortDirection = event?.sortOrder === -1 ? 'desc' : 'asc';

    this.depenseService
      .findAll({
        page,
        size: rows,
        sort: sortField ? `${sortField},${sortDirection}` : 'dateDepense,desc',
        search: this.searchForm.controls.search.value,
      })
      .pipe(finalize(() => (this.loading = false)))
      .subscribe({
        next: (response) => {
          this.depenses = response.content;
          this.totalRecords = response.totalElements;
        },
        error: (error) => this.handleError(error, 'Chargement impossible'),
      });
  }

  openCreateDialog(): void {
    this.mode = 'create';
    this.submitted = false;
    this.selectedDepense = undefined;

    this.form.reset({
      idDepense: '',
    //   idLocataire: '',
      idVehicule: '',
      idIntervention: '',
      idSaisiePar: '',
      typeDepense: '',
      montantXaf: 0,
      dateDepense: new Date(),
      description: '',
      kilometrageAuMoment: null,
      urlJustificatif: '',
      fichierJustificatif: null,
    });

    this.dialogVisible = true;
  }

  openEditDialog(depense: DepenseVehicule): void {
    this.mode = 'edit';
    this.submitted = false;
    this.selectedDepense = depense;

    this.form.reset({
      idDepense: depense.idDepense ?? '',
    //   idLocataire: depense.idLocataire ?? '',
      idVehicule: depense.idVehicule ?? '',
      idIntervention: depense.idIntervention ?? '',
      idSaisiePar: depense.idSaisiePar ?? '',
      typeDepense: depense.typeDepense ?? '',
      montantXaf: depense.montantXaf ?? 0,
      dateDepense: depense.dateDepense
        ? new Date(depense.dateDepense)
        : new Date(),
      description: depense.description ?? '',
      kilometrageAuMoment: depense.kilometrageAuMoment ?? null,
      urlJustificatif: depense.urlJustificatif ?? '',
      fichierJustificatif: null,
    });

    this.dialogVisible = true;
  }

  openDetailDialog(depense: DepenseVehicule): void {
    this.selectedDepense = depense;
    this.detailDialogVisible = true;
  }

  save(): void {
    this.submitted = true;

    console.log(this.form.value);

    if (this.form.invalid) {
      this.form.markAllAsTouched();

      this.messageService.add({
        severity: 'warn',
        summary: 'Formulaire incomplet',
        detail: 'Veuillez corriger les champs obligatoires.',
      });

      return;
    }

    this.saving = true;

    const payload = this.form.getRawValue() as DepenseVehicule;
    console.log(payload);

    const request$ =
      this.mode === 'create'
        ? this.depenseService.create(payload)
        : this.depenseService.update(payload.idDepense!, payload);

    request$
      .pipe(finalize(() => (this.saving = false)))
      .subscribe({
        next: () => {
          this.dialogVisible = false;

          this.messageService.add({
            severity: 'success',
            summary: 'Succès',
            detail:
              this.mode === 'create'
                ? 'Dépense enregistrée avec succès.'
                : 'Dépense mise à jour avec succès.',
          });

          this.loadDepenses(this.lastLazyEvent);
        },
        error: (error) => this.handleError(error, 'Enregistrement impossible'),
      });
  }

  confirmDelete(depense: DepenseVehicule): void {
    if (!depense.idDepense) {
      return;
    }

    this.confirmationService.confirm({
      header: 'Confirmation',
      message: `Voulez-vous vraiment supprimer cette dépense de ${this.formatXaf(
        depense.montantXaf
      )} ?`,
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Supprimer',
      rejectLabel: 'Annuler',
      acceptButtonStyleClass: 'p-button-danger',
      rejectButtonStyleClass: 'p-button-text',
      accept: () => this.delete(depense.idDepense!),
    });
  }

  private delete(idDepense: string): void {
    this.loading = true;

    this.depenseService
      .delete(idDepense)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Suppression effectuée',
            detail: 'La dépense a été supprimée.',
          });

          this.loadDepenses(this.lastLazyEvent);
        },
        error: (error) => this.handleError(error, 'Suppression impossible'),
      });
  }

  onFileSelect(event: any): void {
    const file: File | undefined =
      event?.files?.[0] ?? event?.currentFiles?.[0];

    if (!file) {
      return;
    }

    this.form.patchValue({
      fichierJustificatif: file,
    });
  }

  onFileClear(): void {
    this.form.patchValue({
      fichierJustificatif: null,
    });
  }

  isInvalid(controlName: keyof typeof this.form.controls): boolean {
    const control = this.form.controls[controlName];
    return !!control && control.invalid && (control.touched || this.submitted);
  }

  getTypeLabel(value?: string): string {
    return (
      this.typeDepenseOptions.find((item) => item.code === value)?.code ??
      value ??
      '—'
    );
  }

  getTypeSeverity(value?: string): 'success' | 'info' | 'warn' | 'danger' | 'secondary' {
    switch (value) {
      case 'CARBURANT':
        return 'info';
      case 'ENTRETIEN':
        return 'success';
      case 'REPARATION':
        return 'warn';
      case 'ASSURANCE':
        return 'secondary';
      default:
        return 'secondary';
    }
  }

  formatXaf(value?: number | null): string {
    return new Intl.NumberFormat('fr-CM', {
      style: 'currency',
      currency: 'XAF',
      maximumFractionDigits: 0,
    }).format(value ?? 0);
  }

  private handleError(error: HttpErrorResponse, fallbackMessage: string): void {
    const apiMessage =
      error.error?.message ??
      error.error?.detail ??
      error.message ??
      fallbackMessage;

    this.messageService.add({
      severity: 'error',
      summary: fallbackMessage,
      detail: apiMessage,
    });
  }

}
