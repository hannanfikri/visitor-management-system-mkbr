using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Appointment;

namespace Visitor.EntityFrameworkCore
{
    public class UnitOfWork : IDisposable
    {
        private readonly VisitorDbContext _context;

        public UnitOfWork(VisitorDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        /*public async Task<long> InsertAndGetIdAsync<TEntity> (TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            using (var unitOfWork = new UnitOfWork(new VisitorDbContext())
            {
                var entity = ObjectMapper.Map<AppointmentEnt>(input);
            }
        }*/
    }
}
