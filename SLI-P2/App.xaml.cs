using System.Configuration;
using System.Data;
using System.Windows;
using SLI_P2.Models;

namespace SLI_P2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<Veiculo> lstVeiculos = new List<Veiculo>();
        public static List<Processo> lstProcessos = new List<Processo>();
        public static List<Alfandega> lstAlfandegas = new List<Alfandega>();
        public static List<Contacto> lstContactos = new List<Contacto>();
    }

}
