using System.Windows.Forms;

namespace Game15 {

    class FButton : Button {

        public int Value { get; set; }
        public int Answer { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }

        public FButton(int answer) 
            : base() {
            Answer = answer;
        }

        public bool Correct() {
            return Value == Answer;
        }

    }

}
