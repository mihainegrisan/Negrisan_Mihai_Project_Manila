using System.Threading.Tasks;
using Project_Manila.DAL.Interfaces;

namespace Project_Manila.DAL.Configuration
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
