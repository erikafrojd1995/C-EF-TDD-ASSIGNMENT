using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataInterface
{
    public class Shelf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShelfID { get; set; }

        public int ShelfNumber { get; set; }

        public int PathID { get; set; }
        public Path Path { get; set; }

        public ICollection<Book> Books{ get; set; }
    }
}
