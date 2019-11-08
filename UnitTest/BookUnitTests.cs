using DataInterface;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class BookAPITests
    {
        [TestMethod]
        public void TestAddBookOk()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var successfull = AddBookNumberOne(bookManagerMock);
            var shelf = new Shelf
            {
                ShelfNumber = 1
            };
            Assert.AreEqual(ErrorCodesAddBook.Ok, successfull);
            bookManagerMock.Verify(m =>
               m.AddBook(
                It.Is<string>(i => i == "Boken om kakor"),
                It.Is<string>(i => i == "Gunni"),
                It.Is<string>(i => i == "9789127154377"),
                It.Is<int>(i => i == 2018),
                It.Is<int>(i => i == 3),
                It.Is<int>(i => i == 80),
                It.Is<int>(i => i == 18),
                It.IsAny<bool>(),
                It.Is<int>(i => i == 1),
                It.Is<int>(i => i == 3)),
                Times.Once()) ;
        }
        [TestMethod]
        private ErrorCodesAddBook AddBookNumberOne(Mock<IBookManager> bookManagerMock)
        {

            var shelfManagerMock = new Mock<IShelfManager>();
            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>(),(It.IsAny<int>())))
            .Returns(new Shelf
            {
                ShelfNumber =  1,
                PathID=1
                
            });

            var bookAPI = new BookAPI(bookManagerMock.Object, shelfManagerMock.Object, null);
            var successfull = bookAPI.AddBook("Boken om kakor", "Gunni", "9789127154377", 2018, 3, 80,18,false,1,3);
            return successfull;

        }



        [TestMethod]
        public void TestAddBookAlreadyExists()
        {
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();


            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(new Book
                {
                    BookNumber = 2
                });

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>(),(It.IsAny<int>())))
                .Returns(new Shelf
                {
                    ShelfID = 2
                });

            var bookAPI = new BookAPI(bookManagerMock.Object, shelfManagerMock.Object, null);
            var result = bookAPI.AddBook("Boken om kakor", "Gunni", "9789127154377", 2018, 3, 80, 18, false, 1, 3);
            Assert.AreEqual(ErrorCodesAddBook.BookAlreadyExsist, result);
            bookManagerMock.Verify(m =>
                m.AddBook("Boken om kakor", "Gunni", "9789127154377", 2018, 3, 80, 18,false, 1, 3), Times.Never());
        }

        [TestMethod]
        public void TestAddBookNeedShelf()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var book = new Book
            {
                BookNumber = 1
            };
            var bookAPI = new BookAPI(bookManagerMock.Object, shelfManagerMock.Object, null);
            var result = bookAPI.AddBook("Boken om kakor", "Gunni", "9789127154377", 2018, 3, 80, 18,false, 1, 3);
            Assert.AreEqual(ErrorCodesAddBook.BookNeedsAShelf, result);
            bookManagerMock.Verify(m =>
               m.AddBook(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<int>(),
                It.IsAny<int>()),
                Times.Never());
        }

        [TestMethod]
        public void MoveBookOk()
        {
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>(),It.IsAny<int>()))
                .Returns(new Shelf { ShelfID = 2 });

            bookManagerMock.Setup(m =>
              m.GetBookByBookNumber(It.IsAny<int>()))
               .Returns(new Book
               {
                   BookID = 2,
                   Shelf = new Shelf()
               });

            var bookAPI = new BookAPI(bookManagerMock.Object, shelfManagerMock.Object, null);
            var result = bookAPI.MoveBook(1, 1, 1);
            Assert.AreEqual(ErrorCodesMoveBook.Ok, result);
            bookManagerMock.Verify(m =>
                m.MoveBook(2, 2), Times.Once());
        }

        [TestMethod]
        public void TestRemoveExistingBook()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var loanManagerMock = new Mock<ILoanManager>();
            var shelfManagerMock = new Mock<IShelfManager>();

            bookManagerMock.Setup(m =>
               m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(new Book
                {
                    BookNumber = 4,
                    Loans = new List<Loan>()
                });

            var BookAPI = new BookAPI(bookManagerMock.Object, shelfManagerMock.Object, null);
            var successfull = BookAPI.RemoveBook(4);
            Assert.AreEqual(ErrorCodesRemoveBook.Ok, successfull);
            bookManagerMock.Verify(m =>
                m.RemoveBook(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void TestRemoveBookWithActiveLoan()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var loanManagerMock = new Mock<ILoanManager>();

            bookManagerMock.Setup(m =>
               m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(new Book
                {
                    BookNumber = 3,
                    Loans = new List<Loan>
                    {
                        new Loan()
                    }
                });

            var bookAPI = new BookAPI(bookManagerMock.Object, null, loanManagerMock.Object);
            var successfull = bookAPI.RemoveBook(3);
            Assert.AreEqual(ErrorCodesRemoveBook.BookBelongsToAnActiveLoan, successfull);
            bookManagerMock.Verify(m =>
               m.RemoveBook(It.IsAny<int>()), Times.Never);

        }

        [TestMethod]
        public void TestRemoveNonexistingBook()
        {
            var bookManagerMock = new Mock<IBookManager>();

            bookManagerMock.Setup(m =>
               m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns((Book)null);

            var bookAPI = new BookAPI(bookManagerMock.Object, null, null);
            var successfull = bookAPI.RemoveBook(1);
            Assert.AreEqual(ErrorCodesRemoveBook.NoSuchBook, successfull);
            bookManagerMock.Verify(m =>
               m.RemoveBook(It.IsAny<int>()), Times.Never);

        }

    }
}
