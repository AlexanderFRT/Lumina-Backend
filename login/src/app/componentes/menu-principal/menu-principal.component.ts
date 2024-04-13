import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu-principal',
  standalone: true,
  imports: [],
  templateUrl: './menu-principal.component.html',
  styleUrl: './menu-principal.component.css'
})
export class MenuPrincipalComponent {
  username: string = '';
  


  constructor (private router: Router){}

  ngOnInit() {
    // Access localStorage here:
    const username = localStorage.getItem('username');
    if (username) {
      this.username = username;
    } else {
      return
    }
  }

  salir() {
    localStorage.clear();
    this.router.navigate(['']);
  }
}
