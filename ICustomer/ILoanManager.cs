using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface ILoanManager
    {
        public Loan AddLoan(int customerID, int bookNumber, DateTime loanStart, DateTime loanEnd);

        Loan GetLoanByCustomerAndBook(int customerID, int bookNumber);
        List<Loan> GetLoanByCustomers(int customerID);

        Loan AddActiveLoanToBook(int bookNumber, bool bookActiveLoan);

        Loan AddActiveLoanToCustomer(int customerID, bool customerActiveLoan);

        public Loan ReturnLoan(int customerID, int bookID);


        //Loan CreateActiveLoan(int customerID);
        //void CloseLoan(int loanID);



    }
}
