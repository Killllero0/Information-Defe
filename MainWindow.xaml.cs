using System.Numerics;
using System.Printing;
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

namespace Защита_Информаций
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = InputBox.Text.ToLower();
            string key = KeyBox.Text.ToLower();
            bool operation = false;
            // Зашифровать
            if (CryptText.IsChecked == true)
            {
                operation = true;
            }
            Lab1 code = new Lab1(text, key, operation);
            decodedText.Text = code.getData();
            originalText.Text = text;

        }
    }
}