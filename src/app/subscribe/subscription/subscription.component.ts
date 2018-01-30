import { Component, OnInit } from '@angular/core';

import { SignedUsers } from '../../shared/models/signedusers';
import { SubscribeService } from '../../shared/services/subscribe.service';

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.scss']
})
export class SubscriptionComponent implements OnInit {
  error: string;
  signedUsers: SignedUsers;

  constructor(private subscribeService: SubscribeService) { }

  ngOnInit() {
    this.subscribeService.getSignedUsers()
    .subscribe((signedUsers: SignedUsers) => {
      this.signedUsers = signedUsers;
    },
    error => {
      this.error = error;
    });
  }
}

