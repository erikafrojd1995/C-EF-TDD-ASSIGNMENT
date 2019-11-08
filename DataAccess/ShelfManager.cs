using DataInterface;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
   public class ShelfManager : IShelfManager
    {
        public Shelf AddShelf(int shelfNumber, int pathID)
        {
            var pathManager = new PathManager();
            using var context = new LibraryContext();
            var shelf = new Shelf();
            shelf.ShelfNumber = shelfNumber;
            shelf.PathID = pathManager.GetPathByPathID(pathID).PathID; 
            context.Shelves.Add(shelf);
            context.SaveChanges();
            return shelf;
        }


        public Shelf GetShelfByShelfNumber(int shelfNumber, int pathID)
        {
            using var context = new LibraryContext();
            return(from s in context.Shelves
                   where s.ShelfNumber == shelfNumber
                   select s)
                   .Include(s => s.Path)
                   .FirstOrDefault();
        }

        public void MoveShelf(int shelfID, int pathID)
        {
            using var context = new LibraryContext();
            var shelf = (from s in context.Shelves
                         where s.ShelfID == shelfID
                         select s)
                         .First();
            shelf.PathID = pathID;
            context.SaveChanges();
        }

        public void RemoveShelf(int shelfID)
        {
            using var context = new LibraryContext();
            var shelf = (from s in context.Shelves
                        where s.ShelfID == shelfID
                        select s).FirstOrDefault();
            context.Shelves.Remove(shelf);
            context.SaveChanges();

        }
    }
}
