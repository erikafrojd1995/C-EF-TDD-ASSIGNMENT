using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Library
{
    public class LoanAPI
    {
        IBookManager bookManager;
        ILoanManager loanManager;
        ICustomerManager customerManager;

        public LoanAPI(IBookManager bookManager, ILoanManager loanManager, ICustomerManager customerManager)
        {
            this.bookManager = bookManager;
            this.loanManager = loanManager;
            this.customerManager = customerManager;
        }

        public ErrorCodesLoan AddLoan(int bookNumber, int customerID)
        {
            var loanStart = DateTime.Today;
            var loanEnd = DateTime.Today.AddDays(14);
            var book = bookManager.GetBookByBookNumber(bookNumber);
            if (book == null)
                return ErrorCodesLoan.NoSuchBook;
            var customer = customerManager.GetCustomerByCustomerID(customerID);
            if (customer == null)
                return ErrorCodesLoan.NoSuchCustomer;
            if (book.BookActiveLoan == true)
                return ErrorCodesLoan.BookBelongsToActiveLoan;
            if (loanManager.GetLoanByCustomers(customerID).Count > 5)
                return ErrorCodesLoan.CustomerHasToManyBooks;
            var loan = loanManager.GetLoanByCustomerAndBook(bookNumber, customerID);
            if (loan == null)
                loanManager.AddLoan(customerID, bookNumber, loanStart, loanEnd);
            loanManager.AddActiveLoanToBook(bookNumber, true);
            loanManager.AddActiveLoanToCustomer(customerID, true);
            return ErrorCodesLoan.Ok;

        }

        public ErrorCodesReturnBook ReturnLoan(int bookID, int customerID)
        {

            var book = bookManager.GetBookByBookNumber(bookID);
            if (book == null)
                return ErrorCodesReturnBook.WrongAmountOfBooks;
            var customer = customerManager.GetCustomerByCustomerID(customerID);
            if (customer == null)
                return ErrorCodesReturnBook.NoSuchCustomer;
            if (book.BookActiveLoan == false)
                return ErrorCodesReturnBook.NoOpenLoan;
            var loan = loanManager.GetLoanByCustomerAndBook(customerID, bookID);
            if (loan != null)
                loanManager.ReturnLoan(customerID, bookID);
            loanManager.AddActiveLoanToBook(bookID, false);
            loanManager.AddActiveLoanToCustomer(customerID, false);
            return ErrorCodesReturnBook.Ok;

        }


        /* public ErrorCodesReturnBook ReturnBook(int customerID, int returnedAmount)
         {
             var customersLoanStatus = GetCustomersLoanStatus(customerID);
             if (customersLoanStatus.BookStatus == CustomersLoanStatus.Status.NoSuchCustomer)
                 return ErrorCodesReturnBook.NoSuchCustomer;
             if (customersLoanStatus.BookStatus == CustomersLoanStatus.Status.Settled)
                 return ErrorCodesReturnBook.NoOpenLoan;
             if (customersLoanStatus.Amount != returnedAmount)
                 return ErrorCodesReturnBook.WrongAmountOfBooks;
             var customer = customerManager.GetCustomerByCustomerID(customerID);
             var loan = loanManager.GetActiveLoan(customer.CustomerID);
             loanManager.CloseLoan(loan.LoanID);
             return ErrorCodesReturnBook.Ok;
         }*/


        /* public CustomersLoanStatus GetCustomersLoanStatus(int customerID)
         {
             var customer = customerManager.GetCustomerByCustomerID(customerID);
                 if (customer == null)
                 return new CustomersLoanStatus { BookStatus = CustomersLoanStatus.Status.NoSuchCustomer};
                 var loan = loanManager.GetActiveLoan(customer.CustomerID);
             if (IsBookSettled(loan))
                 return new CustomersLoanStatus { BookStatus = CustomersLoanStatus.Status.Settled };
             CustomersLoanStatus customersLoanStatus = GetCustomersLoanStatusAfterErrorChecks(loan);
             return customersLoanStatus;

         }*/
        /* private static bool IsBookSettled(Loan loan)
         {
             return loan == null || !loan.IsActive;
         }*/

        /* private static CustomersLoanStatus GetCustomersLoanStatusAfterErrorChecks(Loan loan)
         {
             var customersLoanStatus = new CustomersLoanStatus();
             AddBooksToCustomersLoanStatus(loan, customersLoanStatus);
             customersLoanStatus.BookStatus = GetCustomersLoanStatusFromCustomersLoaneStatusAmount(customersLoanStatus);
             return customersLoanStatus;
         }*/
        /*private static CustomersLoanStatus.Status GetCustomersLoanStatusFromCustomersLoaneStatusAmount(CustomersLoanStatus customersLoanStatus)
        {
            return customersLoanStatus.Amount > 0 ? CustomersLoanStatus.Status.LoaneIsOpen : CustomersLoanStatus.Status.Settled;
        }*/

        /* private static void AddBooksToCustomersLoanStatus(Loan loan, CustomersLoanStatus customersLoanStatus)
         {
             customersLoanStatus.Books = new List<CustomersLoanStatusBook>();
             foreach (var book in loan.Books)
             {
                 customersLoanStatus.Amount += book.PurchasePrice;
                 customersLoanStatus.Books.Add(new CustomersLoanStatusBook(book));
             }
         }*/





    }
}

