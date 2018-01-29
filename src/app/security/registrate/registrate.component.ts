import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Registration } from '../../shared/models/registration';
import { CredentialsService } from '../../shared/services/credentials.service';

@Component({
  selector: 'app-registrate',
  templateUrl: './registrate.component.html',
  styleUrls: ['./registrate.component.scss']
})
export class RegistrateComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  // tslint:disable-next-line:no-inferrable-types
  submitted: boolean = false;

  constructor(private credentialService: CredentialsService, private router: Router) { }

  ngOnInit() {
  }

  registerUser({ value, valid }: { value: Registration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (valid) {
        this.credentialService
            .register(value.email, value.password, value.firstName, value.lastName, value.activity, value.comments)
            .finally(() => this.isRequesting = false)
            .subscribe(
              result  => {if (result) {
                  this.router.navigate(['/login'], {queryParams: {brandNew: true, email: value.email}});
              }},
              errors =>  this.errors = errors);
    }
 }


}
