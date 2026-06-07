namespace DSCITY.Services;

public class AdminWorkspace
{
    public List<CompanyRecord> Companies { get; } =
    [
        new() { Id = Guid.NewGuid(), Name = "Central Plaza Parking", TaxCode = "0312456789", Category = "Bai do xe thong minh", Contact = "Nguyen Phuong Linh", Location = "Quan 1, TP.HCM", Status = "Hoat dong", ContractInfo = "Het han 28/06/2026", Notes = "Doi tac chien luoc, uu tien gia han som." },
        new() { Id = Guid.NewGuid(), Name = "Skyline Mobility", TaxCode = "0319823344", Category = "Cho thue xe may dien", Contact = "Tran Duc Anh", Location = "Thu Duc, TP.HCM", Status = "Cho bo sung", ContractInfo = "Ban nhap", Notes = "Dang cho ban cong chung moi." },
        new() { Id = Guid.NewGuid(), Name = "Urban Charge Hub", TaxCode = "0401122233", Category = "Tram sac xe dien", Contact = "Le Minh Chau", Location = "Da Nang", Status = "Cho ky", ContractInfo = "V2.1 - Cho doi tac", Notes = "Can chot phu luc van hanh." },
        new() { Id = Guid.NewGuid(), Name = "Green Transit Services", TaxCode = "0107788991", Category = "Van tai cong cong", Contact = "Pham Quoc Bao", Location = "Ha Noi", Status = "Tam dung", ContractInfo = "Da thanh ly", Notes = "Tam dung sau dot tai cau truc." }
    ];

    public List<RegistrationRecord> Registrations { get; } =
    [
        new() { Id = Guid.NewGuid(), Code = "REG-240607-01", CompanyName = "Skyline Mobility", CurrentStep = "Kiem tra giay phep kinh doanh", Owner = "Nguyen Hoang Oanh", Priority = "Cao", Sla = "Qua han 4 gio", Status = "Cho bo sung", Notes = "Can bo sung giay uy quyen va GPKD." },
        new() { Id = Guid.NewGuid(), Code = "REG-240607-02", CompanyName = "Urban Charge Hub", CurrentStep = "Cho legal review", Owner = "Le Tuan Kiet", Priority = "Trung binh", Sla = "Con 1 ngay", Status = "Cho tham dinh", Notes = "Da du tai lieu ky thuat." },
        new() { Id = Guid.NewGuid(), Code = "REG-240607-03", CompanyName = "Central Plaza Parking", CurrentStep = "Chuan bi phu luc gia han", Owner = "Pham Khanh Linh", Priority = "On dinh", Sla = "Trong SLA", Status = "Cho phe duyet", Notes = "Co the day qua hop dong trong ngay." }
    ];

    public List<ContractRecord> Contracts { get; } =
    [
        new() { Id = Guid.NewGuid(), Number = "DSC-2026-041", CompanyName = "Central Plaza Parking", Version = "V3.0", EffectivePeriod = "01/07/2026 - 30/06/2027", Status = "Da ky", Value = "4.2B", Owner = "Legal Team", Notes = "Da ky va cho dong bo phu luc thanh toan." },
        new() { Id = Guid.NewGuid(), Number = "DSC-2026-057", CompanyName = "Urban Charge Hub", Version = "V2.1", EffectivePeriod = "Cho kich hoat", Status = "Cho ky", Value = "1.8B", Owner = "Sales Team", Notes = "Dang doi dai dien doanh nghiep ky so." },
        new() { Id = Guid.NewGuid(), Number = "DSC-2026-063", CompanyName = "Skyline Mobility", Version = "V1.2", EffectivePeriod = "Ban nhap", Status = "Can bo sung", Value = "950M", Owner = "Partnership Team", Notes = "Can bo sung dieu khoan bao hanh pin." }
    ];

    public CompanyRecord CreateCompanyDraft() => new() { Id = Guid.NewGuid(), Status = "Cho tham dinh", ContractInfo = "Chua tao hop dong" };
    public RegistrationRecord CreateRegistrationDraft() => new() { Id = Guid.NewGuid(), Code = $"REG-{DateTime.Now:yyMMdd-HHmm}", Priority = "Trung binh", Status = "Tiep nhan", Sla = "Moi tao" };
    public ContractRecord CreateContractDraft() => new() { Id = Guid.NewGuid(), Number = $"DSC-{DateTime.Now:yyyy}-{Contracts.Count + 1:000}", Version = "V1.0", Status = "Ban nhap", EffectivePeriod = "Cho cap nhat" };

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
            Registrations.Insert(0, draft.Clone());
            return;
        }

        existing.Code = draft.Code;
        existing.CompanyName = draft.CompanyName;
        existing.CurrentStep = draft.CurrentStep;
        existing.Owner = draft.Owner;
        existing.Priority = draft.Priority;
        existing.Sla = draft.Sla;
        existing.Status = draft.Status;
        existing.Notes = draft.Notes;
    }

    public bool DeleteRegistration(Guid id)
    {
        var existing = Registrations.FirstOrDefault(x => x.Id == id);
        return existing is not null && Registrations.Remove(existing);
    }

    public void SaveContract(ContractRecord draft)
    {
        var existing = Contracts.FirstOrDefault(x => x.Id == draft.Id);
        if (existing is null)
        {
            Contracts.Insert(0, draft.Clone());
            return;
        }

        existing.Number = draft.Number;
        existing.CompanyName = draft.CompanyName;
        existing.Version = draft.Version;
        existing.EffectivePeriod = draft.EffectivePeriod;
        existing.Status = draft.Status;
        existing.Value = draft.Value;
        existing.Owner = draft.Owner;
        existing.Notes = draft.Notes;
    }

    public bool DeleteContract(Guid id)
    {
        var existing = Contracts.FirstOrDefault(x => x.Id == id);
        return existing is not null && Contracts.Remove(existing);
    }
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
