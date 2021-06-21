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
using demomane.DB;

namespace demomane.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage() 
        {
            InitializeComponent();

            Update();

            CbFilterCategory.ItemsSource = MainWindow.db.Category.ToList();
            CbFilterCategory.SelectedItem = MainWindow.db.Category.FirstOrDefault(g => g.TItle == "Все");
            CollectionView view = CollectionViewSource.GetDefaultView(LBView.ItemsSource) as CollectionView;
            view.Filter = c => CategoryFilter(c) && NameFilter(c);
        }

        bool CategoryFilter(object g) => (g as Product).Category == (CbFilterCategory.SelectedItem as Category) || (CbFilterCategory.SelectedItem as Category).TItle == "Все";

        public void Update()
        {
            LBView.ItemsSource = null;
            LBView.ItemsSource = MainWindow.db.Product.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage());
        }

        private void CbFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(LBView.ItemsSource).Refresh();
        }

        private void RbSortAZ_Checked(object sender, RoutedEventArgs e)
        {
            LBView.Items.SortDescriptions.Clear();

            LBView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription()
            {
                Direction = System.ComponentModel.ListSortDirection.Ascending,
                PropertyName = "Title"
            });
        }

        private void RbSortZA_Checked(object sender, RoutedEventArgs e)
        {
            LBView.Items.SortDescriptions.Clear();

            LBView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription()
            {
                Direction = System.ComponentModel.ListSortDirection.Descending,
                PropertyName = "Title"
            });
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(LBView.ItemsSource).Refresh();
        }

        private bool NameFilter(object c) => (c as Product).Title.ToLower().StartsWith(TbSearch.Text.Trim().ToLower());
    }
}
