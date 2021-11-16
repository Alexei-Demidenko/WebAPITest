using System;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<Announcing> Announcings { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
