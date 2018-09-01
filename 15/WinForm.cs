using System;
using System.Windows.Forms;

namespace Game15 {

    public partial class WinForm : Form {

        public WinForm() {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ExitBottom_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
