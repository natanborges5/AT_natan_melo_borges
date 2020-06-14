using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AniversarioModel;
using AniversarioDados;
using System.IO;

namespace ProgramTp3
{
    class Apresentação
    {
        public static void MenuPrincipal()
        {
            AniversariantesDoDia();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------- Gerenciador de aniversarios -----------------");
            Console.WriteLine("Escolha a operação");
            Console.WriteLine("1 - Cadastrar Aniversario");
            Console.WriteLine("2 - Consultar Aniversario");
            Console.WriteLine("3 - Sair");
            char operacao = Console.ReadLine().ToCharArray()[0];

            if (operacao == '1')
            {
                cadastrarAniversario();
            }
            if (operacao == '2')
            {
                consultarAniversario();
            }
            if (operacao >= '4')
            {
                Console.WriteLine("Nenhuma opção disponivel selecionada");
                Console.Clear();
                MenuPrincipal();
            }



        }

        private static void AniversariantesDoDia()
        {
            bool nenhumAniversarianteHoje = true;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("----------------- Aniversariantes do Dia -----------------");
            foreach (var aniversariante in BancoDeDados.BuscarTodosOsAniversarios())
            {
                DateTime datadehoje = DateTime.Now;

                int difData = (datadehoje.Date - aniversariante.dataAniversario.Date).Days;
                var diasParaAnos = (difData / 365 + 1);

                DateTime datefinal = aniversariante.dataAniversario.AddYears(diasParaAnos);

                int resultadoAniversario = (datefinal.Date - datadehoje.Date).Days;
                if (resultadoAniversario == 365)
                {
                    Console.WriteLine($"Nome: {aniversariante.nomeCompleto} aniversario em: {aniversariante.dataAniversario}");
                    nenhumAniversarianteHoje = false;
                }

            }
            if (nenhumAniversarianteHoje == true)
            {
                Console.WriteLine("Nenhum aniversariante hoje");
            }
        }

        public static void cadastrarAniversario()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Entre com o nome:");
                string primeironome = Console.ReadLine();

                Console.WriteLine("Entre com o sobrenome:");
                string segundonome = Console.ReadLine();
                Console.WriteLine("Digite a data de aniversario no formato dd/mm/yy");
                var dataaniversario = DateTime.Parse(Console.ReadLine());


                var aniversariante = new Aniversariantes(primeironome, segundonome, dataaniversario);
                BancoDeDados.Salvar(aniversariante);
            }


