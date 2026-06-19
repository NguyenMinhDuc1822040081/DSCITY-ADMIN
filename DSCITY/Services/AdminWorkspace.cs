namespace DSCITY.Services;

public class AdminWorkspace
{
    private readonly List<AuditLogEntry> _auditLogs = [];

    public List<CompanyRecord> Companies { get; } =
    [
        new() { Id = Guid.NewGuid(), Name = "Central Plaza Parking", TaxCode = "0312456789", Category = "Bãi đỗ xe thông minh", Contact = "Nguyễn Phương Linh", Location = "Quận 1, TP.HCM", Status = "Hoạt động", ContractInfo = "Hết hạn 28/06/2026", Notes = "Đối tác chiến lược, ưu tiên gia hạn sớm." },
        new() { Id = Guid.NewGuid(), Name = "Skyline Mobility", TaxCode = "0319823344", Category = "Cho thuê xe máy điện", Contact = "Trần Đức Anh", Location = "Thủ Đức, TP.HCM", Status = "Chờ bổ sung", ContractInfo = "Bản nháp", Notes = "Đang chờ bản công chứng mới." },
        new() { Id = Guid.NewGuid(), Name = "Urban Charge Hub", TaxCode = "0401122233", Category = "Trạm sạc xe điện", Contact = "Lê Minh Châu", Location = "Đà Nẵng", Status = "Chờ ký", ContractInfo = "V2.1 - Chờ đối tác", Notes = "Cần chốt phụ lục vận hành." },
        new() { Id = Guid.NewGuid(), Name = "Green Transit Services", TaxCode = "0107788991", Category = "Vận tải công cộng", Contact = "Phạm Quốc Bảo", Location = "Hà Nội", Status = "Tạm dừng", ContractInfo = "Đã thanh lý", Notes = "Tạm dừng sau đợt tái cấu trúc." }
    ];

    public List<RegistrationRecord> Registrations { get; } =
    [
        new() { Id = Guid.NewGuid(), Code = "REG-240607-01", CompanyName = "Skyline Mobility", CurrentStep = "Kiểm tra giấy phép kinh doanh", Owner = "Nguyễn Hoàng Oanh", Priority = "Cao", Sla = "Quá hạn 4 giờ", Status = "Chờ bổ sung", Notes = "Cần bổ sung giấy ủy quyền và GPKD." },
        new() { Id = Guid.NewGuid(), Code = "REG-240607-02", CompanyName = "Urban Charge Hub", CurrentStep = "Chờ pháp chế rà soát", Owner = "Lê Tuấn Kiệt", Priority = "Trung bình", Sla = "Còn 1 ngày", Status = "Chờ thẩm định", Notes = "Đã đủ tài liệu kỹ thuật." },
        new() { Id = Guid.NewGuid(), Code = "REG-240607-03", CompanyName = "Central Plaza Parking", CurrentStep = "Chuẩn bị phụ lục gia hạn", Owner = "Phạm Khánh Linh", Priority = "Ổn định", Sla = "Trong SLA", Status = "Chờ phê duyệt", Notes = "Có thể đẩy qua hợp đồng trong ngày." }
    ];

    public List<ContractRecord> Contracts { get; } =
    [
        new() { Id = Guid.NewGuid(), Number = "DSC-2026-041", CompanyName = "Central Plaza Parking", Version = "V3.0", EffectivePeriod = "01/07/2026 - 30/06/2027", Status = "Đã ký", Value = "4.2B", Owner = "Nhóm pháp chế", Notes = "Đã ký và chờ đồng bộ phụ lục thanh toán." },
        new() { Id = Guid.NewGuid(), Number = "DSC-2026-057", CompanyName = "Urban Charge Hub", Version = "V2.1", EffectivePeriod = "Chờ kích hoạt", Status = "Chờ ký", Value = "1.8B", Owner = "Nhóm kinh doanh", Notes = "Đang đợi đại diện doanh nghiệp ký số." },
        new() { Id = Guid.NewGuid(), Number = "DSC-2026-063", CompanyName = "Skyline Mobility", Version = "V1.2", EffectivePeriod = "Bản nháp", Status = "Cần bổ sung", Value = "950M", Owner = "Nhóm đối tác", Notes = "Cần bổ sung điều khoản bảo hành pin." }
    ];

    public IReadOnlyList<AuditLogEntry> AuditLogs => _auditLogs;

    public CompanyRecord CreateCompanyDraft() => new() { Id = Guid.NewGuid(), Status = "Chờ thẩm định", ContractInfo = "Chưa tạo hợp đồng" };
    public RegistrationRecord CreateRegistrationDraft() => new() { Id = Guid.NewGuid(), Code = $"REG-{DateTime.Now:yyMMdd-HHmm}", Priority = "Trung bình", Status = "Tiếp nhận", Sla = "Mới tạo" };
    public ContractRecord CreateContractDraft() => new() { Id = Guid.NewGuid(), Number = $"DSC-{DateTime.Now:yyyy}-{Contracts.Count + 1:000}", Version = "V1.0", Status = "Bản nháp", EffectivePeriod = "Chờ cập nhật" };

    public void SaveCompany(CompanyRecord draft)
    {
        var existing = Companies.FirstOrDefault(x => x.Id == draft.Id);
        if (existing is null)
        {
            Companies.Insert(0, draft.Clone());
            return;
        }

        existing.Name = draft.Name;
        existing.TaxCode = draft.TaxCode;
        existing.Category = draft.Category;
        existing.Contact = draft.Contact;
        existing.Location = draft.Location;
        existing.Status = draft.Status;
        existing.ContractInfo = draft.ContractInfo;
        existing.Notes = draft.Notes;
    }

