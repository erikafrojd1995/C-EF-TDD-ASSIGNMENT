using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IBookManager
    {
        public Book AddBook(string bookTitle, string authorOfBook,string isbn,int purchaseYear, int conditionOfBook, int purchasePrice,int bookNumber, bool bookActiveLoan, int shelfNumber, int pathID ); 
        Book GetBookByBookNumber(int bookNumber);
        void MoveBook(int bookID, int shelfID);
        void RemoveBook(int bookID);

    }
}
