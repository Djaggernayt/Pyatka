using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;


namespace PatnashkiNK
{
    public partial class MainPage : ContentPage
    {
        private List<Button> buttons;
        private int emptyIndex;

        public MainPage()
        {
            InitializeComponent();

            buttons = new List<Button>
            {
                Button1, Button2, Button3,
                Button4, Button5, Button6,
                Button7, Button8, Button9
            };

            emptyIndex = 8;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var buttonIndex = buttons.IndexOf(button);

            if (IsValidMove(buttonIndex))
            {
                SwapButtons(buttonIndex, emptyIndex);
                emptyIndex = buttonIndex;
            }

            CheckForWin();
        }

        private void RandomButton_Clicked(object sender, EventArgs e)
        {
            var random = new Random();

            // Shuffle the buttons
            for (int i = buttons.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                SwapButtons(i, j);
            }

            emptyIndex = buttons.FindIndex(b => b.Text == "");
        }

        private bool IsValidMove(int buttonIndex)
        {
            var emptyRow = emptyIndex / 3;
            var emptyColumn = emptyIndex % 3;

            var buttonRow = buttonIndex / 3;
            var buttonColumn = buttonIndex % 3;

            return (emptyRow == buttonRow && Math.Abs(emptyColumn - buttonColumn) == 1) ||
                   (emptyColumn == buttonColumn && Math.Abs(emptyRow - buttonRow) == 1);
        }

        private void SwapButtons(int index1, int index2)
        {
            var button1 = buttons[index1];
            var button2 = buttons[index2];

            var buttonText = button1.Text;
            button1.Text = button2.Text;
            button2.Text = buttonText;
        }

        private void CheckForWin()
        {
            var orderedButtons = buttons.OrderBy(b => b.Text).ToList();

            if (buttons.SequenceEqual(orderedButtons))
            {
                DisplayAlert("Congratulations", "You won!", "OK");
            }
        }
    }
}