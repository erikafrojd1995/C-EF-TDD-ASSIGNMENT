using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IShelfManager
    {
         public Shelf AddShelf(int shelfNumber, int pathID);  

        void MoveShelf(int shelfNumber, int pathNumber);

        Shelf GetShelfByShelfNumber(int shelfNumber, int pathID);

        void RemoveShelf(int shelfID);

    }
}
