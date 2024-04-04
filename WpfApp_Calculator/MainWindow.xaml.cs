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
        private string currentInput = "";
        private double currentResult = 0;
        private char lastOperator = ' ';

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            currentInput += button.Content.ToString();
            displayTextBox.Text = currentInput;
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (!string.IsNullOrWhiteSpace(currentInput))
            {
                if (double.TryParse(currentInput, out double input))
                {
                    if (lastOperator == '+')
                        currentResult += input;
                    else if (lastOperator == '-')
                        currentResult -= input;
                    else if (lastOperator == '*')
                        currentResult *= input;
                    else if (lastOperator == '/')
                    {
                        if (input != 0)
                            currentResult /= input;
                        else
                        {
                            MessageBox.Show("Cannot divide by zero!");
                            currentInput = "";
                            displayTextBox.Text = "";
                            return;
                        }
                    }
                    else
                        currentResult = input;

                    lastOperator = Convert.ToChar(button.Content);
                    currentInput = "";
                    displayTextBox.Text = "";
                }
                else
                {
                    MessageBox.Show("Invalid input!");
                }
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currentInput))
            {
                if (double.TryParse(currentInput, out double input))
                {
                    if (lastOperator == '+')
                        currentResult += input;
                    else if (lastOperator == '-')
                        currentResult -= input;
                    else if (lastOperator == '*')
                        currentResult *= input;
                    else if (lastOperator == '/')
                    {
                        if (input != 0)
                            currentResult /= input;
                        else
                        {
                            MessageBox.Show("Cannot divide by zero!");
                            currentInput = "";
                            displayTextBox.Text = "";
                            return;
                        }
                    }
                    currentInput = currentResult.ToString();
                    displayTextBox.Text = currentInput;
                    currentResult = 0;
                    lastOperator = ' ';
                }
                else
                {
                    MessageBox.Show("Invalid input!");
                }
            }
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains("."))
            {
                currentInput += ".";
                displayTextBox.Text = currentInput;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            currentResult = 0;
            lastOperator = ' ';
            displayTextBox.Text = "";
        }
    }
}