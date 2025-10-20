import { Component, AfterViewInit, ViewChild, ElementRef, OnInit } from '@angular/core';
import { Modal } from 'bootstrap'; // Importação Chave!
import { AuthService } from '../../services/auth.service';
import { RegisterService } from '../../services/register.service';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.scss'],
})
export class LoginModalComponent implements AfterViewInit, OnInit {
  // Pegamos o elemento #loginModal do HTML
  @ViewChild('loginModal') modalElement!: ElementRef;

  private bsModal!: Modal;
  formLogin!: FormGroup;
  constructor(private formBuilder: FormBuilder, private authService: AuthService, private registerService: RegisterService) {}

  ngOnInit(): void {
    this.formLogin = this.montarFormLogin();
  }
  ngAfterViewInit(): void {
    this.bsModal = new Modal(this.modalElement.nativeElement);
  }

  montarFormLogin(): FormGroup {
    return this.formBuilder.group({
      email: [null, [Validators.required]],
      senha: [null, [Validators.required]],
    });
  }
  // Método público para o HeaderComponent chamar
  open(): void {
    this.bsModal.show();
  }

  // Método público (ou usado pelo 'x' do modal) para fechar
  hide(): void {
    this.bsModal.hide();
  }

  onLogin(): void {
    let email = this.formLogin.get('email')?.value;
    let senha = this.formLogin.get('senha')?.value;
    console.log('Botão de login acionado');
    this.authService.login(email, senha).subscribe((user) => {
      this.hide();
    });
  }

  onRegister(): void {
    let nome = 'marcus';
    let email = 'teste@teste';
    let senha = 'senha senha';
    console.log('Botão de cadastro acionado');
    this.registerService.register(nome, email, senha);
    // .subscribe((user) => {
    //   this.hide();
    // });
  }
}
