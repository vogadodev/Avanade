import { ReplaySubject } from 'rxjs';

// The single-spa-layout can put props onto your microfrontend here.
// For example, if you want to pass a user object, you'd do:
// singleSpaProps.user = myUserObject;

// We use a ReplaySubject to buffer the latest value of the props
// so that components that subscribe late still receive the latest value.
const singleSpaPropsSubject = new ReplaySubject<SingleSpaProps>(1);

// This is the default interface provided by single-spa-angular
// Extend this interface if you need custom props
export interface SingleSpaProps {
    name: string;
    // single-spa props are extensible:
    [key: string]: any; 
}

/**
 * Observable que emite as propriedades injetadas pelo single-spa (e single-spa-layout, se usado).
 * Use em qualquer componente/serviço para reagir a mudanças nas props.
 * * Exemplo de uso:
 * ```typescript
 * import { singleSpaPropsSubject } from 'src/single-spa/single-spa-props';
 * * singleSpaPropsSubject.subscribe(props => {
 * console.log('Props atualizadas:', props);
 * });
 * ```
 */
export { singleSpaPropsSubject };
