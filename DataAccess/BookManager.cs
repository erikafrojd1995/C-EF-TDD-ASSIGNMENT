using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BookManager : IBookManager
    {
 

        public Book AddBook(string bookTitle, string authorOfBook, string isbn, int purchaseYear, int conditionOfBook, int purchasePrice, int bookNumber, bool bookActiveLoan ,int shelfNumber,int pathID)
        {
            var shelfManager = new ShelfManager();
            using var context = new LibraryContext();
            var book = new Book();
            book.BookTitle = bookTitle;
            book.AuthorOfBook = authorOfBook;
            book.ISBN = isbn;
            book.PurchaseYear = purchaseYear;
            book.ConditionOfBook = conditionOfBook;
            book.PurchasePrice = purchasePrice;
            book.BookNumber = bookNumber;
            book.BookActiveLoan = false;
            book.ShelfID = shelfManager.GetShelfByShelfNumber(shelfNumber, pathID).ShelfID; 
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }

        public Book GetBookByBookNumber(int bookNumber)
        {
            using var context = new LibraryContext();
            return (from b in context.Books
                    where b.BookNumber == bookNumber
                    select b)
                         .Include(b => b.Shelf)
                         .FirstOrDefault();
        }

        public void MoveBook(int bookID, int shelfID)
        {
            using var context = new LibraryContext();
            var shelf = (from b in context.Books
                         where b.BookID == bookID
                         select b)
                         .First();
            shelf.ShelfID = shelfID;
            context.SaveChanges();
        }

       /* public List<Book> GetBooks()
        {
            using var context = new LibraryContext();
            return (from b in context.Books
                    select b).ToList();
        }*/

        public void RemoveBook(int bookID)
        {
            using var context = new LibraryContext();
            var book = (from b in context.Books
                         where b.BookID == bookID
                         select b).FirstOrDefault();
            context.Books.Remove(book);
            context.SaveChanges();

        }



    }
}
