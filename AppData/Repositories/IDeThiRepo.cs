using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public interface IDeThiRepo
    {
        Task<IEnumerable<DeThi>> GetAllAsync();
        Task<DeThi> GetByIdAsync(Guid id);
        Task CreateAsync(DeThi deThi);  
        Task UpdateAsync(DeThi deThi);
        Task DeleteAsync(Guid id);  
    }
}
