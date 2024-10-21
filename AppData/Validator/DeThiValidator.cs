using AppData.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validator
{
    public class DeThiValidator : AbstractValidator<DeThi>
    {
        public DeThiValidator()
        {
            RuleFor(d => d.TenDeThi)
                .NotEmpty().WithMessage("Tên đề thi không được để trống");
            RuleFor(d => d.MonHoc)
                .NotEmpty().WithMessage("Môn học không được để trống");
            RuleFor(d => d.NgayThi)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Ngày thi không được trong quá khứ");
            RuleFor(d => d.ThoiGianLamBai)
                .GreaterThan(0).WithMessage("Thời gian làm bài phải lớn hơn 0 phút");
            RuleFor(d => d.SoLuongCauHoi)
                .GreaterThan(0).WithMessage("Số lượng câu hỏi phải lớn hơn 0");
            RuleFor(d => d.Status)
                .Must(trangThai => trangThai == "Đang diễn ra" || trangThai == "Đã kết thúc" || trangThai == "Đã hủy")
                .WithMessage("Trạng thái không hợp lệ. Phải là 'Đang diễn ra', 'Đã kết thúc', hoặc 'Đã hủy'.");
        }
    }
}
