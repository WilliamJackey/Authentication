import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CredentialsService } from './services/credentials.service';
import { SettingService } from './utils/setting.service';
import { SubscribeService } from './services/subscribe.service';

@NgModule({
  imports: [ CommonModule ],
  providers: [ CredentialsService, SettingService, SubscribeService ]
})

export class SharedModule { }
