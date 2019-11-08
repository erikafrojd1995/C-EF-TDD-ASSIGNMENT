using DataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class BookAPI
    {
        private IBookManager bookManager;
        private IShelfManager shelfManager;
        private ILoanManager loanManager;


        public BookAPI(IBookManager bookManager, IShelfManager shelfManager, ILoanManager loanManager)
        {
            this.bookManager = bookManager;
            this.shelfManager = shelfManager;
            this.loanManager = loanManager;

        }

        public ErrorCodesAddBook AddBook(string bookTitle, string authorOfBook, string isbn, int purchaseYear, int conditionOfBook, int purchasePrice, int bookNumber, bool activeLoan, int shelfNumber,int pathID)
        {
            var exsitingBook = bookManager.GetBookByBookNumber(bookNumber);
            var exsistingShelf = shelfManager.GetShelfByShelfNumber(shelfNumber, pathID);
            if (exsitingBook != null)
                return ErrorCodesAddBook.BookAlreadyExsist;
            if (isbn.Length != 13)
                return ErrorCodesAddBook.InvalidISBN;
            if (exsistingShelf == null)
                return ErrorCodesAddBook.BookNeedsAShelf;
            bookManager.AddBook(bookTitle, authorOfBook, isbn, purchaseYear, conditionOfBook, purchasePrice, bookNumber, activeLoan, shelfNumber, pathID);
            return ErrorCodesAddBook.Ok;
        }


        public ErrorCodesMoveBook MoveBook(int bookNumber, int shelfNumber, int pathID) 
        {
            var newShelf = shelfManager.GetShelfByShelfNumber(shelfNumber, pathID);
            if (newShelf == null)
                return ErrorCodesMoveBook.NoSuchShelf;

            var book = bookManager.GetBookByBookNumber(bookNumber);
            if (book == null)
                return ErrorCodesMoveBook.NoSuchBook;
            if (book.Shelf.ShelfNumber == shelfNumber)
                return ErrorCodesMoveBook.BookIsAlreadyAtThatShelf;

            bookManager.MoveBook(book.BookID, newShelf.ShelfID);

            return ErrorCodesMoveBook.Ok;
        }

        public ErrorCodesRemoveBook RemoveBook(int bookID)
        {
            var newBook = bookManager.GetBookByBookNumber(bookID);
            if (newBook == null)
                return ErrorCodesRemoveBook.NoSuchBook;
            if (newBook.Loans.Count > 0)
                return ErrorCodesRemoveBook.BookBelongsToAnActiveLoan;

            bookManager.RemoveBook(newBook.BookID);
            return ErrorCodesRemoveBook.Ok;
        }

    }
}
