using System;
using System.Threading.Tasks;
using OganiProject.Entities;
using OganiProject.Repository;

namespace OganiProject.UniteOfWork
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveChangeAsync();
    }
}
