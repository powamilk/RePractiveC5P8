using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public class DeThiRepo : IDeThiRepo
    {
        private readonly AppDbContext _context;

        public DeThiRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(DeThi deThi)
        {
            await _context.DeThis.AddAsync(deThi);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var deThi = await _context.DeThis.FindAsync(id);
            if(deThi != null && deThi.Status != "Đang diễn ra")
            {
                _context.DeThis.Remove(deThi);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<IEnumerable<DeThi>> GetAllAsync()
        {
            return await _context.DeThis.ToListAsync();
        }

        public async Task<DeThi> GetByIdAsync(Guid id)
        {
            return await _context.DeThis.FindAsync(id);
        }

        public async Task UpdateAsync(DeThi deThi)
        {
            _context.DeThis.Update(deThi);
            await _context.SaveChangesAsync();
        }
    }
}
