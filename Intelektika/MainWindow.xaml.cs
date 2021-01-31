using Microsoft.Win32;
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
using Intelektika.Backend;
namespace Intelektika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //AddColumns();
        }
        private void AddColumns()
        {
            // Add columns to listview
            var gridView = new GridView();
            this.records_lv.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Name",
                DisplayMemberBinding = new Binding("name")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "X",
                DisplayMemberBinding = new Binding("value1")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Y",
                DisplayMemberBinding = new Binding("value2")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Class",
                DisplayMemberBinding = new Binding("recordClass")
            });
        }

        private void btn_fileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Reader.file = openFileDialog.FileName;
                Reader.Read(false);
                DisplayFile();
            }
        }
        private void DisplayFile()
        {
            try
            {
                records_lv.Items.Clear();
                foreach (Record r in Reader.records)
                {
                    this.records_lv.Items.Add(r);
                }
            }
            catch { }
            try
            {
                clasify_lv.Items.Clear();
                foreach (Record r in Reader.toClasify)
                {
                    this.clasify_lv.Items.Add(r);
                }
            }
            catch { }
        }

        private void btn_selectToClasify_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Reader.file = openFileDialog.FileName;
                Reader.Read(true);
                DisplayFile();
            }
        }

        private void btn_run_Click(object sender, RoutedEventArgs e)
        {
            bool keepGoing = true;
            try
            {
                Reader.kmeans = Convert.ToInt32(txt_kmeans.Text);
            }
            catch
            {
                keepGoing = false;
                MessageBox.Show("'K-NN' must be an integer.");
            }
            if(keepGoing)
            if (Reader.records.Count > 0)
            {
                if(Reader.toClasify.Count>0)
                {
                    if(Reader.kmeans>0)
                    {
                        Logic.Calculate();
                        DisplayOutput();
                    }
                    else
                        MessageBox.Show("'K-NN' cannot be <=0.");
                }
                else
                    MessageBox.Show("'Records' list is empty.");
            }
            else
                MessageBox.Show("'To clasify' list is empty.");
        }
        private void DisplayOutput()
        {
            output_lv.Items.Clear();
            foreach(string line in Reader.output)
            {
                output_lv.Items.Add(line);
            }
        }
    }
}
