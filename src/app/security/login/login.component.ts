import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// tslint:disable-next-line:import-blacklist
import { Subscription } from 'rxjs';

import { Credentials } from '../../shared/models/credentials';
import { CredentialsService } from '../../shared/services/credentials.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  brandNew: boolean;
  errors: string;
  isRequesting: boolean;
  // tslint:disable-next-line:no-inferrable-types
  submitted: boolean = false;
  credentials: Credentials = { email: '', password: '' };

  constructor(private credentialsService: CredentialsService,
              private router: Router,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    // subscribe to router event
    this.subscription = this.activatedRoute.queryParams.subscribe(
      (param: any) => {
         this.brandNew = param['brandNew'];
         this.credentials.email = param['email'];
      });
  }

  ngOnDestroy() {
    // prevent memory leak by unsubscribing
    this.subscription.unsubscribe();
  }

  login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (valid) {
      this.credentialsService.login(value.email, value.password)
        .finally(() => this.isRequesting = false)
        .subscribe(
        result => {
          if (result) {
             this.router.navigate(['/subscribe']);
          }
        },
        error => this.errors = error);
    }
  }

}
