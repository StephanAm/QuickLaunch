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
        private readonly Api _api = new Api();
        private Dictionary<string, Image> _images = new Dictionary<string, Image>();
        private readonly QuickLaunchApi _quickLaunchApi;
        private bool allowClose = false;
        public MainForm(QuickLaunchApi api)
        {
            _quickLaunchApi = api;
            InitializeComponent();
            LoadImages();
            loadData();
        }
        private void loadData()
        {
            this.handlerColumn.DataSource = _quickLaunchApi.GetHandlerKeys();
            this.quickLaunchItemBindingSource.DataSource = _quickLaunchApi.GetAllItems();
            popuLateContextMenu();
        }
        private void popuLateContextMenu()
        {
            this.contextMenuStrip.Items.Clear();
            this.contextMenuStrip.Items.Add(this.fixedContextMenuItems);
            this.contextMenuStrip.Items.Add(new ToolStripSeparator());
            var items = _quickLaunchApi.GetAllItems().GroupBy(i => i.Group ?? i.Handler);

            foreach (var group in items)
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
            foreach (var handlerInfo in _quickLaunchApi.GetHandlerInfo())
            {
                _images[handlerInfo.HandlerKey] = handlerInfo.HandlerIcon == null
                    ? null
                    : Image.FromFile(handlerInfo.HandlerIcon);
            }
        }
        private ToolStripMenuItem MapToToolStripMenuItem(QuickLaunchItem item)
        {
            var handlerInfo = _quickLaunchApi.GetHandlerInfo(item.Handler);
            var image = _images[handlerInfo.HandlerKey];
            return new ToolStripMenuItem(item.DisplayName, image, (s, e) => _quickLaunchApi.Handle(item));
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _quickLaunchApi.UpdateOrCreate(quickLaunchItemBindingSource.List.Cast<QuickLaunchItem>());
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.Visible = false;
            }
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

        private void autoStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch(MessageBox.Show("Start automatically when windows starts?",
                "Auto Start",
                MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:_api.SetStartWithWindows(true);break;
                case DialogResult.No:_api.SetStartWithWindows(false);break;
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            createNewItemDialogueAction();
        }
        private void createNewItemDialogueAction()
        {
            var dialog = new CreateNewDialog(
                _quickLaunchApi.GetHandlerInfo(),
                _quickLaunchApi.Validate);
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _quickLaunchApi.AddItem(dialog.Item);
            }
            loadData();
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createNewItemDialogueAction();
        }
    }
}
