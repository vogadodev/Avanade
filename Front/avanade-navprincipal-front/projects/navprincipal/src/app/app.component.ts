import { Component, ViewChild } from '@angular/core';
import { assetUrl } from '../single-spa/asset-url';
import { LoginModalComponent } from './components/login-modal/login-modal.component';

@Component({
  selector: 'avanade-navprincipal-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  @ViewChild(LoginModalComponent) loginModal!: LoginModalComponent;

  title = 'navprincipal';
  constructor() {}
  avanadeLogo = assetUrl('avanade-logo.png');

  onSearch(searchTerm: string): void {
    if (searchTerm?.trim()) {
      console.log('Buscando por:', searchTerm);
      // A lógica de navegação real para /busca seria implementada no AppComponent ou serviço de roteamento principal
      alert(`Simulando busca por: ${searchTerm}`);
    }
  }

  openLoginModal(): void {
    // Chama o método 'open' que criamos no filho
    this.loginModal.open();
  }
}
