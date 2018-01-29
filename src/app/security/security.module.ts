import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RegistrateComponent } from './registrate/registrate.component';
import { RouterModule } from '@angular/router';
import { EmailValidatorDirective } from './directives/email-validator.directive';
import { FocusDirective } from './directives/focus.directive';
import { LoginComponent } from './login/login.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: 'registrate', component: RegistrateComponent},
      { path: 'login', component: LoginComponent},
      ]),
    SharedModule
  ],
  declarations: [RegistrateComponent, EmailValidatorDirective, FocusDirective, LoginComponent],
})
export class SecurityModule { }
