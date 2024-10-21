using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class DeThi
    {
        public Guid Id { get; set; } 
        public string TenDeThi { get; set; }
        public string MonHoc { get; set; }
        public DateTime NgayThi { get; set; }
        public int ThoiGianLamBai {  get; set; }    
        public int SoLuongCauHoi { get; set; }
        public string Status { get; set; }
    }
}
