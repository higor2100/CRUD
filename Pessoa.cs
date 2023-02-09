using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class Pessoa
    {
        private string nome;
        private string sobrenome;           
        private DateTime dtNascimento;
        private string telefone;
        private string email;
        /// <summary>
        /// Retorna o nome completo da pessoa como string do objeto pessoa
        /// </summary>
        /// <returns>Retorna o nome completo da pessoa</returns>
        public override string ToString()
        {
            return nome + " " + sobrenome;
        }
        /// <summary>
        /// Retorna o nome da pessoa
        /// </summary>
        public string Nome
        {
            get { return nome; }
        }
        /// <summary>
        /// Retorna o sobrenome da pessoa
        /// </summary>
        public string Sobrenome
        {
            get { return sobrenome; }
        }

        /// <summary>
        /// Retorna e modifica o telefone da pessoa
        /// </summary>
        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        /// <summary>
        /// Retorna e modifica o email da pessoa
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// Retorna a data de nascimento da pessoa
        /// </summary>
        public DateTime DtNascimento
        {
            get { return dtNascimento; }
        }

        /// <summary>
        /// Construtor de pessoa
        /// </summary>
        /// <param name="nome">Nome da pessoa</param>
        /// <param name="sobrenome">Sobrenome da pessoa</param>
        /// <param name="dtNascimento">Data de nascimento da pessoa</param>
        /// <param name="telefone">Telefone da pessoa</param>
        /// <param name="email">E-mail da pessoa</param>
        public Pessoa(string nome, string sobrenome,
            DateTime dtNascimento, string telefone, string email)
        {
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.dtNascimento = dtNascimento;
            this.telefone = telefone;
            this.email = email;
        }
    }
}
