using System;
using System.Collections.Generic;
using System.Text;
using DataInterface;


namespace Library
{
    public class PathAndShelfAPI
    {
        private IPathManager pathManager;
       private IShelfManager shelfManager;
        private IBookManager BookManager;




        public PathAndShelfAPI(IPathManager pathManager ,IShelfManager shelfManager, IBookManager bookManager)
        {
            this.pathManager = pathManager;
            this.shelfManager = shelfManager;
            this.BookManager = bookManager;

        }

        public bool AddPath(int pathID)
        {
            var exsitingPath = pathManager.GetPathByPathID(pathID);
            if (exsitingPath != null)
                return false;
            pathManager.AddPath(pathID);
            return true;
        }

        
        public ErrorCodesAddShelf AddShelf(int shelfNumber, int pathID)
        {
            var exsitingShelf = shelfManager.GetShelfByShelfNumber(shelfNumber, pathID);
            var existingPath = pathManager.GetPathByPathID(pathID);
            if (exsitingShelf != null)
                return ErrorCodesAddShelf.ShelfAlreadyExist;
            if (existingPath == null)
                return ErrorCodesAddShelf.PathDoesNotExist;
            shelfManager.AddShelf(shelfNumber, pathID);
            return ErrorCodesAddShelf.Ok;
        }

        public ErrorCodesMoveShelf MoveShelf(int shelfNumber, int pathID)
        {
            var newPath = pathManager.GetPathByPathID(pathID);
            if (newPath == null)
                return ErrorCodesMoveShelf.NoSuchShelf;

            var shelf = shelfManager.GetShelfByShelfNumber(shelfNumber,pathID);
            if (shelf == null)
                return ErrorCodesMoveShelf.NoSuchShelf;
            if (shelf.Path.PathID == pathID)
                return ErrorCodesMoveShelf.ShelfIsAlreadyAtThatPath;

            shelfManager.MoveShelf(shelf.ShelfID, newPath.PathID);
            return ErrorCodesMoveShelf.Ok;
                
        }

        public ErrorCodesPath RemovePath(int pathID)
        {
            var newPath = pathManager.GetPathByPathID(pathID);
            if (newPath == null)
                return ErrorCodesPath.NoSuchPath;
            if (newPath.Shelves.Count > 0)
                return ErrorCodesPath.PathHasShelves;

            pathManager.RemovePath(newPath.PathID);

            return ErrorCodesPath.Ok;
        }
        
        public RemoveShelfErrorCodes RemoveShelf(int shelfNumber, int bookNumber, int pathID)
        {
            var newShelf = shelfManager.GetShelfByShelfNumber(shelfNumber,pathID);
            if (newShelf == null)
                return RemoveShelfErrorCodes.NoSuchShelf;
            if (newShelf.Books.Count > 0)
                return RemoveShelfErrorCodes.ShelfHasBooks;

            shelfManager.RemoveShelf(newShelf.ShelfID);
            return RemoveShelfErrorCodes.Ok;
        }
    }
}
