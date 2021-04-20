using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.Series.Context;
using DIO.Series.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DIO.Series
{
    public class SerieRepository : IRepository<Serie>
	{
		private readonly SeriesDbContext _context;

        public SerieRepository(SeriesDbContext context) => _context = context;

        public async Task Atualizar(Serie serie)
		{
			_context.Series.Update(serie);
			await _context.SaveChangesAsync();
		}

		public async Task Excluir(Serie serie)
		{
			_context.Series.Remove(serie);
			await _context.SaveChangesAsync();
		}

		public async Task Adicionar(Serie serie)
		{
			_context.Series.Add(serie);
			await _context.SaveChangesAsync();
		}

        public async Task<List<Serie>> Listar() => await _context.Series.ToListAsync();

        public async Task<Serie> RetornaPorId(Guid id) => await _context.Series.FindAsync(id);
    }
}