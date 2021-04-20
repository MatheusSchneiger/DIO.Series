using System;
using DIO.Series.Context;
using DIO.Series.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DIO.Series
{
    class Program
    {
        static IRepository<Serie> repositorio;

        static async Task Main(string[] args)
        {
			ConfigurarServicos(args);

            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						await ListarSeries();
						break;
					case "2":
						await InserirSerie();
						break;
					case "3":
						await AtualizarSerie();
						break;
					case "4":
						await ExcluirSerie();
						break;
					case "5":
						await VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

		private static void ConfigurarServicos(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                services.AddDbContext<SeriesDbContext>(options => options.UseInMemoryDatabase("Series"))
                        .AddScoped<IRepository<Serie>, SerieRepository>()).Build();

            IServiceScope scope = host.Services.CreateScope();
            var provider = scope.ServiceProvider;
            repositorio = provider.GetRequiredService<IRepository<Serie>>();
        }

        private static async Task ExcluirSerie()
		{
			Console.Write("Digite o id da série: ");
			Guid indiceSerie = Guid.Parse(Console.ReadLine());

			var serie = await repositorio.RetornaPorId(indiceSerie);

			await repositorio.Excluir(serie);
		}

        private static async Task VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			Guid indiceSerie = Guid.Parse(Console.ReadLine());

			var serie = await repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static async Task AtualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			Guid indiceSerie = Guid.Parse(Console.ReadLine());

			var serie = await repositorio.RetornaPorId(indiceSerie);

			foreach (int i in Enum.GetValues(typeof(Genero)))
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));

			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			serie.Genero = (Genero)entradaGenero;
			serie.Titulo = entradaTitulo;
			serie.Ano = entradaAno;
			serie.Descricao = entradaDescricao;

			await repositorio.Atualizar(serie);
		}
        private static async Task ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = await repositorio.Listar();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
				Console.WriteLine($"#ID {serie.Id}: - {serie.Titulo}");
		}

        private static async Task InserirSerie()
		{
			Console.WriteLine("Inserir nova série");

			foreach (int i in Enum.GetValues(typeof(Genero)))
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));

			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			var novaSerie = new Serie
			{
				Genero = (Genero)entradaGenero,
				Titulo = entradaTitulo,
				Ano = entradaAno,
				Descricao = entradaDescricao
			};

			await repositorio.Adicionar(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
