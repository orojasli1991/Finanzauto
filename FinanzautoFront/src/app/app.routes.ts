import { Routes } from '@angular/router';
import { LoginComponent } from './auth/pages/login/login.component';
import { ProductsListComponent } from './products/pages/products-list/products-list.component';
import { ProductFormComponent } from './products/pages/product-form/product-form.component';
import { AuthGuard } from './auth/auth.guard';

export const appRoutes: Routes = [
   { path: 'login', component: LoginComponent},
    { path: 'products', component: ProductsListComponent, canActivate: [AuthGuard] },
    { path: 'products/new', component: ProductFormComponent, canActivate: [AuthGuard] },
    { path: 'products/edit/:id', component: ProductFormComponent, canActivate: [AuthGuard] },
    { path: '**', redirectTo: 'login' }
];