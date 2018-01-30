import { Component, OnInit, OnDestroy } from '@angular/core';

import { CredentialsService } from '../shared/services/credentials.service';

import {Subscription} from 'rxjs/Subscription';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})

export class NavBarComponent implements OnInit, OnDestroy {
  status: boolean;
  subscription: Subscription;

  constructor(private credentialsService: CredentialsService) {
   }

  logout() {
    this.credentialsService.logout();
  }

  ngOnInit() {
    this.subscription = this.credentialsService.authNavStatus$.subscribe(status => this.status = status);
  }

  ngOnDestroy() {
  // prevent memory leak when component is destroyed
  this.subscription.unsubscribe();
  }
}
