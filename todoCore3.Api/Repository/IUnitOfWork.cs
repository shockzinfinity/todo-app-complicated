using System;
using System.Threading.Tasks;

namespace todoCore3.Api.Repository
{
  public interface IUnitOfWork
  {
    Task CompleteAsync();
  }
}
