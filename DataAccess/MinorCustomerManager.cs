using System;
using System.Collections.Generic;
using System.Text;
using DataInterface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MinorCustomerManager : IMinorCustomerManager
    {

        public MinorCustomer AddMinorCustomer(string minorCustomerName, string minorCustomerAdress, string minorCustomersDateOfBirth, int minorCustomerDebt, int customerID)
        {
            var customerManager = new CustomerManager();
            using var context = new LibraryContext();
            var minorCustomer = new MinorCustomer();
            minorCustomer.MinorCustomerName = minorCustomerName;
            minorCustomer.MinorCustomerAdress = minorCustomerAdress;
            minorCustomer.MinorCustomersDateOfBirth = minorCustomersDateOfBirth;
            minorCustomer.MinorCustomerDebt = minorCustomerDebt;
            minorCustomer.CustomerID = customerManager.GetCustomerByCustomerID(customerID).CustomerID;
            context.MinorCustomers.Add(minorCustomer);
            context.SaveChanges();
            return minorCustomer;
        }



        public MinorCustomer GetMinorCustomerByMinorCustomerID(int minorCustomerID)
        {
            using var context = new LibraryContext();
            return (from m in context.MinorCustomers
                    where m.MinorCustomerNumber == minorCustomerID
                    select m)
                   .Include(m => m.Guard)
                   .FirstOrDefault();
        }

        public MinorCustomer GetMinorCustomerAge(string minorCustomersDateOfBirth)
        {
            using var context = new LibraryContext();
            return (from m in context.MinorCustomers
                    where m.MinorCustomersDateOfBirth == minorCustomersDateOfBirth
                    select m)
                     .Include(m => m.MinorCustomerID)
                     .FirstOrDefault();
        }

        public void RemoveMinorCustomer(int minorCustomerID)
        {
            using var context = new LibraryContext();
            var minorCustomer = (from m in context.MinorCustomers
                         where m.MinorCustomerID == minorCustomerID
                         select m).FirstOrDefault();
            context.MinorCustomers.Remove(minorCustomer);
            context.SaveChanges();
        }
    }
}
