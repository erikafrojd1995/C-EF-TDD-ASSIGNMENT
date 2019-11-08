using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public enum ErrorCodesRemoveCustomer
    {
        NoSuchCustomer,
        CustomerIsConnectedToMinorCustomer,
        CustomerHasAnActiveLoan, 
        CustomerHasDept, //Ej fixat
        Ok
    }
}