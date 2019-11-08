using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public enum ErrorCodesLoan
    {
        NoSuchBook,
        NoSuchCustomer,
        CustomerHasToManyBooks,
        CustomerHasDept,
        BookBelongsToActiveLoan,
        Ok
    }
}
