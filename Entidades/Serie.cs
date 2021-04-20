using System.Text;

namespace DIO.Series
{
    public class Serie : EntidadeBase
    {
        // Atributos
		public Genero Genero { get; set; }
		public string Titulo { get; set; }
		public string Descricao { get; set; }
		public int Ano { get; set; }
        public bool Excluido {get; set;}

        public override string ToString()
		{
            StringBuilder retorno = new StringBuilder();
            retorno.AppendLine($"Gênero: {this.Genero}");
			retorno.AppendLine($"Título: {this.Titulo}");
			retorno.AppendLine($"Descrição: {this.Descricao}");
			retorno.AppendLine($"Ano de Início: {this.Ano}");
			return retorno.ToString();
		}
    }
}