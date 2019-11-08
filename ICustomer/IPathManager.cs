using System;
using System.Collections.Generic;
using System.Text;

namespace DataInterface
{
    public interface IPathManager
    {
       
         Path GetPathByPathID(int pathID); //pathNumber
        void AddPath(int pathNumber);
        void RemovePath(int pathID);
    }
}
