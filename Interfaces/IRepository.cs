using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIO.Series.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> Listar();
        Task<T> RetornaPorId(Guid id);        
        Task Adicionar(T entidade);        
        Task Excluir(T entidade);        
        Task Atualizar(T entidade);
    }
}