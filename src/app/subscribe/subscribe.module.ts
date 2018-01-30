import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, CanActivate } from '@angular/router';

import { AuthGuard } from '../shared/utils/auth.guard';
import { SubscriptionComponent } from './subscription/subscription.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: 'activity', component: SubscriptionComponent, canActivate: [AuthGuard],
      }]),
    SharedModule
  ],
  declarations: [ SubscriptionComponent ],
  providers: [ AuthGuard ]
})
export class SubscribeModule { }
