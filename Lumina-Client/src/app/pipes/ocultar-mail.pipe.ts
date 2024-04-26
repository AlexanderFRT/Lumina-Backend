import { Pipe, PipeTransform } from '@angular/core';




@Pipe({ name: 'ocultarEmail',
standalone: true
 })
export class HideEmailPipe implements PipeTransform {
  transform(email: string): string {
    if (!email) return '';
    
    const [username, domain] = email.split('@');
    const hiddenUsername = this.hideCharacters(username, 3); // Oculta los primeros 3 caracteres del nombre de usuario
    return `${hiddenUsername}@${domain}`;
  }

  private hideCharacters(input: string, count: number): string {
    if (input.length <= count) {
      return '*'.repeat(input.length);
    } else {
      return input.substring(0, count) + '*'.repeat(input.length - count);
    }
  }
}