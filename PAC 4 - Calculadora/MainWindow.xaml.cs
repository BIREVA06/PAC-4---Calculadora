using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace PAC_4___Calculadora
{
    /// <summary>
    /// Lògica d'interacció per a MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Expressió interna en format invariant (punt ".")
        private string expression = string.Empty;

        /// <summary>
        /// Constructor: vincula esdeveniments
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Dígits i punt decimal
            Btn0.Click += Input_Click;
            Btn1.Click += Input_Click;
            Btn2.Click += Input_Click;
            Btn3.Click += Input_Click;
            Btn4.Click += Input_Click;
            Btn5.Click += Input_Click;
            Btn6.Click += Input_Click;
            Btn7.Click += Input_Click;
            Btn8.Click += Input_Click;
            Btn9.Click += Input_Click;
            BtnDecimal.Click += Input_Click; // Afegeix "." a expression

            // Operadors
            BtnPlus.Click += Operator_Click;
            BtnMinus.Click += Operator_Click;
            BtnMultiply.Click += Operator_Click;
            BtnDivide.Click += Operator_Click;

            // Backspace (esborra últim caràcter)
            BtnBack.Click += Backspace_Click;

            // Neteja completa
            BtnClear.Click += Clear_Click;
            // Igual
            BtnEquals.Click += Equals_Click;
        }

        /// <summary>
        /// Afegeix dígit o punt a l'expressió i actualitza el display segons la cultura local
        /// </summary>
        private void Input_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                string content = btn.Content.ToString();
                if (content == ".")
                {
                    // Només un punt per nombre
                    int lastOp = expression.LastIndexOfAny(new char[] { '+', '-', '*', '/' });
                    string lastNumber = lastOp >= 0 ? expression.Substring(lastOp + 1) : expression;
                    if (lastNumber.Contains('.')) return;
                    expression += ".";
                }
                else
                {
                    expression += content;
                }
                char dec = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                DisplayTextBox.Text = expression.Replace('.', dec);
            }
        }

        /// <summary>
        /// Afegeix operador amb validació i mapeig a caràcters invariant
        /// </summary>
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (string.IsNullOrEmpty(expression) || "+-*/".Contains(expression[^1]))
                {
                    DisplayTextBox.Text = "Error";
                    expression = string.Empty;
                    return;
                }
                string op = btn.Content.ToString();
                switch (op)
                {
                    case "×": op = "*"; break;
                    case "÷": op = "/"; break;
                    case "−": op = "-"; break;
                }
                expression += op;
                string disp = expression.Replace('*', '×')
                                         .Replace('/', '÷')
                                         .Replace('-', '−');
                char dec = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                DisplayTextBox.Text = disp.Replace('.', dec);
            }
        }

        /// <summary>
        /// Esborra l'últim caràcter de l'expressió
        /// </summary>
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                expression = expression[..^1];
                char dec = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                DisplayTextBox.Text = expression.Replace('.', dec);
            }
        }

        /// <summary>
        /// Neteja completament l'expressió i el display
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            expression = string.Empty;
            DisplayTextBox.Clear();
        }

        /// <summary>
        /// Avalua l'expressió, gestiona decimals, infinits i errors de divisió per zero
        /// </summary>
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(expression) || "+-*/".Contains(expression[^1]))
                    throw new SyntaxErrorException();

                var table = new DataTable();
                object raw = table.Compute(expression, string.Empty);
                double val = Convert.ToDouble(raw);
                if (double.IsInfinity(val)) throw new DivideByZeroException();

                string inv = val.ToString(CultureInfo.InvariantCulture);
                string disp = val.ToString(CultureInfo.CurrentCulture);
                expression = inv;
                DisplayTextBox.Text = disp;
            }
            catch (DivideByZeroException)
            {
                DisplayTextBox.Text = "Error: Divisió per zero";
                expression = string.Empty;
            }
            catch
            {
                DisplayTextBox.Text = "Error";
                expression = string.Empty;
            }
        }
    }
}
