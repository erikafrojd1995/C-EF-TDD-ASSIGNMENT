using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAdress { get; set; }
        public string CustomersDateOfBirth { get; set; }
        public int CustomerDebt { get; set; }

        public bool CustomerActiveLoan { get; set; }


        public ICollection<MinorCustomer> MinorCustomers { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
