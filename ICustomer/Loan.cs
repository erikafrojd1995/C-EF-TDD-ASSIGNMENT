using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class Loan
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanID { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public DateTime LoanStart { get; set; }
        public DateTime LoanEnd { get; set; }

        public int BookID { get; set; }
        public Book Items { get; set; }

       // public List<Book> Books { get; set; }

        
    }
}
