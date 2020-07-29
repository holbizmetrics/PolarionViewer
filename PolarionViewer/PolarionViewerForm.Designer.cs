namespace PolarionViewer
{
    partial class PolarionViewerForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxQuery = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cobQueryHistory = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.createItemsFromTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createItemsFromTextBoxToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyWorkItemRepresentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCompleteItemRepresentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.Location = new System.Drawing.Point(0, 24);
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.Size = new System.Drawing.Size(800, 70);
            this.textBoxQuery.TabIndex = 0;
            this.textBoxQuery.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cobQueryHistory);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxQuery);
            this.splitContainer1.Size = new System.Drawing.Size(562, 491);
            this.splitContainer1.SplitterDistance = 102;
            this.splitContainer1.TabIndex = 1;
            // 
            // cobQueryHistory
            // 
            this.cobQueryHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobQueryHistory.FormattingEnabled = true;
            this.cobQueryHistory.Location = new System.Drawing.Point(0, 73);
            this.cobQueryHistory.Name = "cobQueryHistory";
            this.cobQueryHistory.Size = new System.Drawing.Size(800, 21);
            this.cobQueryHistory.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createItemsFromTextBoxToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(166, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // createItemsFromTextBoxToolStripMenuItem
            // 
            this.createItemsFromTextBoxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createItemsFromTextBoxToolStripMenuItem1});
            this.createItemsFromTextBoxToolStripMenuItem.Name = "createItemsFromTextBoxToolStripMenuItem";
            this.createItemsFromTextBoxToolStripMenuItem.Size = new System.Drawing.Size(158, 20);
            this.createItemsFromTextBoxToolStripMenuItem.Text = "Create Items from TextBox";
            // 
            // createItemsFromTextBoxToolStripMenuItem1
            // 
            this.createItemsFromTextBoxToolStripMenuItem1.Name = "createItemsFromTextBoxToolStripMenuItem1";
            this.createItemsFromTextBoxToolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.createItemsFromTextBoxToolStripMenuItem1.Text = "Create Items From TextBox";
            this.createItemsFromTextBoxToolStripMenuItem1.Click += new System.EventHandler(this.createItemsFromTextBoxToolStripMenuItem1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyLinkToolStripMenuItem,
            this.copyIDToolStripMenuItem,
            this.copyTextToolStripMenuItem,
            this.copyWorkItemRepresentationToolStripMenuItem,
            this.copyCompleteItemRepresentationToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(267, 114);
            // 
            // copyLinkToolStripMenuItem
            // 
            this.copyLinkToolStripMenuItem.Name = "copyLinkToolStripMenuItem";
            this.copyLinkToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.copyLinkToolStripMenuItem.Text = "Copy Link";
            this.copyLinkToolStripMenuItem.Click += new System.EventHandler(this.copyLinkToolStripMenuItem_Click);
            // 
            // copyIDToolStripMenuItem
            // 
            this.copyIDToolStripMenuItem.Name = "copyIDToolStripMenuItem";
            this.copyIDToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.copyIDToolStripMenuItem.Text = "Copy ID";
            this.copyIDToolStripMenuItem.Click += new System.EventHandler(this.copyIDToolStripMenuItem_Click);
            // 
            // copyTextToolStripMenuItem
            // 
            this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
            this.copyTextToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.copyTextToolStripMenuItem.Text = "Copy Text";
            this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // copyWorkItemRepresentationToolStripMenuItem
            // 
            this.copyWorkItemRepresentationToolStripMenuItem.Name = "copyWorkItemRepresentationToolStripMenuItem";
            this.copyWorkItemRepresentationToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.copyWorkItemRepresentationToolStripMenuItem.Text = "Copy WorkItem Representation";
            this.copyWorkItemRepresentationToolStripMenuItem.Click += new System.EventHandler(this.copyWorkItemRepresentationToolStripMenuItem_Click);
            // 
            // copyCompleteItemRepresentationToolStripMenuItem
            // 
            this.copyCompleteItemRepresentationToolStripMenuItem.Name = "copyCompleteItemRepresentationToolStripMenuItem";
            this.copyCompleteItemRepresentationToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.copyCompleteItemRepresentationToolStripMenuItem.Text = "Copy Complete Item Representation";
            this.copyCompleteItemRepresentationToolStripMenuItem.Click += new System.EventHandler(this.copyCompleteItemRepresentationToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer2.Size = new System.Drawing.Size(866, 491);
            this.splitContainer2.SplitterDistance = 562;
            this.splitContainer2.TabIndex = 2;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(300, 491);
            this.propertyGrid1.TabIndex = 0;
            // 
            // PolarionViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 491);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PolarionViewerForm";
            this.Text = "Polarion Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textBoxQuery;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cobQueryHistory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyWorkItemRepresentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyCompleteItemRepresentationToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createItemsFromTextBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createItemsFromTextBoxToolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}

