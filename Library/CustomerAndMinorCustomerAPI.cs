using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class CustomerAndMinorCustomerAPI
    {
        private ICustomerManager customerManager;
        private IMinorCustomerManager minorCustomerManager;
        private ILoanManager loanManager;

        public CustomerAndMinorCustomerAPI(ICustomerManager customerManager, IMinorCustomerManager minorCustomerManager, ILoanManager loanManager)
        {
            this.customerManager = customerManager;
            this.minorCustomerManager = minorCustomerManager;
            this.loanManager = loanManager;

        }

        /* public bool AddCustomer(string customerName, string customerAdress, string customersDateOfBirth, int customerDebt,int customerID)
         {
             var exsitingCustomer = customerManager.GetCustomerByCustomerID(customerID);
             if (exsitingCustomer != null)
                 return false;
             customerManager.AddCustomer(customerName, customerAdress, customersDateOfBirth, customerDebt);
             return true;
         }*/

        public ErrorCodesAddCustomer AddCustomer(string customerName, string customerAdress, string customersDateOfBirth, int customerDebt,bool customerActiveLoan, int customerID)
        {

            var existingCustomer = customerManager.GetCustomerByCustomerID(customerID);
            if (existingCustomer != null)
                return ErrorCodesAddCustomer.CustomerAlreadyExists;
            if (GetAgeCustomer(customersDateOfBirth) < 15)
                return ErrorCodesAddCustomer.CustomerIsToYoung;
            customerManager.AddCustomer(customerName, customerAdress, customersDateOfBirth, customerDebt, customerActiveLoan);
            return ErrorCodesAddCustomer.Ok;
        }

        public ErrorCodesDateOfBirthCheck CustomersDateOfBirth(string minorCustomersDateOfBirth, int customerID)
        {
            var newCustomer = customerManager.GetCustomerByCustomerID(customerID);
            int customersDateofBirthInt = Convert.ToInt32(minorCustomersDateOfBirth);
            DateTime minDate = DateTime.Parse("1900-01-01");
            DateTime maxDate = DateTime.Parse("2019-01-01");
            if (customersDateofBirthInt < minDate.Date.Month)
            {
                return ErrorCodesDateOfBirthCheck.customerIsToOld;
            }
            if (customersDateofBirthInt > maxDate.Date.Month)
            {
                return ErrorCodesDateOfBirthCheck.customerIsToYoung;
            }
            else
            {
                return ErrorCodesDateOfBirthCheck.Ok;
            }
        }

        public int GetAgeCustomer(string customerDateOfBirth)
        {
            DateTime temp;
            DateTime.TryParse(customerDateOfBirth, out temp);

            int today = int.Parse(DateTime.Today.ToString("yyyymmdd"));
            int dateOfBirth = int.Parse(temp.ToString("yyyymmdd"));
            int age = (today - dateOfBirth) / 10000;
            return age;
        }

        public int GetAgeMinorCustomer(string minorCustomerDateOfBirth)
        {
            DateTime temp;
            DateTime.TryParse(minorCustomerDateOfBirth, out temp);

            int today = int.Parse(DateTime.Today.ToString("yyyymmdd"));
            int dateOfBirth = int.Parse(temp.ToString("yyyymmdd"));
            int age = (today - dateOfBirth) / 10000;
            return age;
        }

        public AddMinorCustomerErrorCodes AddMinorCustomer(string minorCustomerName, string minorCustomerAdress, string minorCustomersDateOfBirth, int minorCustomerDebt, int customerNumber, int minorCustomerID)
         {

             var exsitingMinorCustomer = minorCustomerManager.GetMinorCustomerByMinorCustomerID(minorCustomerID);
             var existingCustomer = customerManager.GetCustomerByCustomerID(customerNumber);

             if (exsitingMinorCustomer != null)
                 return AddMinorCustomerErrorCodes.MinorCustomerAlreadyExsist;
             if (GetAgeMinorCustomer(minorCustomersDateOfBirth)> 15)
                 return AddMinorCustomerErrorCodes.MinorCustomerIsToOld;
             if (existingCustomer == null)
             return AddMinorCustomerErrorCodes.MinorCustomerNeedsAGuard;
             minorCustomerManager.AddMinorCustomer(minorCustomerName,minorCustomerAdress,minorCustomersDateOfBirth,minorCustomerDebt,customerNumber);
             return AddMinorCustomerErrorCodes.Ok;
         }



        public ErrorCodesRemoveCustomer RemoveCustomer(int customerID)
        {
            var newCustomer = customerManager.GetCustomerByCustomerID(customerID);
            if (newCustomer == null)
                return ErrorCodesRemoveCustomer.NoSuchCustomer;
            //if (newCustomer.Loans.Count > 0)
               // return ErrorCodesRemoveCustomer.CustomerHasAnActiveLoan;
            if (newCustomer.MinorCustomers.Count > 0)                                   
                return ErrorCodesRemoveCustomer.CustomerIsConnectedToMinorCustomer;
            customerManager.RemoveCustomer(newCustomer.CustomerID);
            return ErrorCodesRemoveCustomer.Ok;
        }


        
        public ErrorCodesRemoveMinorCustomer RemoveMinorCustomer(int minorCustomerID)
        {
            var newMinorCustomer = minorCustomerManager.GetMinorCustomerByMinorCustomerID(minorCustomerID);
            if (newMinorCustomer == null)
                return ErrorCodesRemoveMinorCustomer.NoSuchMinorCustomer;
            //if (newMinorCustomer.Books.Count > 0)
                //return RemoveMinorCustomerErrorCodes.MinorcustomerHasBooks;

            minorCustomerManager.RemoveMinorCustomer(newMinorCustomer.MinorCustomerID);
            return ErrorCodesRemoveMinorCustomer.Ok;
        }

   

    }
}
