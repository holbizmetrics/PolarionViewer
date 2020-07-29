using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGMLViewerLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModelViewer
    {
        void AddNode(string node);
        void AddEdge(string source, string target);
    }
}
