using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Taschenrechner
{
    public partial class MainWindow : Window
    {
        private string _currentInput = "";
        private string _operator = "";
        private double _firstOperand = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var content = button.Content.ToString();
                if (content == "." && _currentInput.Contains("."))
                    return; // Verhindert mehrere Dezimalpunkte

                _currentInput += content;
                Display.Text = _currentInput;
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (double.TryParse(_currentInput, out _firstOperand))
                {
                    _operator = button.Content.ToString();
                    _currentInput = "";
                    Display.Text = _operator;
                }
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentInput) && double.TryParse(_currentInput, out double secondOperand))
            {
                double result = 0;
                switch (_operator)
                {
                    case "+":
                        result = _firstOperand + secondOperand;
                        break;
                    case "-":
                        result = _firstOperand - secondOperand;
                        break;
                    case "*":
                        result = _firstOperand * secondOperand;
                        break;
                    case "/":
                        result = secondOperand != 0 ? _firstOperand / secondOperand : 0;
                        break;
                }
                Display.Text = result.ToString();
                _currentInput = result.ToString();
                _operator = "";
                _firstOperand = 0;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                Display.Text = string.IsNullOrEmpty(_currentInput) ? "0" : _currentInput;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                _currentInput += (e.Key - Key.D0).ToString();
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                _currentInput += (e.Key - Key.NumPad0).ToString();
            }
            else if (e.Key == Key.Back)
            {
                if (!string.IsNullOrEmpty(_currentInput))
                {
                    _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                }
            }
            else if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                if (!_currentInput.Contains("."))
                {
                    _currentInput += ".";
                }
            }
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Equals_Click(null, null);
                return;
            }
            else if (e.Key == Key.C)
            {
                ClearAll();
                return;
            }
            else if (e.Key == Key.Add || e.Key == Key.OemPlus)
            {
                Operator_Click(new Button { Content = "+" }, null);
                return;
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                Operator_Click(new Button { Content = "-" }, null);
                return;
            }
            else if (e.Key == Key.Multiply)
            {
                Operator_Click(new Button { Content = "*" }, null);
                return;
            }
            else if (e.Key == Key.Divide)
            {
                Operator_Click(new Button { Content = "/" }, null);
                return;
            }

            Display.Text = string.IsNullOrEmpty(_currentInput) ? "0" : _currentInput;
        }

        private void ClearAll()
        {
            _currentInput = "";
            _operator = "";
            _firstOperand = 0;
            Display.Text = "0";
        }
    }
}