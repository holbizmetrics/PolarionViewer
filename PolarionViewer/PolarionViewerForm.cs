using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Codan.Argus.TestEnvironment.PolarionAPI;
using Codan.Argus.TestEnvironment.PolarionAPI.Tests;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using PolarionViewerLibrary;

using Color = Microsoft.Msagl.Drawing.Color;
using PolarionItem = PolarionViewerLibrary.PolarionItem;

namespace PolarionViewer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PolarionViewerForm : Form
    {
        Graph _graph = new Graph("Polarion View");
        WindowsFormsHost _viewer = new WindowsFormsHost();

        ElementHost _host = new ElementHost();
        GViewer _gviewer = new GViewer();

        CyclicColorConfiguration cyclicColorConfiguration = new CyclicColorConfiguration("Blub");

        public static string polarionMainURL = "https://polarion.codan.de/polarion/"; //"http://sr65dev04.codancompanies.com/polarion/";

        private PolarionItem _LastSelectedPolarionItem = null;
        private Node _LastSelectedNode = null;
        private string _LastSelectedURI = null;


        /// <summary>
        /// 
        /// </summary>
        public PolarionViewerForm()
        {

            cyclicColorConfiguration.Assign.Add("testPlan", Color.Gray);
            cyclicColorConfiguration.Assign.Add("meeting", Color.White);
            cyclicColorConfiguration.Assign.Add("testCaseAutomatic", Color.Blue);
            cyclicColorConfiguration.Assign.Add("problemReport", Color.Orange);
            cyclicColorConfiguration.Assign.Add("bildPipeline", Color.Azure);

            InitializeComponent();

            InitializeGraph();
            _host.Child = _viewer;
            _host.Dock = DockStyle.Fill;
            _host.Parent = splitContainer1.Panel2;

            bool isConnected = API.Connect(polarionMainURL, "sw.project.automation", "Codanargus1234");
            if (!isConnected) MessageBox.Show("Couldn't connect to polarion.");

            string query = textBoxQuery.Text;
            textBoxQuery.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    Thread thread = new Thread(new ThreadStart(NoParamsDelegateThread));
                    bool useThreads = true;
                    if (useThreads)
                    {
                        thread.Start();
                    }
                    else
                    {
                        NoParamsDelegateNoThread();
                    }
                    cobQueryHistory.Items.Add(textBoxQuery.Text);
                }
            };

            _viewer.Child = _gviewer;

            cobQueryHistory.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                string selectedQuery = cobQueryHistory.SelectedItem as string;
                if (string.IsNullOrEmpty(selectedQuery))
                {
                    textBoxQuery.Text = selectedQuery;
                }
            };

            _gviewer.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip1.Show(PointToScreen(e.Location));
                }
            };


            _gviewer.MouseClick += delegate (object sender, MouseEventArgs e)
            {
                GViewer viewer = sender as GViewer;
                if (!(viewer.SelectedObject is Node)) return;

                Node node = viewer.SelectedObject as Node;
                PolarionItem polarionItem = node.UserData as PolarionItem;

                _LastSelectedNode = node;
                _LastSelectedPolarionItem = polarionItem;
                _LastSelectedURI = Helper.GetURI(polarionItem.WorkItem.uri);

                this.propertyGrid1.SelectedObject = polarionItem.WorkItem;
            };

            _gviewer.MouseDoubleClick += delegate (object sender, MouseEventArgs e)
            {
                GViewer viewer = sender as GViewer;
                if (!(viewer.SelectedObject is Node)) return;

                Node node = viewer.SelectedObject as Node;
                PolarionItem polarionItem = node.UserData as PolarionItem;

                string uri = Helper.GetURI(polarionItem.WorkItem.uri);

                if (string.IsNullOrEmpty(uri)) return;

                Process.Start(uri);
                //...do works here
            };
        }

        private void InitializeGraph()
        {
            _gviewer.OutsideAreaBrush = Brushes.Gray;
            _gviewer.LayoutEditingEnabled = true;
            _gviewer.NavigationVisible = true;
            _gviewer.SaveButtonVisible = true;
            _gviewer.SaveGraphButtonVisible = true;
            _gviewer.ToolBarIsVisible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void NoParamsDelegateNoThread()
        {
            UpdateGraphByQuery(textBoxQuery.Text.TrimEnd(new char[] { '\n', '\r' }));
        }

        /// <summary>
        /// 
        /// </summary>
        public void NoParamsDelegateThread()
        {
            _gviewer.Invoke(new MethodInvoker(NoParamsDelegateNoThread));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void UpdateGraphByQuery(string text)
        {
            _graph.Edges.Clear();

            PolarionItem[] polarionItems = PolarionItem.GetItemsFromQuery(text);

            if (polarionItems == null)
            {
                _graph.Edges.Clear();
                return;
            }

            foreach (PolarionItem polarionItem in polarionItems)
            {
                if (polarionItem.WorkItem == null) continue;

                foreach (PolarionItem polarionChildItem in polarionItem.Childs)
                {
                    _graph.AddEdge($"{polarionItem.WorkItem.id}-{polarionItem.WorkItem.title}",
                        $"{polarionChildItem.WorkItem.id}-{polarionChildItem.WorkItem.title}");

                    Node node = _graph.FindNode($"{polarionChildItem.WorkItem.id}-{polarionChildItem.WorkItem.title}");
                    node.UserData = polarionChildItem;

                    Color color = Color.White;
                    if (polarionChildItem.WorkItem.type != null)
                    {
                        color = cyclicColorConfiguration.GetColor(polarionChildItem.WorkItem.type.id);
                    }
                    NodeStyle nodeStyle = new NodeStyle(color);
                    nodeStyle.ApplyToNode(node);
                }

                Node mainNode = _graph.FindNode($"{polarionItem.WorkItem.id}-{polarionItem.WorkItem.title}");
                if (mainNode != null)
                {
                    mainNode.UserData = polarionItem;
                }
                else
                {
                    //we shouldn't get here normally.
                }

            }
            _gviewer.Graph = null;
            _gviewer.Graph = _graph;

        }

        private void SaveAsBitmap()
        {
            //Microsoft.Glee.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Glee.GraphViewerGdi.GraphRenderer(graph);
            //renderer.CalculateLayout();
            //int width = 900;
            //Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), PixelFormat.Format32bppPArgb);
            //renderer.Render(bitmap);
            //bitmap.Save("test.png");
        }

        private void copyLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_LastSelectedURI);
        }

        private void copyIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_LastSelectedNode.Id);
        }

        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"{_LastSelectedPolarionItem.ID}-{_LastSelectedPolarionItem.Title}");
        }

        private void copyWorkItemRepresentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"{_LastSelectedPolarionItem.TextualRepresentation(false)}");
        }

        private void copyCompleteItemRepresentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"{_LastSelectedPolarionItem.TextualRepresentation(true)}");
        }

        private void createItemsFromTextBoxToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PolarionViewerAPI.CreateItems("testCaseAutomatic", this.textBoxQuery.Text);
        }
    }
}
