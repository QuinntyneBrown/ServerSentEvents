import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  ngOnInit(): void {
    var source = new EventSource('https://localhost:44346/api/events');

    source.onmessage = function (event) {
      console.log('onmessage: ' + event.data);
    };

  }

}
