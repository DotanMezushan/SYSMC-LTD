import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { CustomerService } from '../../Services/customer.service';
import {MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Customer } from '../../models/models';
import { CommonModule } from '@angular/common';
import { MatInputModule} from '@angular/material/input';
import { Observable, of } from 'rxjs';


@Component({
  selector: 'app-customer-form',
  imports: [
    MatDialogModule,
    CommonModule, 
    ReactiveFormsModule,
    MatFormFieldModule, 
    MatIconModule,
     FormsModule, 
     MatButtonModule,
     MatInputModule,
    ],
  templateUrl: './customer-form.component.html',
  styleUrl: './customer-form.component.scss'
})
export class CustomerFormComponent implements OnInit {
  customerForm!: FormGroup;
  isEditMode = false;
  customerId !: number ;

  constructor(
    public fb: FormBuilder,
    private customerService: CustomerService,
    private dialogRef: MatDialogRef<CustomerFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any  
  ) {}

  ngOnInit(): void {
    this.customerId = Number(this.data); 
    this.initForm();

    if (this.customerId) {
      this.isEditMode = true;
      this.customerService.getCustomerById(this.customerId).subscribe((customer: Customer) => {
        if (!customer) {
          console.error('Customer not found!');
          return;
        }
        
        this.customerForm.patchValue({
          name: customer.name,
          customerNumber: customer.customerNumber
        });

        this.setExistingData(customer);
      });
    }
  }

  private initForm() {
    this.customerForm = this.fb.group({
      name: ['', Validators.required],
      customerNumber: ['', Validators.required, this.nineDigitValidator],
      addresses: this.fb.array([]),
      contacts: this.fb.array([]),
    });
  }

  private setExistingData(customer: Customer) {
    if (customer.addresses && Array.isArray(customer.addresses)) {
      customer.addresses.forEach((address) =>
        this.addresses.push(this.createAddressGroup(address))
      );
    }
  
    if (customer.contacts && Array.isArray(customer.contacts)) {
      customer.contacts.forEach((contact) =>
        this.contacts.push(this.createContactGroup(contact as any))
      );
    }
  }  

  get addresses(): FormArray {
    return this.customerForm.get('addresses') as FormArray;
  }

  get contacts(): FormArray {
    return this.customerForm.get('contacts') as FormArray;
  }

  createAddressGroup(address: { id?: number, city: string, street: string } = { id: 0, city: '', street: '' }): FormGroup {
    return this.fb.group({
      id: [address.id],
      city: [address.city, Validators.required],
      street: [address.street, Validators.required],
    });
  }

  createContactGroup(contact: { id?: number, fullName: string, officeNumber: string, email: string } = { id: 0, fullName: '', officeNumber: '', email: '' }): FormGroup {
    return this.fb.group({
      id: [contact.id],
      fullName: [contact.fullName, Validators.required],
      officeNumber: [contact.officeNumber], 
      email: [contact.email, Validators.email],
    });
  }

  addAddress() {
    this.addresses.push(this.createAddressGroup());
  }

  addContact() {
    this.contacts.push(this.createContactGroup());
  }

  removeAddress(index: number) {
    this.addresses.removeAt(index);
  }

  removeContact(index: number) {
    this.contacts.removeAt(index);
  }

  save() {
    if (this.customerForm.invalid) return;
  
    const customerData = this.customerForm.value;
    console.log('Data being sent:', customerData); 
  
    if (this.isEditMode) {
      this.customerService.updateCustomer(customerData, this.customerId).subscribe(() => {
        this.dialogRef.close(true);
      });
    } else {
      this.customerService.createCustomer(customerData).subscribe(() => {
        this.dialogRef.close(true);
      });
    }
  }  

  close() {
    this.dialogRef.close(false);
  }

  nineDigitValidator(control: FormControl): Observable<ValidationErrors | null> { 
    const value = control.value;

    if (!value) {
      return of(null);
    }

    const cleanedValue = value.replace(/\D/g, '');

    if (cleanedValue.length !== 9) {
      return of({ 'nineDigits': true }); 
    }

    return of(null); 
  }
  
}
