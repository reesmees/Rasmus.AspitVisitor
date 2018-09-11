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
using Rasmus.AspitVisitor.DataAccess.EF;
using Rasmus.AspitVisitor.Business;

namespace Rasmus.AspitVisitor.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AspitVisitorContext db = new AspitVisitorContext();
        private DataHandler handler;


        public MainWindow()
        {
            InitializeComponent();
            handler = new DataHandler(db);
        }

        private void btnVisitor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow(handler);
            adminWindow.ShowDialog();
        }
    }
}