    public bool DeleteCompany(Guid id)
    {
        var existing = Companies.FirstOrDefault(x => x.Id == id);
        return existing is not null && Companies.Remove(existing);
    }

    public void SaveRegistration(RegistrationRecord draft)
    {
        var existing = Registrations.FirstOrDefault(x => x.Id == draft.Id);
        if (existing is null)
        {
            var created = draft.Clone();
            Registrations.Insert(0, created);
            AddAuditLog("add", $"Hồ sơ đăng ký {created.Code}", $"Registration dossier {created.Code}", "Chưa có hồ sơ", "No dossier yet", RegistrationSummaryVi(created), RegistrationSummaryEn(created));
            return;
        }

        var beforeVi = RegistrationSummaryVi(existing);
        var beforeEn = RegistrationSummaryEn(existing);

        existing.Code = draft.Code;
        existing.CompanyName = draft.CompanyName;
        existing.CurrentStep = draft.CurrentStep;
        existing.Owner = draft.Owner;
        existing.Priority = draft.Priority;
        existing.Sla = draft.Sla;
        existing.Status = draft.Status;
        existing.Notes = draft.Notes;

        AddAuditLog("edit", $"Hồ sơ đăng ký {existing.Code}", $"Registration dossier {existing.Code}", beforeVi, beforeEn, RegistrationSummaryVi(existing), RegistrationSummaryEn(existing));
    }

    public bool DeleteRegistration(Guid id)
    {
        var existing = Registrations.FirstOrDefault(x => x.Id == id);
        if (existing is null)
        {
            return false;
        }

        var removed = Registrations.Remove(existing);
        if (removed)
        {
            AddAuditLog("delete", $"Hồ sơ đăng ký {existing.Code}", $"Registration dossier {existing.Code}", RegistrationSummaryVi(existing), RegistrationSummaryEn(existing), "Đã xóa hồ sơ khỏi danh sách", "Removed the dossier from the list");
        }

        return removed;
    }

    public void SaveContract(ContractRecord draft)
    {
        var existing = Contracts.FirstOrDefault(x => x.Id == draft.Id);
        if (existing is null)
        {
            var created = draft.Clone();
            Contracts.Insert(0, created);
            AddAuditLog("add", $"Hợp đồng {created.Number}", $"Contract {created.Number}", "Chưa có hợp đồng", "No contract yet", ContractSummaryVi(created), ContractSummaryEn(created));
            return;
        }

        var beforeVi = ContractSummaryVi(existing);
        var beforeEn = ContractSummaryEn(existing);

        existing.Number = draft.Number;
        existing.CompanyName = draft.CompanyName;
        existing.Version = draft.Version;
        existing.EffectivePeriod = draft.EffectivePeriod;
        existing.Status = draft.Status;
        existing.Value = draft.Value;
        existing.Owner = draft.Owner;
        existing.Notes = draft.Notes;

        AddAuditLog("edit", $"Hợp đồng {existing.Number}", $"Contract {existing.Number}", beforeVi, beforeEn, ContractSummaryVi(existing), ContractSummaryEn(existing));
    }

    public bool DeleteContract(Guid id)
    {
        var existing = Contracts.FirstOrDefault(x => x.Id == id);
        if (existing is null)
        {
            return false;
        }

        var removed = Contracts.Remove(existing);
        if (removed)
        {
            AddAuditLog("delete", $"Hợp đồng {existing.Number}", $"Contract {existing.Number}", ContractSummaryVi(existing), ContractSummaryEn(existing), "Đã xóa hợp đồng khỏi danh sách", "Removed the contract from the list");
        }

        return removed;
    }

    private void AddAuditLog(string actionKey, string targetVi, string targetEn, string beforeVi, string beforeEn, string afterVi, string afterEn)
    {
        _auditLogs.Insert(0, new AuditLogEntry
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.Now,
            ActionKey = actionKey,
            TargetVi = targetVi,
            TargetEn = targetEn,
            BeforeVi = beforeVi,
            BeforeEn = beforeEn,
            AfterVi = afterVi,
            AfterEn = afterEn
        });
    }

    private static string RegistrationSummaryVi(RegistrationRecord registration) =>
        $"Doanh nghiệp: {registration.CompanyName} | Bước: {registration.CurrentStep} | Người phụ trách: {registration.Owner} | Trạng thái: {registration.Status}";

    private static string RegistrationSummaryEn(RegistrationRecord registration) =>
        $"Company: {registration.CompanyName} | Step: {registration.CurrentStep} | Owner: {registration.Owner} | Status: {registration.Status}";

    private static string ContractSummaryVi(ContractRecord contract) =>
        $"Doanh nghiệp: {contract.CompanyName} | Phiên bản: {contract.Version} | Giá trị: {contract.Value} | Trạng thái: {contract.Status}";

    private static string ContractSummaryEn(ContractRecord contract) =>
        $"Company: {contract.CompanyName} | Version: {contract.Version} | Value: {contract.Value} | Status: {contract.Status}";
}

public class AuditLogEntry
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string ActionKey { get; set; } = string.Empty;
    public string TargetVi { get; set; } = string.Empty;
    public string TargetEn { get; set; } = string.Empty;
    public string BeforeVi { get; set; } = string.Empty;
    public string BeforeEn { get; set; } = string.Empty;
    public string AfterVi { get; set; } = string.Empty;
    public string AfterEn { get; set; } = string.Empty;
}

public class CompanyRecord
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TaxCode { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ContractInfo { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public CompanyRecord Clone() => (CompanyRecord)MemberwiseClone();
}

public class RegistrationRecord
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CurrentStep { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Sla { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public RegistrationRecord Clone() => (RegistrationRecord)MemberwiseClone();
}

public class ContractRecord
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string EffectivePeriod { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public ContractRecord Clone() => (ContractRecord)MemberwiseClone();
}
