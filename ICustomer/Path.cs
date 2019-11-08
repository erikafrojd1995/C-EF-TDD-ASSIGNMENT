using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataInterface
{
    public class Path
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PathID { get; set; }

        public int PathNumber { get; set; }

        public ICollection<Shelf> Shelves { get; set; }
    }
}
