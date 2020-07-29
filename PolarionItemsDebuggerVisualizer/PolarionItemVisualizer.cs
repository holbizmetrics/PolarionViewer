using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;
using TrackerService = net.seabay.polarion.Tracker;
using FastColoredTextBox = FastColoredTextBoxNS.FastColoredTextBox;

using PolarionItemsDebuggerVisualizer.Behaviors;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(PolarionItemsDebuggerVisualizer.DebuggerSide),
typeof(VisualizerObjectSource),
Target = typeof(TrackerService.WorkItem),
Description = "Polarion WorkItem Visualizer")]
namespace PolarionItemsDebuggerVisualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        private IBehavior _behavior = null;

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            TrackerService.WorkItem workItem = (TrackerService.WorkItem)objectProvider.GetObject();
            _behavior = new ObjectDumperBehavior(workItem);

            StringBuilder builder = new StringBuilder();

            Form form = new Form();
            form.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            SplitContainer splitContainer = new SplitContainer();

            FastColoredTextBox textBox = new FastColoredTextBox();
            textBox.ReadOnly = true;
            textBox.Text = _behavior.Text;
            textBox.Dock = DockStyle.Fill;

            splitContainer.Panel1.Controls.Add(textBox);

            PropertyGrid propertyGrid = new PropertyGrid();
            propertyGrid.SelectedObject = workItem;
            propertyGrid.Dock = DockStyle.Fill;

            splitContainer.Panel2.Controls.Add(propertyGrid);
            splitContainer.Dock = DockStyle.Fill;

            splitContainer.Parent = form;

            windowService.ShowDialog(form);
        }

        public void Approvals()
        {

        }

        public void Attachments()
        {

        }

        public void Categories()
        {

        }

        public void Comments()
        {

        }

        public void ExternallyLinkedWorkItem()
        {

        }

        public void Hyperlinks()
        {

        }

        public void LinkedRevisions()
        {

        }

        public void LinkedRevisionsDerived()
        {
        }
    }
}
