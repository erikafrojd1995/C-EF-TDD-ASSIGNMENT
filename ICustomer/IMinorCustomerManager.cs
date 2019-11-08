using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IMinorCustomerManager
    {

       public  MinorCustomer AddMinorCustomer(string minorCustomerName, string minorCustomerAdress, string minorCustomersDateOfBirth, int minorCustomerDebt, int customerID);

        public MinorCustomer GetMinorCustomerByMinorCustomerID(int minorCustomerID);

        public MinorCustomer GetMinorCustomerAge(string MinorCustomerDateOfBirth);

        void RemoveMinorCustomer(int shelfID); 
       
    }
}
