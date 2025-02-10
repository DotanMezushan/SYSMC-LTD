export interface Customer {
    id: number;
    name: string;
    customerNumber: string; 
    isDeleted: boolean;
    created: Date; 
    addresses: Address[];
    contacts: Contact[];
  }
  
  export interface Address {
    id: number;
    city: string;
    street: string;
    customerId: number;
    isDeleted: boolean;
    created: Date; 
  }
  
  export interface Contact {
    id: number;
    fullName: string;
    officeNumber?: string;  
    email?: string;         
    customerId: number;   
    isDeleted: boolean;
    created: Date;      
  }
  