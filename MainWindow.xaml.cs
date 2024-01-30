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
            string inputText = TextBoxContent.Text;
            string key = TextBoxKey.Text;
            Lab1 test = new Lab1(inputText, key);
            inputText = TextBoxContent.Text;
            EncodedText.Text = test.GetEncodeString();
            DecodedText.Text = test.GetDecodeSring();
        }
    }
}