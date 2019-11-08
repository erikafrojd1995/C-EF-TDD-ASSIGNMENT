using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;


namespace Library
{
   public class CustomersLoanStatusBook
    {
        public CustomersLoanStatusBook(Book book)
        {
            BookTitle = book.BookTitle;
            PurchasePrice = book.PurchasePrice;
            BookNumber = book.BookNumber;

        }
        public string BookTitle;
        public int PurchasePrice;
        public int BookNumber;
    }   


}
