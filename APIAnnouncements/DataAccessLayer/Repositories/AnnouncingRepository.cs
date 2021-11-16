using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    class AnnouncingRepository// : IRepository<Announcing>
    {
        private AnnouncementsContext db;
        public AnnouncingRepository(AnnouncementsContext context)
        {
            this.db = context;
        }

    }
}
