using QuickLaunchManager.Models;
using QuickLaunchManager.Validation;
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
        private readonly Func<QuickLaunchItem, OperationResult> _validationFunc;
        public QuickLaunchItem Item { get; private set; }
        public CreateNewDialog(
            IEnumerable<HandlerInfo> handlerInfo,
            Func<QuickLaunchItem,OperationResult> validationFunction)
        {
            _validationFunc = validationFunction;
            InitializeComponent();
            comboBoxType.Items.AddRange(handlerInfo.Select(h => new ComboBoxItem {
                Text = h.HandlerName,
                Value = h
            }).ToArray());        
        }

        private bool Validate(QuickLaunchItem item)
        {
            var result = _validationFunc(item);
            if (result == null) { return true; }
            else
            {
                MessageBox.Show(result.Message, result.Reference, MessageBoxButtons.OK);
                
            }
            return (result.Severity < Severity.Error);
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Func<string,string> nullifyIfBlank = (s) => string.IsNullOrWhiteSpace(s) ? null : s;
            Item = new QuickLaunchItem();
            Item.DisplayName = nullifyIfBlank(textBoxDisplayName.Text);
            Item.Group = nullifyIfBlank(textBoxGroup.Text);
            Item.Handler = ((ComboBoxItem)comboBoxType.SelectedItem)?.Value?.HandlerKey;
            Item.URI = textBoxResource.Text;
            if (Validate(Item))
            {
                this.DialogResult = DialogResult.OK;
            }
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
