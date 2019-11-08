using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class MinorCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MinorCustomerID { get; set; }
        public string MinorCustomerName { get; set; }
        public string MinorCustomerAdress { get; set; }
        public string MinorCustomersDateOfBirth { get; set; }
        public int MinorCustomerDebt { get; set; }
        public int MinorCustomerNumber { get; set; }

        public int CustomerID { get; set; }
        public Customer Guard { get; set; }

        //public ICollection<MinorCustomerLoan> MinorCustomerLoans { get; set; }
    }
}
