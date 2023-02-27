using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PIN_Code
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // This is the stored PIN
        private string PIN = "012340";
        // This is the input PIN
        private string PIN_IN = "";
        // This just holds all the textboxes so they can be controlled with
        // just indexes
        private TextBox[] dBoxes = new TextBox[6];
        // This just holds all the buttons so they can be controlled with
        // just indexes
        private Button[] iButtons = new Button[12];
        // this is for the bonus attempt feature.
        int attempt = 3;

        public MainWindow()
        {
            InitializeComponent();

            #region Populate dBoxes
            dBoxes[0] = txtb_1;
            dBoxes[1] = txtb_2;
            dBoxes[2] = txtb_3;
            dBoxes[3] = txtb_4;
            dBoxes[4] = txtb_5;
            dBoxes[5] = txtb_6;
            #endregion

            #region Populate iButtons
            iButtons[0] = btn_0;
            iButtons[1] = btn_1;
            iButtons[2] = btn_2;
            iButtons[3] = btn_3;
            iButtons[4] = btn_4;
            iButtons[5] = btn_5;
            iButtons[6] = btn_6;
            iButtons[7] = btn_7;
            iButtons[8] = btn_8;
            iButtons[9] = btn_9;
            iButtons[10] = btn_10;
            iButtons[11] = btn_11;
            #endregion

            randomizeButtons();
            displayPinIn();
        }

        /// <summary>
        /// this method just randomizes the buttons
        /// </summary>
        private void randomizeButtons()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            Random rnd = new Random();
            int temp = 0;

            for(int x = 0; x < 10; x++)
            {
                temp = rnd.Next(nums.Count);
                iButtons[x].Content = nums[temp];
                nums.RemoveAt(temp);
            }
        }

        /// <summary>
        /// This method just changes the displayed pin
        /// </summary>
        private void displayPinIn()
        {
            for(int x = 0; x < dBoxes.Length; x++)
            {
                if (padPIN()[x] == ' ')
                    dBoxes[x].Text = "*";
                else
                    dBoxes[x].Text = padPIN()[x] + "";
            }
        }

        /// <summary>
        /// This method just pads the input pin with spaces if the input PIN
        /// is not yet 6 characters long without changing the input PIN
        /// </summary>
        /// <returns>returns the padded input pin</returns>
        private string padPIN()
        {
            string paddedPin = PIN_IN;
            while(paddedPin.Length < PIN.Length)
                paddedPin += " ";

            return paddedPin;
        }

        /// <summary>
        /// This manages the 0-9 buttons for the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keypadPress(object sender, RoutedEventArgs e)
        {
            if(PIN_IN.Length < 6)
                PIN_IN += ((Button)sender).Content;
            displayPinIn();
            randomizeButtons();
        }

        /// <summary>
        /// This is cancel, this just clears the input PIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_10_Click(object sender, RoutedEventArgs e)
        {
            PIN_IN = "";
            displayPinIn();
        }

        /// <summary>
        /// This checks and compares the input PIN with the stored PIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            // compare
            if (PIN == PIN_IN)
            {
                MessageBox.Show("You have entered the correct PIN!");
                attempt = 3;
            }
            else
            {
                MessageBox.Show("You have entered the wrong PIN!");
                PIN_IN = "";
                displayPinIn();
                attempt--;
                if (attempt == 0)
                {
                    MessageBox.Show("You have reached the maximum number of attempts! Program terminating!");
                    this.Close();
                }
            }
        }
    }
}
