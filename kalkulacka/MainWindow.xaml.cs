using kalkulacka.src;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace kalkulacka
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainLogic logic;
        public MainWindow()
        {          
            InitializeComponent();
            logic = new MainLogic(TextField);
        }

        private void NumberButton(object sender, RoutedEventArgs e)
        {
            if (logic.IsResultOnDisplay)
            {
                TextField.Text = "";
                logic.IsResultOnDisplay = false;
            }
            if ( ((Button)sender).Content.ToString() == "." && TextField.Text.Contains('.'))
            {
                return;
            }
            TextField.Text += ((Button)sender).Content;
        }
        private void OperationButton(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Content)
            {
                case "=":
                    logic.ResultOperation();
                    break;
                case "+":
                    logic.MathOperation(EOperation.ADD);
                    break;
                case "-":
                    logic.MathOperation(EOperation.SUB);
                    break;
                case "*":
                    logic.MathOperation(EOperation.MUL);
                    break;
                case "/":
                    logic.MathOperation(EOperation.DIV);
                    break;
                case "AC":
                    logic.ClearOperation();
                    break;
                case "+/-":
                    logic.Negation();
                    break;
            }
        }
    }
}
