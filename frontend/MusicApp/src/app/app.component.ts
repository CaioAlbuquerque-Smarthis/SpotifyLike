import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatGridListModule} from '@angular/material/grid-list';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatIconModule, MatButtonModule, MatGridListModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Infnet';

  constructor( private router: Router){}

  public home(){
    if (sessionStorage.getItem("user_session")){
      this.router.navigate(["/home"]);
    }
    
  }
}
