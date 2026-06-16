using DSCITY.Data;
using Microsoft.EntityFrameworkCore;

namespace DSCITY.Services;

public class AdminWorkspace
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public AdminWorkspace(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public List<CompanyRecord> Companies
    {
        get
        {
            using var db = _factory.CreateDbContext();
            return db.Companies.AsNoTracking().OrderByDescending(x => x.Seq).ToList();
        }
    }

    public List<RegistrationRecord> Registrations
    {
        get
        {
            using var db = _factory.CreateDbContext();
            return db.Registrations.AsNoTracking().OrderByDescending(x => x.Seq).ToList();
        }
    }

    public List<ContractRecord> Contracts
    {
        get
        {
            using var db = _factory.CreateDbContext();
            return db.Contracts.AsNoTracking().OrderByDescending(x => x.Seq).ToList();
        }
    }

    public CompanyRecord CreateCompanyDraft() => new() { Id = Guid.NewGuid(), Status = "Chờ thẩm định", ContractInfo = "Chưa tạo hợp đồng" };
    public RegistrationRecord CreateRegistrationDraft() => new() { Id = Guid.NewGuid(), Code = $"REG-{DateTime.Now:yyMMdd-HHmm}", Priority = "Trung bình", Status = "Tiếp nhận", Sla = "Mới tạo" };

    public ContractRecord CreateContractDraft()
    {
        using var db = _factory.CreateDbContext();
        var count = db.Contracts.Count();
        return new() { Id = Guid.NewGuid(), Number = $"DSC-{DateTime.Now:yyyy}-{count + 1:000}", Version = "V1.0", Status = "Bản nháp", EffectivePeriod = "Chờ cập nhật" };
    }

    public void SaveCompany(CompanyRecord draft)
    {
        using var db = _factory.CreateDbContext();
        var existing = db.Companies.FirstOrDefault(x => x.Id == draft.Id);
        if (existing is null)
        {
            var clone = draft.Clone();
            clone.Seq = 0;
            db.Companies.Add(clone);
            db.SaveChanges();
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
        db.SaveChanges();
    }

    public bool DeleteCompany(Guid id)
    {
        using var db = _factory.CreateDbContext();
        var existing = db.Companies.FirstOrDefault(x => x.Id == id);
        if (existing is null) return false;
        db.Companies.Remove(existing);
        db.SaveChanges();
        return true;
    }

    public void SaveRegistration(RegistrationRecord draft)
    {
        using var db = _factory.CreateDbContext();
        var existing = db.Registrations.FirstOrDefault(x => x.Id == draft.Id);
        if (existing is null)
        {
            var clone = draft.Clone();
            clone.Seq = 0;
            db.Registrations.Add(clone);
            db.SaveChanges();
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
        db.SaveChanges();
    }

    public bool DeleteRegistration(Guid id)
    {
        using var db = _factory.CreateDbContext();
        var existing = db.Registrations.FirstOrDefault(x => x.Id == id);
        if (existing is null) return false;
        db.Registrations.Remove(existing);
        db.SaveChanges();
        return true;
    }

    public void SaveContract(ContractRecord draft)
    {
        using var db = _factory.CreateDbContext();
        var existing = db.Contracts.FirstOrDefault(x => x.Id == draft.Id);
        if (existing is null)
        {
            var clone = draft.Clone();
            clone.Seq = 0;
            db.Contracts.Add(clone);
            db.SaveChanges();
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
        db.SaveChanges();
    }

    public bool DeleteContract(Guid id)
    {
        using var db = _factory.CreateDbContext();
        var existing = db.Contracts.FirstOrDefault(x => x.Id == id);
        if (existing is null) return false;
        db.Contracts.Remove(existing);
        db.SaveChanges();
        return true;
    }
}

public class CompanyRecord
{
    public Guid Id { get; set; }
    public int Seq { get; set; }
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
    public int Seq { get; set; }
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
    public int Seq { get; set; }
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
