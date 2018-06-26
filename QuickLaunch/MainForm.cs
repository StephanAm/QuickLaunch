using QuickLaunchManager;
using QuickLaunchManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickLaunch
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Image> _images = new Dictionary<string, Image>();
        private readonly QuickLaunchApi _api;
        private bool allowClose = false;
        public MainForm(QuickLaunchApi api)
        {
            _api = api;
            InitializeComponent();
            LoadImages();
            loadData();
        }
        private void loadData()
        {
            this.handlerColumn.DataSource = _api.GetHandlerKeys();
            this.quickLaunchItemBindingSource.DataSource = _api.GetAllItems();
            popuLateContextMenu();
        }
        private void popuLateContextMenu()
        {
            this.contextMenuStrip.Items.Clear();
            this.contextMenuStrip.Items.Add(this.fixedContextMenuItems);
            this.contextMenuStrip.Items.Add(new ToolStripSeparator());
            var items = _api.GetAllItems().GroupBy(i => i.Group ?? i.Handler);

            foreach(var group in items)
            {

                var menuItem = new ToolStripMenuItem(group.Key);
                foreach (var item in group)
                {
                    menuItem.DropDownItems.Add(MapToToolStripMenuItem(item));
                }
                contextMenuStrip.Items.Add(menuItem);
            }
        }
        private void LoadImages()
        {
            foreach (var handlerInfo in _api.GetHandlerInfo())
            {
                _images[handlerInfo.HandlerKey] = handlerInfo.HandlerIcon == null
                    ? null
                    : Image.FromFile(handlerInfo.HandlerIcon);
            }
        }
        private ToolStripMenuItem MapToToolStripMenuItem(QuickLaunchItem item)
        {
            var handlerInfo = _api.GetHandlerInfo(item.Handler);
            var image = _images[handlerInfo.HandlerKey];
            return new ToolStripMenuItem(item.DisplayName, image, (s, e) => _api.Handle(item));
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _api.UpdateOrCreate(quickLaunchItemBindingSource.List.Cast<QuickLaunchItem>());
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (allowClose) return;
            e.Cancel = true;
            this.ShowInTaskbar = false;
            this.Visible = false;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.allowClose = true;
            this.Close();
        }
    }
}
