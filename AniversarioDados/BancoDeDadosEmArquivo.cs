using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AniversarioModel;

namespace AniversarioDados
{
    public class BancoDeDadosEmArquivo : BancoDeDados
    {
        public override void Salvar(Aniversariantes aniversariante)
        {
            bool aniversarianteJaExiste = false;
            if (aniversarianteJaExiste == false)
            {
                string nomeDoArquivo = obterNomeArquivo();

                string formato = $"{aniversariante.primeiroNome},{aniversariante.segundoNome},{aniversariante.dataAniversario.ToString()}\n";

                File.AppendAllText(nomeDoArquivo, formato);
            }

        }
        private static string obterNomeArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);

            string nomeDoArquivo = @"\dadosDosAniversariantes.txt";

            return localDaPastaDesktop + nomeDoArquivo;
        }

        public override IEnumerable<Aniversariantes> BuscarTodosOsAniversarios()
        {
            var nomeDoArquivo = obterNomeArquivo();

            string resultado = File.ReadAllText(nomeDoArquivo);
            string[] aniversariantes = resultado.Split('\n');

            List<Aniversariantes> AniversarioList = new List<Aniversariantes>();

            for (int i = 0; i < aniversariantes.Length - 1; i++)
            {
                string[] dadosDosAniversariantes = aniversariantes[i].Split(',');


                string primeiroNome = dadosDosAniversariantes[0];
                string segundoNome = dadosDosAniversariantes[1];
                DateTime dataAniversario = Convert.ToDateTime(dadosDosAniversariantes[2]);

                Aniversariantes aniversario = new Aniversariantes(primeiroNome, segundoNome, dataAniversario);
                AniversarioList.Add(aniversario);

            }

            return AniversarioList;

        }

        public override IEnumerable<Aniversariantes> BuscarTodosOsAniversarios(string nomeVarBusca)
        {
            return (from x in BuscarTodosOsAniversarios()
                    where x.nomeCompleto.Contains(nomeVarBusca)
                    orderby x.nomeCompleto
                    select x);

        }
        public override IEnumerable<Aniversariantes> BuscarTodosOsAniversariantesPeloAniversario(DateTime dataAniversarioBusca)
        {
            return (from x in BuscarTodosOsAniversarios()
                    where x.dataAniversario.Equals(dataAniversarioBusca)
                    orderby x.nomeCompleto
                    select x);

        }
        public override IEnumerable<Aniversariantes>Remover(string nomeVarBusca)
        {
            var antigos = File.ReadAllLines(obterNomeArquivo());
            var novas = antigos.Where(linha => !linha.Contains(nomeVarBusca));
            File.WriteAllLines(obterNomeArquivo(), novas);

            return (from x in BuscarTodosOsAniversarios()
                    where x.nomeCompleto.Contains(nomeVarBusca)
                    orderby x.nomeCompleto
                    select x);


        }


    }
}
