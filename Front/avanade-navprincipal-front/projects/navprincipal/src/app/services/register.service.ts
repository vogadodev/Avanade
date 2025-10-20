import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  constructor() {}

  register(nome: string, email: string, senha: string) {
    console.log('cadastro');
  }
}
