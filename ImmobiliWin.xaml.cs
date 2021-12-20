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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImmobiliWPF
{
    /// <summary>
    /// Logica di interazione per ImmobiliWin.xaml
    /// </summary>
    public partial class ImmobiliWin : UserControl
    {
        public ImmobiliWin()
        {
            InitializeComponent();
            SqlConnection conn = DbUtils.GetConnection();
            conn.Open();
            string sql = "SELECT i.id as id_imm,i.via, i.superfice, i.vani, i.prezzo, i.in_vendita,p.id as id_prop, p.CF, p.nome,p.cognome,p.telefono,tp.id as id_tp,tp.descrizione "+
                           "FROM Immobili i "+
                                "LEFT JOIN "+
                                "Proprietari p "+
                                "on "+
                                "i.id_proprietario = p.id "+ 
                                "LEFT JOIN TipiImmobile tp "+
                                "on "+
                                "i.id_tipo = tp.id";
            SqlDataReader rd =DbUtils.GetReader(conn, sql, null);
            List<Immobili> immo = new List<Immobili>();
            while (rd.Read())
            {
                TipoImmobile tp =null;
                Proprietario p=null;

                if(rd["id_tp"] != DBNull.Value && rd["descrizione"] != DBNull.Value)
                {
                    tp= new TipoImmobile(Convert.ToInt32(rd["id_tp"]), Convert.ToString(rd["descrizione"]));
                }
                if (rd["id_prop"] != DBNull.Value && rd["CF"] != DBNull.Value && rd["nome"] != DBNull.Value && rd["cognome"] != DBNull.Value && rd["telefono"] != DBNull.Value)
                {
                    p = new Proprietario(Convert.ToInt32(rd["id_prop"]), Convert.ToString(rd["CF"]), Convert.ToString(rd["nome"]), Convert.ToString(rd["cognome"]), Convert.ToString(rd["telefono"]));
                }
                immo.Add(new Immobili(Convert.ToInt32(rd["id_imm"]), tp, Convert.ToString(rd["via"]), Convert.ToString(rd["superfice"]), Convert.ToInt32(rd["vani"]), Convert.ToInt32(rd["prezzo"]), p, Convert.ToBoolean(rd["in_vendita"])));
            }

            listaImmobili.ItemsSource = immo;
            conn.Close();
        }

        private void Modifica_Click(object sender, RoutedEventArgs e)
        {
            /*var immobils = (Immobili)listaImmobili.SelectedItem;
            Console.WriteLine(immobils.Id);*/
            Immobili immobile = (Immobili)((Button)sender).Tag;
            if (immobile != null)
            {
                MainWindow newWin = new MainWindow(immobile);
                Application.Current.MainWindow.Content = newWin;
            }
        }

        private void Aggiungi_Imm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWin = new MainWindow();
            Application.Current.MainWindow.Content = newWin;
        }
        private void Aggiungi_Prop_Click(object sender, RoutedEventArgs e)
        {
            WindowProp newWin = new WindowProp();
            Application.Current.MainWindow.Content = newWin;
        }
    }
}
