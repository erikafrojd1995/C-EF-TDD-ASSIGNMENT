using System;
using System.Collections.Generic;
using System.Text;
using DataInterface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PathManager : IPathManager
    {


        public void AddPath(int pathNumber)
        {
            using var context = new LibraryContext();
            var path = new Path();
            path.PathNumber = pathNumber;
            context.Paths.Add(path);
            context.SaveChanges();


        }


        public Path GetPathByPathID(int pathID)
        {
            using var context = new LibraryContext();
            return (from p in context.Paths
                    where p.PathID == pathID
                    select p)
                    .Include(p => p.Shelves)
                    .FirstOrDefault();
        }



        public void RemovePath(int pathID)
        {
            using var context = new LibraryContext();
            var path = (from p in context.Paths
                        where p.PathID == pathID
                        select p).FirstOrDefault();
            context.Paths.Remove(path);
            context.SaveChanges();
                
        }

    }
}
