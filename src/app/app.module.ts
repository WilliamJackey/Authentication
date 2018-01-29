import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, XHRBackend } from '@angular/http';
import { ModuleWithProviders } from '@angular/core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AuthenticateXHRBackend } from './authenticate-xhr.backend';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RegistrateComponent } from './security/registrate/registrate.component';
import { SecurityModule } from './security/security.module';
import { SharedModule } from './shared/shared.module';
import { SubscribeModule } from './subscribe/subscribe.module';
import { SettingService } from './shared/utils/setting.service';


@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      { path: 'login', component: RegistrateComponent },
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: '**', redirectTo: 'login', pathMatch: 'full' },
    ], {useHash: true}),
    SecurityModule,
    SharedModule,
    SubscribeModule
  ],
  providers: [SettingService, {
    provide: XHRBackend,
    useClass: AuthenticateXHRBackend
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
