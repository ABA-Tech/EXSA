import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-access-denied',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    CardModule,
    ButtonModule
  ],
  template: `
    <div class="access-denied-page">
      <p-card styleClass="access-card">
        <div class="content">
          <i class="pi pi-lock icon"></i>

          <h1>Accès refusé</h1>

          <p>
            Vous n'avez pas les droits nécessaires pour accéder à cette ressource.
          </p>

          <a routerLink="/dashboard">
            <p-button
              label="Retour au tableau de bord"
              icon="pi pi-arrow-left"
            ></p-button>
          </a>
        </div>
      </p-card>
    </div>
  `,
  styles: [`
    .access-denied-page {
      min-height: calc(100vh - 5rem);
      display: flex;
      align-items: center;
      justify-content: center;
      padding: 2rem;
    }

    .access-card {
      max-width: 480px;
      width: 100%;
      text-align: center;
    }

    .content {
      display: flex;
      flex-direction: column;
      align-items: center;
      gap: 1rem;
    }

    .icon {
      font-size: 3rem;
      color: #dc2626;
    }

    h1 {
      margin: 0;
      font-size: 1.75rem;
    }

    p {
      color: #64748b;
      margin: 0 0 1rem;
    }
  `]
})
export class AccessDeniedComponent {}
