using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
using MaterialDesignThemes.Wpf;

namespace CRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Pessoa pessoa;
        private CadastroCSV cadastrar;
        public MainWindow()
        {
            InitializeComponent();
            /*
            cadastrar = new CadastroCSV("C:\\Users\\higor\\Desktop\\LeituraDeArquivo\\tabela_de_higor.csv");
            lboItems.ItemsSource = cadastrar.leituraBase();
            btnAdd.Content = "Salvar Dados";
            */
        }

        
        private void BtnLimpar_OnClick(object sender, RoutedEventArgs e)
        {
            lboItems.ItemsSource = null;
            btnAdd.Content = "Adicionar";
            if (cadastrar == null) return;
            cadastrar.limparCadastro();
           
        }

        private void lboItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (lboItems.SelectedItem == null)
                return;
            PackIcon Teste = btnAddItem.Content as PackIcon;
            Teste.Kind = PackIconKind.Edit;
        }
        
        

        private async void BtnAddItem_OnClick(object sender, RoutedEventArgs e)
        {
            PackIcon icone = btnAddItem.Content as PackIcon;

            if (icone.Kind == PackIconKind.Add)
            {
                Gerenciar gerencia = new Gerenciar();
                gerencia.cds = cadastrar;
                Pessoa[] pessoas = await DialogHost.Show(gerencia) as Pessoa[];
                if (pessoas == null) return;
                lboItems.ItemsSource = null;
                cadastrar.listaPessoas = pessoas;
                lboItems.ItemsSource = pessoas;
            }
            else
            {
                Gerenciar gerencia = new Gerenciar();
                gerencia.cds = cadastrar;
                gerencia.pss = lboItems.SelectedItem as Pessoa;
                Pessoa pessoa = lboItems.SelectedItem as Pessoa;
                gerencia.btnExcluir.Visibility = Visibility.Visible;
                gerencia.btnAdd.Content = "Editar";
                gerencia.txtNome.IsEnabled = false;
                gerencia.txtSobrenome.IsEnabled = false;
                gerencia.dtTeste.IsEnabled = false;
                gerencia.txtNome.Text = pessoa.Nome;
                gerencia.txtSobrenome.Text = pessoa.Sobrenome;
                gerencia.dtTeste.Text = pessoa.DtNascimento.ToString();
                gerencia.txtTelefone.Text = pessoa.Telefone;
                gerencia.txtEmail.Text = pessoa.Email.Replace("\r","");
                Pessoa[] pessoas = await DialogHost.Show(gerencia) as Pessoa[];
                if (pessoas == null)
                {
                    icone.Kind = PackIconKind.Add;
                    lboItems.SelectedItem = null;
                    return;
                }
                lboItems.ItemsSource = null;
                cadastrar.listaPessoas = pessoas;
                lboItems.ItemsSource = pessoas;
                icone.Kind = PackIconKind.Add;

            }
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (btnAdd.Content.ToString() == "Adicionar")
            {
              
                //Devolve a lista de elementos
                try
                {
                    if (cadastrar == null) cadastrar = new CadastroCSV();
                    Pessoa[] pessoas = cadastrar.leituraArquivo();
                    lboItems.ItemsSource = pessoas;
                    btnAdd.Content = "Salvar Arquivo";
                }
                catch (Exception E)
                {
                    Erros erros = new Erros();

                    erros.lblErro.Text = E.Message;
                    DialogHost.Show(erros);
                }
                
            }

            else 
            {
                //Salva o arquivo e limpa os dados
                try
                {
                    cadastrar.salvarArquivo();
                    btnAdd.Content = "Adicionar";
                }
                catch (Exception E)
                {
                    Erros erros = new Erros();
                    erros.lblErro.Text = E.Message;
                    DialogHost.Show(new Erros().lblErro.Text = E.Message);
                }
            }
            
        }
    }
}
