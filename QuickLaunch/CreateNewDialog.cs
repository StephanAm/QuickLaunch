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
    public partial class CreateNewDialog : Form
    {
        public QuickLaunchItem Item { get; private set; }
        public CreateNewDialog(IEnumerable<HandlerInfo> handlerInfo)
        {
            InitializeComponent();
            comboBoxType.Items.AddRange(handlerInfo.Select(h => new ComboBoxItem {
                Text = h.HandlerName,
                Value = h
            }).ToArray());        
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Item = new QuickLaunchItem();
            Item.DisplayName = textBoxDisplayName.Text;
            Item.Group = textBoxGroup.Text;
            Item.Group = string.IsNullOrWhiteSpace(Item.Group) ? null : Item.Group;
            Item.Handler = ((ComboBoxItem)comboBoxType.SelectedItem).Value.HandlerKey;
            Item.URI = textBoxResource.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
    class ComboBoxItem
    {
        public string Text { get; set; }
        public HandlerInfo Value { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
