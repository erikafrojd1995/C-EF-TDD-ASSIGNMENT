using Library;
using System;
using System.Collections.Generic;
using System.Text;
using DataInterface;

namespace Library
{
    public class CustomersLoanStatus
    {
        public enum Status
        {
            NoSuchCustomer,
            Settled,
            LoaneIsOpen
        }
        public Status BookStatus;
        public int Amount;
        public List<CustomersLoanStatusBook> Books
            ;
    }
}
