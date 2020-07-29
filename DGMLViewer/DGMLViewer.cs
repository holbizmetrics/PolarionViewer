using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace DGMLViewerLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class DGMLViewer : IModelViewer
    {
        private WindowsFormsHost m_Viewer = new WindowsFormsHost();
        private ElementHost m_Host = new ElementHost();
        private Graph m_Graph = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="graph"></param>
        public DGMLViewer(string graphName = "graph", Graph graph = null)
        {
            Initialize(graphName, graph);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="graph"></param>
        protected virtual void Initialize(string graphName = "graph", Graph graph = null)
        {
            SetGraph(graphName, graph);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphName"></param>
        /// <param name="graph"></param>
        private void SetGraph(string graphName, Graph graph)
        {
            if (graph == null)
            {
                m_Graph = new Graph(graphName);
            }
            else
            {
                m_Graph = graph;
            }

            ////The easiest way to build a graph is to create the edges of the graph like in the example below.
            //m_Graph.AddEdge("S35", "36");
            //m_Graph.AddEdge("S35", "43");
            //m_Graph.AddEdge("S30", "31");
            //m_Graph.AddEdge("S30", "33");
            //m_Graph.AddNode(CreateSourceNode("Code Review Request 32029:\nReview of SetSmartTracTest\n, ShowMessageScreenTest\n and GetWeightMeasurementTest\n (closed)"));


            GViewer gViewer = new GViewer();
            //gViewer.SelectionChanged += new EventHandler(gViewer_SelectionChanged);
            gViewer.Graph = m_Graph;

            m_Viewer.Child = gViewer;

            m_Host.Child = m_Viewer;
            m_Host.Dock = DockStyle.Fill;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void AddEdge(string source, string target)
        {
            m_Graph.AddEdge(source, target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="label"></param>
        public void AddEdge(string source, string target, string label)
        {
            m_Graph.AddEdge(source, target, label);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(Node node)
        {
            m_Graph.AddNode(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeId"></param>
        public void AddNode(string nodeId)
        {
            m_Graph.AddNode(nodeId);
        }

        /// <summary>
        /// 
        /// </summary>
        public Graph Graph
        {
            set { m_Graph = value; }
            get { return m_Graph; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementHost ElementHost
        {
            get { return m_Host; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static Node CreateSourceNode(string id)
        {
            Node node = CreateSourceNode(new Node(id));
            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static Node CreateSourceNode(Node node)
        {
            node.Attr.Shape = Shape.Box;
            node.Attr.XRadius = 3;
            node.Attr.YRadius = 3;
            node.Attr.FillColor = Color.Green;
            node.Attr.LineWidth = 1;

            node.UserData = "UserData present";
            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gViewer_SelectionChanged(object sender, EventArgs e)
        {

            //    if (selectedObject != null)
            //    {
            //        if (selectedObject is Edge)
            //            (selectedObject as Edge).Attr = selectedObjectAttr as EdgeAttr;
            //        else if (selectedObject is Node)
            //            (selectedObject as Node).Attr = selectedObjectAttr as NodeAttr;

            //        selectedObject = null;
            //    }
        }
    }
}
