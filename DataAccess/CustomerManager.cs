using DataInterface;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CustomerManager : ICustomerManager
    {


        public void AddCustomer(string customerName, string customerAdress, string customersDateOfBirth, int customerDebt, bool customerActiveLoan)
        {
            using var context = new LibraryContext();
            var customer = new Customer();
            customer.CustomerName = customerName;
            customer.CustomerAdress = customerAdress;
            customer.CustomersDateOfBirth = customersDateOfBirth;
            customer.CustomerDebt = customerDebt;
            customer.CustomerActiveLoan = false;
            context.Customers.Add(customer);
            context.SaveChanges();
        }


        public Customer GetCustomerByCustomerID(int customerID)
        {
            using var context = new LibraryContext();
            return (from c in context.Customers
                    where c.CustomerID == customerID
                    select c)
                    .Include(c => c.MinorCustomers)
                    .FirstOrDefault();
        }

        public void RemoveCustomer(int customerID)
        {
            using var context = new LibraryContext();
            var customer = (from c in context.Customers
                        where c.CustomerID == customerID
                        select c).FirstOrDefault();
            context.Customers.Remove(customer);
            context.SaveChanges();

            //Om kunden är skyldig biblioteket pengar ska den ej gå att ta bort.
            //Om Kunden har böcker hemma ska den ej gå att ta bort

        }



        public void ValidDateCheck(string customersDateOfBirth)
        {
            using var context = new LibraryContext();
            var customer = (from c in context.Customers
                         where c.CustomersDateOfBirth == customersDateOfBirth
                         select c).FirstOrDefault();
            context.SaveChanges();
        }



    }
}
