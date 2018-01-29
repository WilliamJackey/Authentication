import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AuthGuard } from '../auth.guard';
import { SubscriptionComponent } from './subscription/subscription.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: 'subscribe', component: SubscriptionComponent, canActivate: [AuthGuard]},
      ]),
    SharedModule
  ],
  declarations: [SubscriptionComponent]
})
export class SubscribeModule { }
