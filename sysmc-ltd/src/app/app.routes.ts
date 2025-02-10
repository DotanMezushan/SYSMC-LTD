import { Routes } from '@angular/router';
import { CustomersComponent } from './components/customers/customers.component';
import { CustomerDetailsComponent } from './components/customer-details/customer-details.component';

export const routes: Routes = [
    { path: 'customers', component: CustomersComponent },
    { path: 'customer-detail/:id', component: CustomerDetailsComponent }, 
    { path: '', redirectTo: '/customers', pathMatch: 'full' },
];
