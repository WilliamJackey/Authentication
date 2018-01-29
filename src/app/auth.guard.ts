// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { CredentialsService } from './shared/services/credentials.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private user: CredentialsService, private router: Router) {}

  canActivate() {

    if (!this.user.isLoggedIn()) {
       this.router.navigate(['/login']);
       return false;
    }

    return true;
  }
}
