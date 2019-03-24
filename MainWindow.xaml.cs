using ContextEditor10.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
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

namespace ContextEditor10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        RegeditManager manager = new RegeditManager();
        private string currentItem = "";
        public MainWindow()
        {


            
            InitializeComponent();
            initList();


        }
        
        private void initList()
        {
            manager.initCollection();
            lv1.ItemsSource = manager.ContextMenuData;

        }

        

        private void lvChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                currentItem = e.AddedItems[0].ToString();
            }
            catch (Exception exc)
            {
                currentItem = "";
            }
            //MessageBox.Show(e.AddedItems[0].ToString());
        }

        

        

        private void deleteItem(object sender, RoutedEventArgs e)
        {
            manager.deleteItemByName(currentItem);
            initList();
        }

        private void addItem(object sender, RoutedEventArgs e)
        {
            manager.addItem(itemName.Text, itemCommand.Text, itemIcon.Text);
            initList();
        }
    }
}
