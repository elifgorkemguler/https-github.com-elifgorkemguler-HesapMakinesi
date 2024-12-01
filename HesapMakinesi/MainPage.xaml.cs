using System;
using Microsoft.Maui.Controls;

namespace HesapMakinesi

{
    public partial class MainPage : ContentPage
    {
        private string _currentInput = ""; 
        private string _firstOperand = "";
        private string _operator = "";     

        public MainPage()
        {
            InitializeComponent();
        }

        // Sayı butonuna tıklama işlemi
        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            _currentInput += button.Text;
            UpdateCurrentOperationLabel();
        }

       
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null || string.IsNullOrEmpty(_currentInput)) return;

            _firstOperand = _currentInput;
            _operator = button.Text;
            _currentInput = ""; 
            UpdateCurrentOperationLabel();
        }

      
        private void OnEqualClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_firstOperand) || string.IsNullOrEmpty(_currentInput) || string.IsNullOrEmpty(_operator))
            {
                ResultLabel.Text = "Hata";
                return;
            }

            double first = double.Parse(_firstOperand);
            double second = double.Parse(_currentInput);
            double result = 0;

            switch (_operator)
            {
                case "+":
                    result = first + second;
                    break;
                case "-":
                    result = first - second;
                    break;
                case "x":
                    result = first * second;
                    break;
                case "÷":
                    if (second == 0)
                    {
                        ResultLabel.Text = "Hata: Sıfıra bölünemez!";
                        return;
                    }
                    result = first / second;
                    break;
            }

            _currentInput = result.ToString();
            _firstOperand = "";
            _operator = "";
            UpdateResultLabel();
            CurrentOperationLabel.Text = ""; 
        }

        
        private void OnClearClicked(object sender, EventArgs e)
        {
            _currentInput = "";
            _firstOperand = "";
            _operator = "";
            UpdateResultLabel();
            CurrentOperationLabel.Text = "";
        }

        
        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                UpdateCurrentOperationLabel();
            }
        }

        
        private void UpdateResultLabel()
        {
            ResultLabel.Text = string.IsNullOrEmpty(_currentInput) ? "0" : _currentInput;
        }

        
        private void UpdateCurrentOperationLabel()
        {
            CurrentOperationLabel.Text = $"{_firstOperand} {_operator} {_currentInput}";
        }
    }
}
