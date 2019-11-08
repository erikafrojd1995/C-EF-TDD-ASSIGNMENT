using DataInterface;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PathAndShelfAPITests
    {
        [TestMethod]
        public void TestAddPath()
        {
            Mock<IPathManager> pathManagerMock = SetupMock((Path)null); ;
            bool successfull = AddPathNumberOne(pathManagerMock);
            Assert.IsTrue(successfull);
            pathManagerMock.Verify(
                m => m.AddPath(It.Is<int>(i => i == 1)),
                    Times.Once());

        }
        
        [TestMethod]
        public void TestAddExistingPath()
        {
            var pathManagerMock = SetupMock(new Path());
            bool successfull = AddPathNumberOne(pathManagerMock);
            Assert.IsFalse(successfull);
            pathManagerMock.Verify(
                m => m.AddPath(It.Is<int>(i => i == 1)),
                Times.Never());
        }

        [TestMethod]
        public void TestRemoveExistingPath()
        {
            var pathManagerMock = SetupMock(new Path());
            bool successfull = AddPathNumberOne(pathManagerMock);
            Assert.IsFalse(successfull);
            pathManagerMock.Verify(
                m => m.AddPath(It.Is<int>(i => i == 1)),
                    Times.Never());
        }

        [TestMethod]
        public void TestRemovePathwithShelf()
        {
            var pathManagerMock = new Mock<IPathManager>();
            var shelfManagerMock = new Mock<IShelfManager>();

            pathManagerMock.Setup(m =>
               m.GetPathByPathID(It.IsAny<int>()))
                .Returns(new Path
                {
                    PathNumber = 4,
                    Shelves = new List<Shelf>
                    {
                        new Shelf()
                    }
                });

            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, shelfManagerMock.Object, null);
            var successfull = pathAndShelfAPI.RemovePath(4);
            Assert.AreEqual(ErrorCodesPath.PathHasShelves, successfull);
            pathManagerMock.Verify(m =>
               m.RemovePath(It.IsAny<int>()), Times.Never);

        }

        [TestMethod]
        public void TestRemoveNonexistingPath()
        {
            var pathManagerMock = new Mock<IPathManager>();
            var shelfManagerMock = new Mock<IShelfManager>();

            pathManagerMock.Setup(m =>
               m.GetPathByPathID(It.IsAny<int>()))
                .Returns((Path)null);

            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, shelfManagerMock.Object,null);
            var successfull = pathAndShelfAPI.RemovePath(4);
            Assert.AreEqual(ErrorCodesPath.NoSuchPath, successfull);
            pathManagerMock.Verify(m =>
               m.RemovePath(It.IsAny<int>()), Times.Never);

        }
        [TestMethod]
        public void RemoveEmptyPath()
        {
            var pathManagerMock = new Mock<IPathManager>();
            var shelfManagerMock = new Mock<IShelfManager>();

            pathManagerMock.Setup(m =>
               m.GetPathByPathID(It.IsAny<int>()))
                .Returns(new Path
                {
                    PathNumber = 4,
                    Shelves = new List<Shelf>()
                });

            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, shelfManagerMock.Object,null);
            var successfull = pathAndShelfAPI.RemovePath(4);
            Assert.AreEqual(ErrorCodesPath.Ok, successfull);
            pathManagerMock.Verify(m =>
                m.RemovePath(It.IsAny<int>()), Times.Once);
        }


        [TestMethod]
        private static Mock<IPathManager> SetupMock(Path path)
        {
            var pathManagerMock = new Mock<IPathManager>();

            pathManagerMock.Setup(m =>
           m.GetPathByPathID(It.IsAny<int>()))
                .Returns(path);

            pathManagerMock.Setup(m =>
            m.AddPath(It.IsAny<int>()));
            return pathManagerMock;

        }
        [TestMethod]
        private static bool AddPathNumberOne(Mock<IPathManager> pathManagerMock)
        {
            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, null,null);
            var successfull = pathAndShelfAPI.AddPath(1);
            return successfull;
        }




        [TestMethod]
        public void TestAddShelfOk()
        {
            var shelfManagerMock = new Mock<IShelfManager>();
            var successfull = AddShelfNumberOne(shelfManagerMock);
            var path = new Path
            {
                PathNumber =  1
            };
            Assert.AreEqual(ErrorCodesAddShelf.Ok, successfull);
            shelfManagerMock.Verify(m =>
               m.AddShelf(It.IsAny<int>(),(It.IsAny<int>())), Times.Once); 
        }
        [TestMethod]
        public void TestAddShelfAlreadyExist()
        {
            var pathManagerMock = new Mock<IPathManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
           

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>(), (It.IsAny<int>())))
                .Returns(new Shelf
                { 
                    ShelfNumber = 2 
                });

            pathManagerMock.Setup(m =>
            m.GetPathByPathID(It.IsAny<int>()))
                .Returns(new Path
                {
                    PathNumber = 2
                });

            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, shelfManagerMock.Object, null);
            var result = pathAndShelfAPI.AddShelf(2, 2);
            Assert.AreEqual(ErrorCodesAddShelf.ShelfAlreadyExist,result);
            shelfManagerMock.Verify(m =>
                m.AddShelf(2, 2), Times.Never());
        }

        [TestMethod]
        public void TestAddShelfPathDoesNotExist()
        {
            var shelfManagerMock = new Mock<IShelfManager>();
            var pathManagerMock = new Mock<IPathManager>();
            var shelf = new Shelf
            {
                ShelfNumber = 1
            };
            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, shelfManagerMock.Object, null);
            var result = pathAndShelfAPI.AddShelf(1, 2);
            Assert.AreEqual(ErrorCodesAddShelf.PathDoesNotExist, result);
            shelfManagerMock.Verify(m =>
               m.AddShelf(It.IsAny<int>(), (It.IsAny<int>())), Times.Never); 
        }

        [TestMethod]
        private ErrorCodesAddShelf AddShelfNumberOne(Mock<IShelfManager> ShelfManagerMock)
        {
           var pathManagerMock = new Mock<IPathManager>();
           pathManagerMock.Setup(m =>
           m.GetPathByPathID(It.IsAny<int>()))
           .Returns(new Path
            {
                
                PathID =1
            });

            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, ShelfManagerMock.Object, null);
            var successfull = pathAndShelfAPI.AddShelf(1,1);
            return successfull;
        }



        [TestMethod]
        public void MoveShelfOk()
        {
            var pathManagerMock = new Mock<IPathManager>();
            var shelfManagerMock = new Mock<IShelfManager>();

            pathManagerMock.Setup(m =>
            m.GetPathByPathID(It.IsAny<int>()))
                .Returns(new Path { PathID = 2 });

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>(),It.IsAny<int>()))
                .Returns(new Shelf
                {
                    ShelfID = 2,
                    Path = new Path()
                });

            var pathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, shelfManagerMock.Object,null);
            var result = pathAndShelfAPI.MoveShelf(1, 1);
            Assert.AreEqual(ErrorCodesMoveShelf.Ok, result);
            shelfManagerMock.Verify(m =>
                m.MoveShelf(2, 2), Times.Once());


        }
        
        [TestMethod]
        public void TestRemoveExistingShelf()
        {
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var pathManagerMock = new Mock<IPathManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>(),It.IsAny<int>()))
                .Returns(new Shelf
                {
                    ShelfNumber = 4,
                    Books = new List<Book>()
                });

            var PathAndShelfAPI = new PathAndShelfAPI(pathManagerMock.Object, shelfManagerMock.Object, null);
            var successfull = PathAndShelfAPI.RemoveShelf(1,4,1);
            Assert.AreEqual(RemoveShelfErrorCodes.Ok, successfull);
            shelfManagerMock.Verify(m =>
                m.RemoveShelf(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]                   
        public void TestRemoveShelfwithBook()
        {
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new Shelf
                {
                    ShelfNumber = 3,
                    Books = new List<Book>
                    {
                        new Book()
                    }
                });

            var pathAndShelfAPI = new PathAndShelfAPI(null, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = pathAndShelfAPI.RemoveShelf(3,3,1);
            Assert.AreEqual(RemoveShelfErrorCodes.ShelfHasBooks, successfull);
            shelfManagerMock.Verify(m =>
               m.RemoveShelf(It.IsAny<int>()), Times.Never);

        }



        [TestMethod]
        public void TestRemoveNonexistingShelf()
        {
            var shelfManagerMock = new Mock<IShelfManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((Shelf)null);

            var pathAndShelfAPI = new PathAndShelfAPI(null, shelfManagerMock.Object,null);
            var successfull = pathAndShelfAPI.RemoveShelf(1,1,1);
            Assert.AreEqual(RemoveShelfErrorCodes.NoSuchShelf, successfull);
            shelfManagerMock.Verify(m =>
               m.RemoveShelf(It.IsAny<int>()), Times.Never);

        }

    }
}
