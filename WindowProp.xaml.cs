using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace ImmobiliWPF
{
    /// <summary>
    /// Logica di interazione per WindowProp.xaml
    /// </summary>
    public partial class WindowProp : UserControl
    {
        public WindowProp()
        {
            InitializeComponent();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWin = new MainWindow();
            Application.Current.MainWindow.Content = newWin;
        }

        private void Immobile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Proprietario_Click(object sender, RoutedEventArgs e)
        {
            WindowProp newWin = new WindowProp();
            Application.Current.MainWindow.Content = newWin;
        }

        private void invia_Click(object sender, RoutedEventArgs e)
        {
            if (checkFields())
            {
                string cf_box = cf.Text;
                string nome_box = nome.Text;
                string cognome_box = cognome.Text;
                int telefono_box = Convert.ToInt32(telefono.Text);
                SqlConnection conn = DbUtils.GetConnection();
                conn.Open();
                string sql = "INSERT INTO Proprietari (CF,nome,cognome,telefono) VALUES(@CF,@nome,@cognome,@telefono)";
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("CF", cf_box);
                dict.Add("nome", nome_box);
                dict.Add("cognome", cognome_box);
                dict.Add("telefono", telefono_box);
                DbUtils.ExecCommand(conn, sql, dict);
                conn.Close();
                if (MessageBox.Show("Operazione riuscita", "Proprietario inserito con successo!!") == MessageBoxResult.OK)
                {
                    Application.Current.MainWindow.Content = new ImmobiliWin();

                }
            }
        }
        private bool checkFields()
        {
            resetBorderBrushes();
            bool check = true;
            if (cf.Text == "")
            {
                cf.BorderBrush = Brushes.Red;
                check = false;
            }
            if (nome.Text == "")
            {
                nome.BorderBrush = Brushes.Red;
                check = false;
            }
            if (cognome.Text == "")
            {
                cognome.BorderBrush = Brushes.Red;
                check = false;
            }
            if (telefono.Text == "")
            {
                telefono.BorderBrush = Brushes.Red;
                check = false;
            }
            if (!check)
                errore.Text = "Campi Obbligatori";
            return check;
        }
        private void resetBorderBrushes()
        {
            cf.BorderBrush = Brushes.Gray;
            nome.BorderBrush = Brushes.Gray;
            cognome.BorderBrush = Brushes.Gray;
            telefono.BorderBrush = Brushes.Gray;
            errore.Text = "";

        }
    }
}
