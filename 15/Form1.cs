using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game15 {

    public partial class Game15Form : Form {

        const int size = 4;
        const int buttonSize = 70;
        const int answer = 16;
        FButton[,] buttons;
        int corrects;

        public Game15Form() {
            StartInit();
            InitializeComponent();
        }

        void StartInit() {
            corrects = 0;
            if (buttons != null)
                for (int i = 0; i < size; i++)
                    for (int k = 0; k < size; k++)
                        Controls.Remove(buttons[i, k]);
            buttons = new FButton[size, size];
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            Random random = new Random();
            for (int n = 0; n < 38; n++) {
                int first = random.Next(15);
                int second = random.Next(15);
                int temp = numbers[first];
                numbers[first] = numbers[second];
                numbers[second] = temp;
            }
            int pairs = 0;
            for (int n = 0; n < numbers.Length - 2; n++)
                for (int m = n + 1; m < numbers.Length - 1; m++)
                    if (numbers[n] > numbers[m])
                        pairs++;
            if (pairs % 2 == 1) {
                int temp = numbers[9];
                numbers[9] = numbers[10];
                numbers[10] = temp;
            }
            for (int i = 0; i < size; i++)
                for (int k = 0; k < size; k++) {
                    int index = i + k * size;
                    FButton b = new FButton(index + 1) { Width = buttonSize, Height = buttonSize, TabStop = false, X = i, Y = k };
                    b.Value = numbers[index];
                    if (b.Correct())
                        corrects++;
                    b.Font = new Font("Microsoft Sans Serif", 10f);
                    if (b.Value < 16)
                        b.Text = b.Value.ToString();
                    else
                        b.Enabled = false;
                    b.Location = new Point(i * buttonSize, k * buttonSize);
                    b.Click += ButtonClick;
                    Controls.Add(b);
                    buttons[i, k] = b;
                }
        }

        void ButtonClick(object sender, EventArgs e) {
            FButton b = (FButton)sender;
            if (b.X != 0 && !buttons[b.X - 1, b.Y].Enabled)
                SwapCheck(b, buttons[b.X - 1, b.Y]);
            else if (b.X != 3 && !buttons[b.X + 1, b.Y].Enabled)
                SwapCheck(b, buttons[b.X + 1, b.Y]);
            else if (b.Y != 0 && !buttons[b.X, b.Y - 1].Enabled)
                SwapCheck(b, buttons[b.X, b.Y - 1]);
            else if (b.Y != 3 && !buttons[b.X, b.Y + 1].Enabled)
                SwapCheck(b, buttons[b.X, b.Y + 1]);
        }

        void SwapCheck(FButton first, FButton second) {
            if (first.Correct())
                corrects--;
            if (second.Correct())
                corrects--;
            second.Value = first.Value;
            second.Text = second.Value.ToString();
            second.Enabled = true;
            first.Value = 16;
            first.Text = "";
            first.Enabled = false;
            if (first.Correct())
                corrects++;
            if (second.Correct())
                corrects++;
            if (corrects == answer) {
                WinForm win = new WinForm();
                win.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = win.ShowDialog();
                if (result == DialogResult.OK)
                    StartInit();
                else
                    Close();
            }
        }
    }
}
