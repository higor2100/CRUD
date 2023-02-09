using MaterialDesignThemes.Wpf;
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

namespace CRUD
{
    /// <summary>
    /// Interação lógica para Gerenciar.xam
    /// </summary>
    public partial class Gerenciar : UserControl
    {
        private CadastroCSV cadastrar;
        private Pessoa pessoa;

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
    }
}
