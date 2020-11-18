import { Component, Inject, OnInit } from '@angular/core';
import { baseUrl } from './_core/constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(@Inject(baseUrl)private _baseUrl:string) {

  }
  ngOnInit(): void {
    var source = new EventSource(`${this._baseUrl}api/events`);

    source.onmessage = function (event) {
      console.log('onmessage: ' + event.data);
    };

    source.onopen = function(event) {
      console.log('onopen');
    };

    source.onerror = function(event) {
        console.log('onerror');
        console.log(event);
    }
  }

}
