import { Injectable } from '@angular/core';

@Injectable()
export class SettingService {
    _apiURI: string;

    constructor() {
       this._apiURI = 'http://localhost:5000/api';
    }

    getApiURI() {
        return this._apiURI;
    }
}
