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
using System.Data.SqlClient;

namespace ImmobiliWPF
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public MainWindow()
        {
        
            InitializeComponent();
            InitializeComboBox();
            
        }
        public MainWindow(Immobili imm)
        {
            InitializeComponent();
            InitializeComboBox();
            via.Text = imm.Via;
            superficie.Text = imm.Superfice;
            nvani.Text = imm.Vani.ToString();
            prezzo.Text = imm.Prezzo.ToString();
            if (imm.Prop != null)
            {
                prop.Text = imm.Prop.Nome + " " + imm.Prop.Cognome;
            }
            if (imm.Tipo_imm != null)
            {
                tipo.Text = imm.Tipo_imm.Descrizione;
            }
            in_vendita.SelectedItem = imm.In_vendita == true ? "Si" : "No";
            invia.Tag = imm.Id;
            invia.Click -= btn_Click;
            invia.Click += modifica_Click;
        }
        private void modifica_Click(object sender, RoutedEventArgs e)
        {
            if (checkFields())
            {
                string via_box = via.Text;
                string sup = superficie.Text;
                int vani = Convert.ToInt32(nvani.Text);
                int price = Convert.ToInt32(prezzo.Text);
                ComboBoxItem cbProp = (ComboBoxItem)prop.SelectedItem;
                ComboBoxItem cBImm = (ComboBoxItem)tipo.SelectedItem;
                bool vendita = in_vendita.Text == "Si" ? vendita = true : vendita = false;
                int id_tipo_imm = (String)cBImm.Content == "Nessun Tipo" ? -1 : Convert.ToInt32(cBImm.Tag);
                int id_proprietario = (String)cbProp.Content == "Nessun Proprietario" ? -1 : Convert.ToInt32(cbProp.Tag);
                SqlConnection conn = DbUtils.GetConnection();
                conn.Open();
                string sql = "UPDATE Immobili" +
                             "   SET id_tipo = @idt,via = @via, superfice=@sup, vani=@vani,prezzo=@prezzo,id_proprietario=@idp,in_vendita=@ven "+
                             " WHERE id = @id";
                Dictionary<string, object> dict = new Dictionary<string, object>();
                if (id_tipo_imm == -1)
                    dict.Add("idt", null);
                else
                    dict.Add("idt", id_tipo_imm);
                dict.Add("via", via_box);
                dict.Add("sup", sup);
                dict.Add("vani", vani);
                dict.Add("prezzo", price);
                if (id_proprietario == -1)
                    dict.Add("idp", null);
                else
                    dict.Add("idp", id_proprietario);
                dict.Add("ven", vendita);
                dict.Add("id", ((Button)sender).Tag);
                DbUtils.ExecCommand(conn, sql, dict);
                conn.Close();
                if (MessageBox.Show("Operazione riuscita", "Immobile modificato con successo!!") == MessageBoxResult.OK)
                {

                    ImmobiliWin newWin = new ImmobiliWin();
                    Application.Current.MainWindow.Content = newWin;

                }
            }
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkFields())
            {
                string via_box = via.Text;
                string sup = superficie.Text;
                int vani = Convert.ToInt32(nvani.Text);
                int price = Convert.ToInt32(prezzo.Text);
                ComboBoxItem cbProp = (ComboBoxItem)prop.SelectedItem;
                ComboBoxItem cBImm = (ComboBoxItem)tipo.SelectedItem;
                bool vendita = in_vendita.Text == "Si" ? vendita = true : vendita = false;
                int id_tipo_imm = (String)cBImm.Content == "Nessun Tipo" ? -1 : Convert.ToInt32(cBImm.Tag);
                int id_proprietario = (String)cbProp.Content == "Nessun Proprietario" ? -1 : Convert.ToInt32(cbProp.Tag);
                SqlConnection conn = DbUtils.GetConnection();
                conn.Open();
                string sql = "INSERT INTO Immobili (id_tipo,via,superfice,vani,prezzo,id_proprietario,in_vendita) VALUES(@idt,@via,@sup,@vani,@prezzo,@idp,@ven)";
                Dictionary<string, object> dict = new Dictionary<string, object>();
                if (id_tipo_imm == -1)
                    dict.Add("idt", null);
                else
                    dict.Add("idt", id_tipo_imm);
                dict.Add("via", via_box);
                dict.Add("sup", sup);
                dict.Add("vani", vani);
                dict.Add("prezzo", price);
                if (id_proprietario == -1)
                    dict.Add("idp", null);
                else
                    dict.Add("idp", id_proprietario);
                dict.Add("ven", vendita);
                DbUtils.ExecCommand(conn, sql, dict);
                conn.Close();
                if (MessageBox.Show("Operazione riuscita", "Immobile inserito con successo!!") == MessageBoxResult.OK)
                {

                    ImmobiliWin newWin = new ImmobiliWin();
                    Application.Current.MainWindow.Content = newWin;

                }
            }
            
        }

        private void InitializeComboBox()
        {
            SqlConnection conn = DbUtils.GetConnection();
            conn.Open();
            SqlDataReader rd = DbUtils.GetReader(conn, "SELECT * FROM Proprietari", null);
            while (rd.Read())
            {
                ComboBoxItem value = new ComboBoxItem();
                value.Content = rd["nome"].ToString() + " " + rd["cognome"].ToString();
                value.Tag = rd["id"];
                prop.Items.Add(value);
            }
            rd.Close();
            rd = DbUtils.GetReader(conn, "SELECT * FROM TipiImmobile", null);
            while (rd.Read())
            {
                ComboBoxItem value = new ComboBoxItem();
                value.Content = rd["descrizione"].ToString();
                value.Tag = rd["id"];
                tipo.Items.Add(value);
            }
            rd.Close();
            conn.Close();
        }
        private bool checkFields()
        {
            resetBorderBrushes();
            bool check = true;
            if(via.Text == "")
            {
                via.BorderBrush = Brushes.Red;
                check = false;
            }
            if(superficie.Text == "")
            {
                superficie.BorderBrush = Brushes.Red;
                check = false;
            }
            if(nvani.Text == "")
            {
                nvani.BorderBrush = Brushes.Red;
                check = false;
            }
            if(prezzo.Text == "")
            {
                prezzo.BorderBrush = Brushes.Red;
                check = false;
            }
            if (!check)
                errore.Text = "Campi Obbligatori";
            return check;
            

        }
        private void resetBorderBrushes()
        {
            via.BorderBrush = Brushes.Gray;
            superficie.BorderBrush = Brushes.Gray;
            nvani.BorderBrush = Brushes.Gray;
            prezzo.BorderBrush = Brushes.Gray;
            errore.Text = "";

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

        private void Visualizza_Click(object sender, RoutedEventArgs e)
        {
            ImmobiliWin newWin = new ImmobiliWin();
            Application.Current.MainWindow.Content = newWin;
        }
    }
}
