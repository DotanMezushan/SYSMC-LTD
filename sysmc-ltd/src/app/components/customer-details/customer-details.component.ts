import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../Services/customer.service';
import { Customer } from '../../models/models';
import { CommonModule } from '@angular/common';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-customer-details',
  imports: [CommonModule, MatButtonModule, MatIconModule],
  templateUrl: './customer-details.component.html',
  styleUrl: './customer-details.component.scss'
})
export class CustomerDetailsComponent implements OnInit {
  customer: Customer | undefined;

  constructor(
    private route: ActivatedRoute,
    private router : Router,
    private customerService: CustomerService
  ) { 
      
  }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.customerService.getCustomerById(id).subscribe((customer: Customer) => {
      this.customer = customer;
    });
  }

  navigetToHome(): void{
    this.router.navigate(['/customers']);
  }
}
