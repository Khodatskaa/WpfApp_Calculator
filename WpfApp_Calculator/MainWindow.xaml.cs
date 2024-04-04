using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_Calculator
{
    public partial class MainWindow : Window
    {
        private string previousOperation = "";
        private string currentNumber = "";
        private double result = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateDisplay()
        {
            previousOperationTextBox.Text = previousOperation;
            currentNumberTextBox.Text = currentNumber;
        }

        private void AppendDigit(char digit)
        {
            if (!(digit == '0' && currentNumber == "0"))
            {
                currentNumber += digit;
                UpdateDisplay();
            }
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            AppendDigit(button.Content.ToString()[0]);
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!currentNumber.Contains("."))
            {
                currentNumber += ".";
                UpdateDisplay();
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (!string.IsNullOrWhiteSpace(currentNumber))
            {
                if (previousOperation != "")
                {
                    double num;
                    if (double.TryParse(currentNumber, out num))
                    {
                        Calculate();
                    }
                    else
                    {
                        MessageBox.Show("Invalid input!");
                        return;
                    }
                }
                previousOperation = currentNumber + " " + button.Content.ToString();
                currentNumber = "";
                UpdateDisplay();
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currentNumber))
            {
                double num;
                if (double.TryParse(currentNumber, out num))
                {
                    Calculate();
                    previousOperation = "";
                    UpdateDisplay();
                }
                else
                {
                    MessageBox.Show("Invalid input!");
                    return;
                }
            }
        }

        private void Calculate()
        {
            double num;
            if (!double.TryParse(currentNumber, out num))
            {
                MessageBox.Show("Invalid input!");
                return;
            }

            string[] operationParts = previousOperation.Split(' ');
            if (operationParts.Length != 2)
            {
                MessageBox.Show("Invalid operation!");
                return;
            }

            double previousNum;
            if (!double.TryParse(operationParts[0], out previousNum))
            {
                MessageBox.Show("Invalid input!");
                return;
            }

            string operation = operationParts[1];
            switch (operation)
            {
                case "+":
                    result = previousNum + num;
                    break;
                case "-":
                    result = previousNum - num;
                    break;
                case "*":
                    result = previousNum * num;
                    break;
                case "/":
                    if (num != 0)
                        result = previousNum / num;
                    else
                    {
                        MessageBox.Show("Cannot divide by zero!");
                        return;
                    }
                    break;
                default:
                    MessageBox.Show("Invalid operation!");
                    return;
            }

            currentNumber = result.ToString();
            previousOperation = "";
            result = 0;
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            currentNumber = "";
            UpdateDisplay();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            previousOperation = "";
            currentNumber = "";
            UpdateDisplay();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber.Length > 0)
            {
                currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
                UpdateDisplay();
            }
        }
    }
}