export interface RationTransportGridDto {
  techniciens?: TechnicienColumnDto[];
  jours?: JourRationTransportDto[];
  sousTotauxTechniciens?: SousTotalTechnicienDto[];

  grandTotal?: number;
  devise?: string;
}

export interface TechnicienColumnDto {
  idEmploye: string;
  nomComplet: string;
  numeroEmploye: string;
  estPrincipal: boolean;

  initiales: string;
}

export interface JourRationTransportDto {
  date: string; // ISO date string: "2025-05-04T00:00:00"
  libelleJour: string; // "Lun", "Mar", etc.
  libelleDate: string; // "4 mai", "5 mai", etc.

  cellules: CelluleRationTransportDto[];

  sousTotalRationsJour: number;
  sousTotalTransportJour: number;
  sousTotalJour: number;
}

export interface CelluleRationTransportDto {
  idEmploye: string;

  montantRation: number | null;
  montantTransport: number | null;

  idDepenseRation: string | null;
  idDepenseTransport: string | null;

  totalCase: number;

  hasRation?: boolean;
  hasTransport?: boolean;
}

export interface SousTotalTechnicienDto {
  idEmploye: string;

  sousTotalRations: number;
  sousTotalTransport: number;
  sousTotal: number;

  nbJoursAvecRation: number;
  nbJoursAvecTransport: number;
}
