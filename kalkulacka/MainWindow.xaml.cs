using kalkulacka.src;
using System.Collections.Generic;
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
            logic = new MainLogic(TextField, HistoryPanel);
        }

        private void InputButton(object sender, RoutedEventArgs e)
        {
            if(TextField.Text.Length>0)
            if (MainLogic.IsOperator(TextField.Text[TextField.Text.Length - 1]) && MainLogic.IsOperator(((Button)sender).Content.ToString()[0]))
            {
                return;
            }
            //rozdělení výrazu na podřetězce
            string LastExpression = TextField.Text.Split('+','-','*','/').Last();
            //pokud poslední část výrazu již obsahuje desetinnou tečku neni možné vložit další
            if (LastExpression.Contains(',') && ((Button)sender).Content.ToString() == ",")
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
