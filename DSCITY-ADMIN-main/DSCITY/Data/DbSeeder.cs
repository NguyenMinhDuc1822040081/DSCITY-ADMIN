using DSCITY.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSCITY.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var factory = services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        await using var db = await factory.CreateDbContextAsync();

        await db.Database.EnsureCreatedAsync();

        if (!await db.Users.AnyAsync())
        {
            var hasher = new PasswordHasher<PortalUser>();
            var admin = new PortalUser
            {
                Id = Guid.NewGuid(),
                FullName = "Lê Thu Hà",
                Phone = "0123456789",
                Role = "Quản trị viên cấp cao"
            };
            admin.PasswordHash = hasher.HashPassword(admin, "123456");
            db.Users.Add(admin);
        }

        if (!await db.Companies.AnyAsync())
        {
            // Higher Seq is shown first, so the original display order is preserved
            // (Central Plaza Parking on top).
            db.Companies.AddRange(
                new CompanyRecord { Id = Guid.NewGuid(), Seq = 4, Name = "Central Plaza Parking", TaxCode = "0312456789", Category = "Bãi đỗ xe thông minh", Contact = "Nguyễn Phương Linh", Location = "Quận 1, TP.HCM", Status = "Hoạt động", ContractInfo = "Hết hạn 28/06/2026", Notes = "Đối tác chiến lược, ưu tiên gia hạn sớm." },
                new CompanyRecord { Id = Guid.NewGuid(), Seq = 3, Name = "Skyline Mobility", TaxCode = "0319823344", Category = "Cho thuê xe máy điện", Contact = "Trần Đức Anh", Location = "Thủ Đức, TP.HCM", Status = "Chờ bổ sung", ContractInfo = "Bản nháp", Notes = "Đang chờ bản công chứng mới." },
                new CompanyRecord { Id = Guid.NewGuid(), Seq = 2, Name = "Urban Charge Hub", TaxCode = "0401122233", Category = "Trạm sạc xe điện", Contact = "Lê Minh Châu", Location = "Đà Nẵng", Status = "Chờ ký", ContractInfo = "V2.1 - Chờ đối tác", Notes = "Cần chốt phụ lục vận hành." },
                new CompanyRecord { Id = Guid.NewGuid(), Seq = 1, Name = "Green Transit Services", TaxCode = "0107788991", Category = "Vận tải công cộng", Contact = "Phạm Quốc Bảo", Location = "Hà Nội", Status = "Tạm dừng", ContractInfo = "Đã thanh lý", Notes = "Tạm dừng sau đợt tái cấu trúc." }
            );
        }

        if (!await db.Registrations.AnyAsync())
        {
            db.Registrations.AddRange(
                new RegistrationRecord { Id = Guid.NewGuid(), Seq = 3, Code = "REG-240607-01", CompanyName = "Skyline Mobility", CurrentStep = "Kiểm tra giấy phép kinh doanh", Owner = "Nguyễn Hoàng Oanh", Priority = "Cao", Sla = "Quá hạn 4 giờ", Status = "Chờ bổ sung", Notes = "Cần bổ sung giấy ủy quyền và GPKD." },
                new RegistrationRecord { Id = Guid.NewGuid(), Seq = 2, Code = "REG-240607-02", CompanyName = "Urban Charge Hub", CurrentStep = "Chờ pháp chế rà soát", Owner = "Lê Tuấn Kiệt", Priority = "Trung bình", Sla = "Còn 1 ngày", Status = "Chờ thẩm định", Notes = "Đã đủ tài liệu kỹ thuật." },
                new RegistrationRecord { Id = Guid.NewGuid(), Seq = 1, Code = "REG-240607-03", CompanyName = "Central Plaza Parking", CurrentStep = "Chuẩn bị phụ lục gia hạn", Owner = "Phạm Khánh Linh", Priority = "Ổn định", Sla = "Trong SLA", Status = "Chờ phê duyệt", Notes = "Có thể đẩy qua hợp đồng trong ngày." }
            );
        }

        if (!await db.Contracts.AnyAsync())
        {
            db.Contracts.AddRange(
                new ContractRecord { Id = Guid.NewGuid(), Seq = 3, Number = "DSC-2026-041", CompanyName = "Central Plaza Parking", Version = "V3.0", EffectivePeriod = "01/07/2026 - 30/06/2027", Status = "Đã ký", Value = "4.2B", Owner = "Nhóm pháp chế", Notes = "Đã ký và chờ đồng bộ phụ lục thanh toán." },
                new ContractRecord { Id = Guid.NewGuid(), Seq = 2, Number = "DSC-2026-057", CompanyName = "Urban Charge Hub", Version = "V2.1", EffectivePeriod = "Chờ kích hoạt", Status = "Chờ ký", Value = "1.8B", Owner = "Nhóm kinh doanh", Notes = "Đang đợi đại diện doanh nghiệp ký số." },
                new ContractRecord { Id = Guid.NewGuid(), Seq = 1, Number = "DSC-2026-063", CompanyName = "Skyline Mobility", Version = "V1.2", EffectivePeriod = "Bản nháp", Status = "Cần bổ sung", Value = "950M", Owner = "Nhóm đối tác", Notes = "Cần bổ sung điều khoản bảo hành pin." }
            );
        }

        await db.SaveChangesAsync();
    }
}
