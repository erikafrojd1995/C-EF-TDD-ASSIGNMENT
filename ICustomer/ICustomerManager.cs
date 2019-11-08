using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface ICustomerManager
    {

        Customer GetCustomerByCustomerID(int customerID);
        void RemoveCustomer(int customerID);
        void AddCustomer(string customerName, string customerAdress, string customersDateOfBirth, int customerDebt, bool customerActiveLoan);
       public void ValidDateCheck(string customersDateOfBirth);

    }


}



