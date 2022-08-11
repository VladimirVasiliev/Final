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


using System.Data;
using System.Data.SQLite;


namespace WpfSqlite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string SqLiteTableName { get; set; }

        public MainWindow()
        {
            
            InitializeComponent();

            //Binding binding = new Binding();

            //binding.ElementName = "Label_Text"; //элемент - источник
            //binding.Path = new PropertyPath("Content"); //свойство элемента источника

            //Button1_id.SetBinding(Label.ContentProperty, binding); //установить привязки для элемента - приемника


            //===================для кнопок================================
            string Btn1Name = "Кнопка 1";
            Binding bindingBtn1 = new Binding();
            bindingBtn1.Source = Btn1Name;
            Button1_id.SetBinding(Button.ContentProperty, bindingBtn1);

            string Btn2Name = "Кнопка 2";
            Binding bindingBtn2 = new Binding();
            bindingBtn2.Source = Btn2Name;
            Button2_id.SetBinding(Button.ContentProperty, bindingBtn2);

            string Btn3Name = "Кнопка 3";
            Binding bindingBtn3 = new Binding();
            bindingBtn3.Source = Btn3Name;
            Button3_id.SetBinding(Button.ContentProperty, bindingBtn3);
            //==================!для кнопок==================================

            //===================для lable таблица===========================
            //Binding binding = new Binding();
            //binding.Source = SqLiteTableName;
            //Label_Text.SetBinding(Label.ContentProperty, binding);
            //==================!для lable таблица===========================


            //Binding binding1 = new Binding();
            //binding1.Source = str;
            //Label_Text.SetBinding(Label.ContentProperty, binding1);

            //ClassDataGrid classDataGrid = new ClassDataGrid();


            //Binding binding = new Binding();
            //binding.Source = classDataGrid;
            //SqlDataGrid.SetBinding(DataGrid., binding);


            refreshdata("Select name from sqlite_sequence", "ListBox");

            
        }

        public void SelectedItemDataGrid()
        {
            MessageBox.Show("asdasd");
        }

        public void refreshdata(string qweru, string Temp)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source= 
            D:\soft\DB.Browser.for.SQLite-3.12.2-win32\DB\test.db;");
            con.Open();

            //SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1



            //SQLiteCommand cmd = new SQLiteCommand("select * from ТаблицаСотрудники", con);
           
                       

            if(Temp == "ListBox")
            {
                ListBoxTableName.Items.Clear();
                SQLiteCommand cmd = new SQLiteCommand(qweru, con);
                System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListBoxTableName.Items.Add(reader[0]);
                }
                    
            }

            if (Temp == "DataGrid")
            {
                SQLiteCommand cmd = new SQLiteCommand(qweru, con);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SqlDataGrid.ItemsSource = ds.Tables[0].DefaultView;
                    //SqlDataGrid.Columns[0].Visibility = Visibility.Hidden;
                    SqlDataGrid.IsReadOnly = true;
                }
            }
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender == Button1_id)
            {
                MessageBox.Show("кнлпка 1");
            } 
            if(sender == Button2_id)
            {
                MessageBox.Show("кнопка 2");
            }
            if(sender == Button3_id)
            {
                MessageBox.Show("кнопка 3");
            }

        }

        private void SqlDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //int i = SqlDataGrid.SelectedIndex;
            //MessageBox.Show(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[dataGridView1.CurrentCell.ColumnIndex].Value.ToString())

            //индекс
            //MessageBox.Show(SqlDataGrid.SelectedIndex.ToString());

            //SqlDataGrid.Columns[0].Visibility = Visibility.;
            var list = new List<string>();
            for (int i = 0; i < SqlDataGrid.Columns.Count; i++)
            {
                var ci = new DataGridCellInfo(SqlDataGrid.Items[SqlDataGrid.SelectedIndex], SqlDataGrid.Columns[i]);
                var content = ci.Column.GetCellContent(ci.Item) as TextBlock;
                list.Add(SqlDataGrid.Columns[i].Header.ToString() + " = " + content.Text + "; ");
            }

            string str="";
            foreach (var item in list)
            {
                str = str + item ;
                
            }

            


            MessageLog(str);
           
            

            //MessageBox.Show(SqlDataGrid.SelectedCells[0].ToString());

            //string[,] data = new string[SqlDataGrid.Items.Count, SqlDataGrid.Columns.Count];
            //for (int i = 0; i < SqlDataGrid.Items.Count; i++)
            //{
            //    for (int j = 0; j < SqlDataGrid.Columns.Count; j++)
            //    {
            //        //data[i,j]=SqlDataGrid.
            //    }
            //}


            //MessageBox.Show(SqlDataGrid.Items.Count.ToString());

           // MessageBox.Show(SqlDataGrid.Columns.Count.ToString());
        }

        public void MessageLog(string logo)
        {
            TextBlockLogo.Text = logo;

            //string Btn1Name = "Кнопка 1";
            //Binding bindingBtn1 = new Binding();
            //bindingBtn1.Source = Btn1Name;
            //Button1_id.SetBinding(Button.ContentProperty, bindingBtn1);

        }

        private void ListBoxTableName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label_Text.Content = "Таблица : " + ListBoxTableName.SelectedItem.ToString();
            string qwery = "select * from " + ListBoxTableName.SelectedItem.ToString();
            refreshdata(qwery, "DataGrid");
        }

        private void SqlDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
