export type Role =
  | 'ADMIN'
  | 'GESTIONNAIRE'
  | 'COMPTABLE'
  | 'CAISSIER'
  | 'LECTEUR';

export interface LoginRequest {
  identifiant: string;
  motDePasse: string;
  tokenFcm?: string | null;
}

export interface AuthUser {
  idUtilisateur: string;
  idLocataire: string;
  nomComplet: string;
  email?: string | null;
  telephone: string;
  role: Role;
}
