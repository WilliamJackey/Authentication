import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { CredentialsService } from './services/credentials.service';
import { SettingService } from './utils/setting.service';
import { SubscribeService } from './services/subscribe.service';
import { SpinnerComponent } from './spinner/spinner.component';

@NgModule({
  imports: [ CommonModule ],
  providers: [ CredentialsService, SettingService, SubscribeService ],
  declarations: [ SpinnerComponent ],
  exports: [ SpinnerComponent ]
})

export class SharedModule { }