            catch (System.FormatException)
            {
                Console.WriteLine("Dados inseridos incorretamente");
                Console.WriteLine("Pressione qualquer tecla para continuar");
                Console.ReadKey();
                MenuPrincipal();
            }
            Console.WriteLine("Aniversariante cadastrado com sucesso!!");
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            MenuPrincipal();
        }
        public static void consultarAniversario()
        {
            Console.Clear();
            FiltrosAniversario();
        }
        public static void FiltrosAniversario()
        {
            Console.WriteLine("----------------- Consulta de Aniversarios -----------------");
            Console.WriteLine("Escolha uma opção de filtro");
            Console.WriteLine("1 - Consultar por nome");
            Console.WriteLine("2 - Consultar por data de anivarsario");
            Console.WriteLine("3 - Consultar todos os aniversariantes");
            Console.WriteLine("4 - Editar aniversariantes");
            Console.WriteLine("5 - Deletar aniversariante");
            Console.WriteLine("6 - Voltar para o menu");
            string tipoConsulta = Console.ReadLine();

            switch (tipoConsulta)
            {
                case "1":
                    consultarPorNome();
                    break;
                case "2":
                    consultarPorData();
                    break;
                case "3":
                    consultarTodos();
                    break;
                case "4":
                    EditarAniversariante();
                    break;
                case "5":
                    DeletarAniversariante();
                    break;
                case "6":
                    MenuPrincipal();
                    break;
                default:
                    Console.WriteLine("Consulta incorreta");
                    FiltrosAniversario();
                    break;
            }
            MenuPrincipal();
        }
        public static void consultarPorNome()
        {
            Console.WriteLine("----------------- Consulta de Aniversarios -----------------");
            Console.WriteLine("Digite o nome do aniversariante que deseja buscar: ");
            string nomeVarBusca = Console.ReadLine();

            var aniversarianteEncontrados = BancoDeDados.BuscarTodosOsAniversarios(nomeVarBusca);

            int quantidadeDeaniversarianteEncontrados = aniversarianteEncontrados.Count();
            if (quantidadeDeaniversarianteEncontrados > 0)
            {
                Console.Clear();
                Console.WriteLine("----------------- Aniversariantes encontrados!! -----------------");
                foreach (var aniversariante in aniversarianteEncontrados)
                {
                    Console.WriteLine($"Nome: {aniversariante.nomeCompleto} aniversario em: {aniversariante.dataAniversario}");
                    Console.WriteLine("Deseja calcular quanto falta para o proximo aniversario desse aniversariante?");
                    Console.WriteLine("(se deseja selecionar outro aniversariante selecione a opção não)");
                    Console.WriteLine("1 - Sim");
                    Console.WriteLine("2 - Não");
                    Console.WriteLine("3 - Voltar ao menu principal");
                    Console.WriteLine("-------------------------------------------------------------------------------------");
                    var dataAniversarioBusca = aniversariante.dataAniversario;
                    char operacao = Console.ReadLine().ToCharArray()[0];
                    if (operacao == '1')
                    {
                        Console.Clear();
                        DateTime datadehoje = DateTime.Now;

                        int difData = (datadehoje.Date - dataAniversarioBusca.Date).Days;
                        var diasParaAnos = (difData / 365 + 1);

                        DateTime datefinal = dataAniversarioBusca.AddYears(diasParaAnos);

                        int resultadoAniversario = (datefinal.Date - datadehoje.Date).Days;
                        if (resultadoAniversario == 365)
                        {
                            Console.WriteLine("Parabens!! Hoje é o seu aniversario");
                        }
                        Console.WriteLine($"Faltam {resultadoAniversario} dias para o seu proximo aniversario");
                        Console.WriteLine($"Nome: {aniversariante.nomeCompleto}");
                        Console.WriteLine($"Data de nascimento: {aniversariante.dataAniversario}");
                        Console.WriteLine("///////////////////////////////////////////////////////////////////////////");

                    }
                    if (operacao >= '3')
                    {
                        MenuPrincipal();
                    }


                }
                MenuPrincipal();
            }
            else
            {
                Console.WriteLine("Nenhum aniversariante encontrado");
            }
        }

        public static void consultarPorData()
        {

            Console.WriteLine("Entre com a data de aniversario");
            var dataAniversarioBusca = DateTime.Parse(Console.ReadLine());
            var funcionariosFiltradosData = BancoDeDados.BuscarTodosOsAniversariantesPeloAniversario(dataAniversarioBusca);
            int quantidadeDeaniversarianteEncontrados = funcionariosFiltradosData.Count();
            if (quantidadeDeaniversarianteEncontrados == 0)
            {
                Console.WriteLine("Nenhum aniversariante encontrado");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                MenuPrincipal();
            }
            Console.Clear();

            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);

            string nomeDoArquivo = @"\dadosDosAniversariantes.txt";

            string local = localDaPastaDesktop + nomeDoArquivo;


            DateTime datadehoje = DateTime.Now;

            int difData = (datadehoje.Date - dataAniversarioBusca.Date).Days;
            var diasParaAnos = (difData / 365 + 1);

            DateTime datefinal = dataAniversarioBusca.AddYears(diasParaAnos);

            int resultadoAniversario = (datefinal.Date - datadehoje.Date).Days;


            foreach (var aniversariante in funcionariosFiltradosData)
            {

                if (resultadoAniversario == 365)
                {
                    Console.WriteLine("Parabens!! Hoje é o seu aniversario");
                }
                Console.WriteLine($"Faltam {resultadoAniversario} dias para o seu proximo aniversario");
                Console.WriteLine($"Nome: {aniversariante.nomeCompleto}");
                Console.WriteLine($"Data de nascimento: {aniversariante.dataAniversario}");
            }
        }
        public static void consultarTodos()
        {

            foreach (var aniversariante in BancoDeDados.BuscarTodosOsAniversarios())
            {
                Console.WriteLine($"Nome: {aniversariante.nomeCompleto} aniversario em: {aniversariante.dataAniversario}");
            }
        }
        public static void EditarAniversariante()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;
            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\dadosDosAniversariantes.txt";
            string local = localDaPastaDesktop + nomeDoArquivo;

            Console.WriteLine("----------------- Editor de Aniversarios -----------------");
            Console.WriteLine("Digite o nome do aniversariante que deseja editar: ");
            string nomeVarBusca = Console.ReadLine();

            var aniversarianteEncontrados = BancoDeDados.BuscarTodosOsAniversarios(nomeVarBusca);

            int quantidadeDeaniversarianteEncontrados = aniversarianteEncontrados.Count();
            if (quantidadeDeaniversarianteEncontrados == 0)
            {
                Console.WriteLine("Nenhum aniversariante encontrado");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                MenuPrincipal();
            }

            if (quantidadeDeaniversarianteEncontrados > 0)
            {
                Console.Clear();
                Console.WriteLine("----------------- Aniversariantes encontrados!! -----------------");
                foreach (var aniversariante in aniversarianteEncontrados)
                {
                    Console.WriteLine($"Nome: {aniversariante.nomeCompleto} aniversario em: {aniversariante.dataAniversario}");
                    Console.WriteLine("1 - Mudar nome e sobrenome");
                    Console.WriteLine("2 - Mudar data de aniversario");
                    Console.WriteLine("3 - Proximo aniversariante");
                    Console.WriteLine("4 - Voltar para o menu de navegação");
                    char operacao = Console.ReadLine().ToCharArray()[0];
                    if (operacao == '1')
                    {
                        Console.WriteLine("O nome que deseja botar");
                        string primeiroNomeEditado = Console.ReadLine();

                        Console.WriteLine("O Sobrenome que deseja botar");
                        string segundoNomeEditado = Console.ReadLine();


                        string text = File.ReadAllText(local);
                        text = text.Replace(aniversariante.primeiroNome, primeiroNomeEditado);
                        text = text.Replace(aniversariante.segundoNome, segundoNomeEditado);

                        File.WriteAllText(local, text);
                        Console.WriteLine("Editado com sucesso");
                        Console.WriteLine("Aperte qualquer tecla para continuar");
                        Console.ReadKey();
                        MenuPrincipal();
                    }
                    if (operacao == '2')
                    {
                        Console.WriteLine("A data de aniversario que deseja botar");
                        string dataAniversarioEditado = Console.ReadLine();

                        string dataconvertida = Convert.ToString(aniversariante.dataAniversario);

                        string text = File.ReadAllText(local);
                        text = text.Replace(dataconvertida, dataAniversarioEditado);
                        File.WriteAllText(local, text);
                        Console.WriteLine("Editado com sucesso");
                        Console.WriteLine("Aperte qualquer tecla para continuar");
                        Console.ReadKey();
                        MenuPrincipal();
                    }
                    if (operacao >= '4')
                    {
                        MenuPrincipal();
                    }
                }

            }
        }
        public static void DeletarAniversariante()
        {
            foreach (var aniversariante in BancoDeDados.BuscarTodosOsAniversarios())
            {
                Console.WriteLine($"Nome: {aniversariante.nomeCompleto} aniversario em: {aniversariante.dataAniversario}");
            }
            Console.WriteLine("------------ Digite o nome COMPLETO que deseja deletar ------------");
            string nomeVarBusca = Console.ReadLine();
            var aniversarianteRemover = BancoDeDados.BuscarTodosOsAniversarios(nomeVarBusca);
            int quantidadeDeaniversarianteEncontrados = aniversarianteRemover.Count();

            if (quantidadeDeaniversarianteEncontrados == 0)
            {
                Console.WriteLine("Nenhum aniversariante encontrado");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                MenuPrincipal();
            }
            BancoDeDados.Remover(nomeVarBusca);

            Console.WriteLine("Aniversariante Removido Com Sucesso");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();

        }
        public static BancoDeDados BancoDeDados
        {
            get
            {
                return new BancoDeDadosEmArquivo();
            }
        }
    }

}
