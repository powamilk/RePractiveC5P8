using AppData;
using AppData.Entities;
using AppData.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeThiController : ControllerBase
    {
        private readonly IDeThiRepo _deThiRepo;
        private readonly IValidator<DeThi> _validator;
        private readonly AppDbContext _context;

        public DeThiController(IDeThiRepo deThiRepo, IValidator<DeThi> validator, AppDbContext context)
        {
            _deThiRepo = deThiRepo;
            _validator = validator;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var deThis = await _deThiRepo.GetAllAsync();
            return Ok(deThis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var deThi = await _deThiRepo.GetByIdAsync(id);
            if (deThi == null)
            {
                return NotFound();
            }
            return Ok(deThi);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(DeThi deThi)
        {
            var validatorResult = await _validator.ValidateAsync(deThi);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }));
            }

            await _deThiRepo.CreateAsync(deThi);
            return Ok(deThi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DeThi deThi)
        {
            if (id != deThi.Id)
            {
                return BadRequest("ID không khớp");
            }

            var validatorResult = await _validator.ValidateAsync(deThi);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }));
            }

            await _deThiRepo.UpdateAsync(deThi);
            return Ok(deThi);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deThi = await _deThiRepo.GetByIdAsync(id);

            if (deThi == null)
            {
                return NotFound();
            }
            if (deThi.Status == "Đang diễn ra")
            {
                return BadRequest("Không thể xóa đề thi khi trạng thái là 'Đang diễn ra'");
            }

            await _deThiRepo.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByMonHocAndTrangThai([FromQuery] string monHoc, [FromQuery] string trangThai)
        {
            if (string.IsNullOrEmpty(monHoc) || string.IsNullOrEmpty(trangThai))
            {
                return BadRequest("Môn học và trạng thái không được để trống");
            }

            var deThis = await _context.DeThis
                                       .Where(d => d.MonHoc == monHoc && d.Status == trangThai)
                                       .ToListAsync();

            if (!deThis.Any())
            {
                return NotFound("Không tìm thấy đề thi nào với môn học và trạng thái đã cung cấp.");
            }

            return Ok(deThis);
        }
    }
}
