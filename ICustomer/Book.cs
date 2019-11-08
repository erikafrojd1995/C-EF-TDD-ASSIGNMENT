using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        public string BookTitle { get; set; }
        public string AuthorOfBook { get; set; }
        public string ISBN { get; set; }
        public int PurchaseYear { get; set; }
        public int ConditionOfBook { get; set; }
        public int PurchasePrice { get; set; }
        public int BookNumber { get; set; }

        public bool BookActiveLoan { get; set; }

        

        public int ShelfID { get; set; }
        public Shelf Shelf { get; set; }
        public ICollection<Loan> Loans { get; set; }




    }
}
