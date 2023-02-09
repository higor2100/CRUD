using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace CRUD
{
    /// <summary>
    /// Interação lógica para Gerenciar.xam
    /// </summary>
    public partial class Gerenciar
    {
        private CadastroCSV cadastrar;
        private Pessoa pessoa;
        bool tl, em = false;

        public CadastroCSV cds
        {
            set { cadastrar = value; }
        }
        public Pessoa pss
        {
            set { pessoa = value; }
        }
        public Gerenciar()
        {
            InitializeComponent();
            dtTeste.Foreground = Brushes.Red;
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {

            if (btnAdd.Content.ToString() == "Adicionar")
            {
                if(cadastrar == null) cadastrar =new CadastroCSV();

                string nome = txtNome.Text,
                    sobrenome = txtSobrenome.Text,
                    telefone = txtTelefone.Text,
                    email = txtEmail.Text;
                DateTime dtNascimento = DateTime.Parse(dtTeste.Text);
                pessoa = new Pessoa(nome, sobrenome, dtNascimento, telefone, email);

                DialogHost.Close(null, cadastrar.adicionarPessoa(pessoa));
            }
            else
            {
                    
                pessoa.Telefone = txtTelefone.Text;
                pessoa.Email = txtEmail.Text;
                //Limpa a seleção, lista e atualiza o dado editado
                DialogHost.Close(null,cadastrar.editarPessoa(pessoa));
            }
        
        }


        private void BtnExcluir_OnClick(object sender, RoutedEventArgs e)
       {
           DialogHost.Close(null,cadastrar.removePessoa(pessoa));
           
        }

       private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
       {
           DialogHost.Close(null, new Pessoa("","",DateTime.Now, "",""));
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            //MessageBox.Show(txt.Foreground.ToString());
            var match = Regex.Match(txt.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.IgnoreCase);
            
            if (!match.Success)
            {
                HintAssist.SetHelperText(txt, "Digite um e-mail válido");
                txt.Foreground = Brushes.Brown;
                em = false;
                return;
            }
            HintAssist.SetHelperText(txt, "");
            txt.Foreground = Brushes.DimGray;
            em = true;
        }

        private void txtTelefone_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //
            TextBox txt = (TextBox)sender;
            //MessageBox.Show(String.Format("{0:(##) #####-####}", txt.Text));
            txt.Text = Regex.Replace(
                txt.Text,
                @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)*$",
                "($1$2) $3$4$5$6$7-$8$9$10$11");
            var match = Regex.Match(txt.Text, @"\(\d{2,}\) \d{5,}\-\d{4}", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                HintAssist.SetHelperText(txt, "Digite um telefone válido");
                txt.Foreground = Brushes.Brown;
                em = false;
                return;
            }
            HintAssist.SetHelperText(txt, "");
            txt.Foreground = Brushes.DimGray;
            tl = true;
        }

      

        private void txtTelefone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
