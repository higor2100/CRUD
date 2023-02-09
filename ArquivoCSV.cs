using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Win32;

namespace CRUD
{
    public class ArquivoCSV
    {
        protected OpenFileDialog novoArquivo = new();
        protected SaveFileDialog salvarNovoArquivo = new();
        protected string path;

        public ArquivoCSV(){}

        public  ArquivoCSV(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Retorna dados lidos
        /// </summary>
        /// <returns>Retorna o que o arquivo contem, caso não tenha nada nulo</returns>
        protected virtual string lendoArquivo()
        {
            
            novoArquivo.Filter = "Arquivo CSV (*.csv)|*.csv";
            novoArquivo.Title = "Selecione o arquivo CSV";
            if (!(novoArquivo.ShowDialog() is not null))
                return null;
            if (novoArquivo.FileName == null)
                return null;
            string caminho = novoArquivo.FileName;
            try
            {
                return File.ReadAllText(novoArquivo.FileName);
            }
            catch (Exception E)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Salva o arquivo
        /// </summary>
        public virtual void salvarArquivo(){}
        protected virtual void salvarArquivo(string txt)
        {
            salvarNovoArquivo.Filter = "Arquivo CSV|*.csv";
            salvarNovoArquivo.Title = "Salvar Arquivo";

            if (!salvarNovoArquivo.ShowDialog() is not null)
                return;

            FileStream abrirArquivo = (FileStream)salvarNovoArquivo.OpenFile();
            StreamWriter salvandoArquivo = new StreamWriter(abrirArquivo);

            salvandoArquivo.WriteLine(txt);
            salvandoArquivo.Close();
            abrirArquivo.Close();
        }
    }
}
