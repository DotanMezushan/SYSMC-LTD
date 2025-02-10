import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../Services/customer.service';
import { Customer } from '../../models/models';
import {MatTableModule} from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CustomerFormComponent } from '../customer-form/customer-form.component';


@Component({
  selector: 'app-customers',
  imports: [
    CommonModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatIconModule 
  ],
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit {
  customers: Customer[] = [];
  displayedColumns: string[] = ['name', 'customerNumber', 'created', 'actions'];

  constructor(
    private customerService: CustomerService,
     private router: Router,
     private dialog: MatDialog
    ) {}

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe((customers: Customer[]) => {
      this.customers = customers;
    });
  }

  editCustomer(id  :number){
    const dialogRef = this.dialog.open(CustomerFormComponent, {
      width: '600px',
      data: id
    });
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        window.location.reload();
      }
    });
  }

  deleteCustomer(customerId: number): void {
    if (confirm("Are you sure you want to delete this customer?")) {
      this.customerService.deleteCustomer(customerId).subscribe(() => {
        this.customers = this.customers.filter(c => c.id !== customerId);
      });
    }
  }

  navigateToCustomerPage(id : number){
    this.router.navigate(['/customer-detail', id]);
  }
}
