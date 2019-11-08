using DataInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess
{
    public class LoanManager : ILoanManager
    {
        public Loan AddLoan(int customerID, int bookNumber, DateTime loanStart, DateTime loanEnd)
        {
            using var context = new LibraryContext();
            var customer = (from c in context.Customers
                        where c.CustomerID == customerID
                        select c).First();
            var book= (from b in context.Books
                            where b.BookNumber == bookNumber
                            select b).First();

            var loan = new Loan();
            loan.CustomerID = customer.CustomerID;
            loan.BookID = book.BookID;
            loan.LoanStart = loanStart;
            loan.LoanEnd = loanEnd;
            book.Loans.Add(loan);
            context.SaveChanges();
            return loan;
            
        }
        public Loan AddActiveLoanToBook( int bookNumber, bool bookActiveLoan)
        {
            using var context = new LibraryContext();
            var book = (from b in context.Books
                        where b.BookNumber == bookNumber
                        select b).First();
            var loan = new Loan();
            loan.BookID =book.BookID;
            context.Loans.Add(loan);
            context.SaveChanges();
            return loan;
        }

        public Loan AddActiveLoanToCustomer(int customerID, bool customerActiveLoan)
        {
            using var context = new LibraryContext();
            var customer = (from c in context.Customers
                        where c.CustomerID == customerID
                        select c).First();
            var loan = new Loan();
            loan.CustomerID = customer.CustomerID;
            context.Loans.Add(loan);
            context.SaveChanges();
            return loan;
        }

        public List<Loan>GetLoanByCustomers(int customerID)
        {
            using var context = new LibraryContext();
            return (from c in context.Customers
                    join l in context.Loans
                    on c.CustomerID equals l.CustomerID
                    where c.CustomerID == customerID
                    select l).ToList();

        }
        /*public Loan GetLoanByCustomer(string customerName)
        {
            using var context = new LibraryContext();
            return (from c in context.Customers
                    join l in context.Loans
                    on c.CustomerName equals l.Customer
                    where l.CustomerName == customerName
                    select l).FirstOrDefault();

        }
        */
        public Loan GetLoanByCustomerAndBook(int customerID, int bookNumber)
        {
            using var context = new LibraryContext();
            return (from c in context.Customers
                    join l in context.Loans
                    on c.CustomerID equals l.CustomerID
                    join b in context.Books
                    on l.BookID equals b.BookID
                    where b.BookNumber == bookNumber && c.CustomerID == customerID
                    select l).FirstOrDefault();

        }


        public Loan ReturnLoan(int customerID, int bookID)
        {
            using var context = new LibraryContext();
            var customer = (from c in context.Customers
                            where c.CustomerID == customerID
                            select c).First();
            var book = (from b in context.Books
                        where b.BookID == bookID
                        select b).First();

            var loan = new Loan();
            loan.CustomerID = customer.CustomerID;
            loan.BookID = book.BookID;
           // loan.Books.Add(book);
            context.SaveChanges();
            return loan;

        } ////////////////////////////////


        /*public Loan CreateActiveLoan(int customerID)
        {
            using var context = new LibraryContext();
            var loan = new Loan();
            loan.IsActive = true;
            loan.CustomerID = customerID;
            context.Loans.Add(loan);
            context.SaveChanges();
            return loan;
        }*/

        /* public Loan GetActiveLoan(int customerID)
         {
             using var context = new LibraryContext();
             return (from l in context.Loans
                     where l.CustomerID == customerID && l.IsActive
                     select l)
                     .Include(l => l.Items) //ändrade från Book till items
                     .FirstOrDefault();
         }*/

        /*public void CloseLoan(int loanID )
        {
            using var context = new LibraryContext();
            var loan = (from l in context.Loans
                         where l.LoanID == loanID
                         select l)
                    .FirstOrDefault();
            loan.IsActive = false;
            context.SaveChanges();
        }*/

        /*public List<Book> GetLoanForCustomer(int customerID, string nowBorrowingBookTitle)
        {
            using var context = new LibraryContext();
            return (from l in context.Loans.Include(l => l.Items) //Ändrade från book till items
                    join c in context.Customers
                    on l.CustomerID equals c.CustomerID
                    where c.CustomerID == customerID
                    select l.Items)//Ändrade från book till items
                    .Select(b => b)
                    .Where(b => b.BookTitle == nowBorrowingBookTitle)
                    .ToList();
        }*/


    }
}
