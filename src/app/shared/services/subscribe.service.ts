import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';

import { SignedUsers } from '../models/signedusers';
import { SettingService } from '../../shared/utils/setting.service';

import {BaseService} from '../../shared/services/base.service';

// tslint:disable-next-line:import-blacklist
import { Observable } from 'rxjs/Rx';

// Add the RxJS Observable operators we need in this app.
// Statics
import 'rxjs/add/observable/throw';

// Operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';
@Injectable()

export class SubscribeService extends BaseService {

  // tslint:disable-next-line:no-inferrable-types
  baseUrl: string = '';

  constructor(private http: Http, private settingService: SettingService) {
     super();
     this.baseUrl = settingService.getApiURI();
  }

  getSignedUsers(): Observable<SignedUsers> {
      const headers = new Headers();
      headers.append('Accept', 'application/json');
      headers.append('Access-Control-Allow-Headers', '*');
      headers.append('Access-Control-Allow-Origin', '*');
      headers.append('Access-Control-Allow-Methods', 'GET');
      const authToken = localStorage.getItem('auth_token');
      headers.append('Authorization', `Bearer ${authToken}`);

    return this.http.get(this.baseUrl + '/subscribe/get', {headers})
      .map(response => response.json())
      .catch(this.handleError);
  }
}
