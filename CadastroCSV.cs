using System;
using System.Collections.Generic;
using System.IO;

namespace CRUD
{
    public class CadastroCSV : ArquivoCSV
    {
        
		/*
         * Atributos que será utilizado durante a execução
         */
		private List<Pessoa> pessoas;

        public Pessoa[] listaPessoas
        {
            set { pessoas = new List<Pessoa>(value);}
        }
        /// <summary>
        /// Inicia o cadastro de pessoas
        /// </summary>
        public CadastroCSV()
        {
            pessoas = new List<Pessoa>();
        }

		public CadastroCSV(string path) : base(path)
		{
			pessoas = new List<Pessoa>();
		}
		/// <summary>
		/// Adiciona uma pessoa ao cadastro
		/// </summary>
		/// <param name="pessoa">Nova pessoa para o cadastro</param>
		/// <returns></returns>
		public Pessoa[] adicionarPessoa(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
            return pessoas.ToArray();
        }
        /// <summary>
        /// Faz a leitura da base de dados
        /// </summary>
        /// <returns>Retorna a base de dados</returns>
        public Pessoa[] leituraArquivo()
        {

            string textoLido = base.lendoArquivo();

			if (textoLido == null) throw new Exception("Erro na leitura do arquivo");

            int i = 0;
            foreach (var linha in textoLido.Split('\n'))
            {
                if (linha == "" || linha == "\r") break;
                if (i != 0)
                {
                    string[] tratamento = linha.Split(';');
                    Pessoa ps = new Pessoa(tratamento[0], tratamento[1], DateTime.Parse(tratamento[2]), tratamento[3],
                        tratamento[4]);
                    pessoas.Add(ps);
                }

                i++;
            }

            return pessoas.ToArray();
        }

        protected override string lendoArquivo()
        {
			
			try
			{
				return File.ReadAllText(path);
			}
			catch (Exception E)
			{
				return null;
			}
		}

        
		public Pessoa[] leituraBase()
        {

            string textoLido = this.lendoArquivo();
            if (textoLido == null) throw new Exception("Erro na leitura do arquivo");

            int i = 0;
            foreach (var linha in textoLido.Split('\n'))
            {
                if (linha == "" || linha == "\r") break;
                if (i != 0)
                {
                    string[] tratamento = linha.Split(';');
                    Pessoa ps = new Pessoa(tratamento[0], tratamento[1], DateTime.Parse(tratamento[2]), tratamento[3],
                        tratamento[4]);
                    pessoas.Add(ps);
                }

                i++;
            }

            return pessoas.ToArray();
        }

		/// <summary>
		/// Edita uma pessoa do cadastrp
		/// </summary>
		/// <param name="pessoa">Nova pessoa</param>
		/// <returns>Retorna a nova base de dados</returns>
		public Pessoa[] editarPessoa(Pessoa pessoa)
        {
            for (int i = 0; i < pessoas.Count; i++)
            {
                if (pessoas[i] == pessoa)
                {
                    pessoas[i] = pessoa;
                    break;
                }
            }

            return pessoas.ToArray();
        }
        
        /// <summary>
        /// Limpa o cadastro
        /// </summary>
        public void limparCadastro()
        {
            pessoas.Clear();
        }

        public override void salvarArquivo()
		{
			base.salvarArquivo(textoProcessado());
		}
        public void salvarBase()
        {
			StreamWriter salvandoArquivo = new StreamWriter(path);


			salvandoArquivo.WriteLine(textoProcessado());
            salvandoArquivo.Close();
            
		}

		/// <summary>
		/// Processa o texto para ser salvo
		/// </summary>
		/// <returns>Texto processado</returns>
		private string textoProcessado()
        {
            string txt = "Nome;Sobrenome;Data de Nascimento; Telefone;Email\n";
            foreach (Pessoa pessoa in pessoas)
            {
                txt += pessoa.Nome + ";";
                txt += pessoa.Sobrenome + ";";
                txt += pessoa.DtNascimento + ";";
                txt += pessoa.Telefone + ";";
                txt += pessoa.Email + "\n";
            }
            return txt;
        }
        /// <summary>
        /// Remove uma pessoa do cadastro
        /// </summary>
        /// <param name="pessoa">Pessoa a ser removida</param>
        /// <returns>Base de dados atualizada</returns>

        public Pessoa[] removePessoa(Pessoa pessoa)
        {
            pessoas.Remove(pessoa);
            return pessoas.ToArray();
        }

    }
}
