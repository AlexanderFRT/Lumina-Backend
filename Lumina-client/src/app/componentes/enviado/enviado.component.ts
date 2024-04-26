import { Component } from '@angular/core';
import { HideEmailPipe } from '../../pipes/ocultar-mail.pipe';
import { CoolLocalStorage } from '@angular-cool/storage';


@Component({
  selector: 'app-enviado',
  standalone: true,
  imports: [HideEmailPipe],
  templateUrl: './enviado.component.html',
  styleUrl: './enviado.component.css'
})
export class EnviadoComponent {
  email: string = '';


  ngOnInit() {
    // Access localStorage here:
    const email = localStorage.getItem('email');
    if (email) {
      this.email = email;
    } else {
      return
    }
  
}

}
