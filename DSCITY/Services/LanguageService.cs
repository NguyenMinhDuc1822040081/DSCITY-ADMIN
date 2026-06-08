namespace DSCITY.Services;

public sealed class LanguageService
{
    private bool _isEnglish;

    public bool IsEnglish => _isEnglish;
    public string CurrentCode => _isEnglish ? "EN" : "VI";
    public event Action? OnChange;

    public void SetEnglish(bool value)
    {
        if (_isEnglish == value)
        {
            return;
        }

        _isEnglish = value;
        OnChange?.Invoke();
    }

    public string Text(string vietnamese, string english) => _isEnglish ? english : vietnamese;

    public string Translate(string value)
    {
        if (!_isEnglish || string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        return value switch
        {
            "Hoạt động" => "Active",
            "Chờ thẩm định" => "Pending review",
            "Chờ bổ sung" => "Needs revision",
            "Chờ ký" => "Pending signature",
            "Tạm dừng" => "Paused",
            "Đã ký" => "Signed",
            "Kích hoạt" => "Activated",
            "Bản nháp" => "Draft",
            "Cần bổ sung" => "Need updates",
            "Tiếp nhận" => "Intake",
            "Chờ phê duyệt" => "Pending approval",
            "Hoàn tất" => "Completed",
            "Cao" => "High",
            "Trung bình" => "Medium",
            "Ổn định" => "Stable",
            "Bãi đỗ xe thông minh" => "Smart parking",
            "Cho thuê xe máy điện" => "Electric scooter rental",
            "Cho thuê xe điện" => "Electric vehicle rental",
            "Trạm sạc xe điện" => "EV charging hub",
            "Trạm sạc" => "Charging hub",
            "Vận tải công cộng" => "Public transit",
            "Hết hạn 28/06/2026" => "Expires 28/06/2026",
            "Gia hạn 06/2026" => "Renewal 06/2026",
            "V2.1 - Chờ đối tác" => "V2.1 - Waiting for partner",
            "Đã thanh lý" => "Terminated",
            "Chưa tạo hợp đồng" => "Contract not created",
            "Bản công chứng mới" => "New notarized copy",
            "Kiểm tra giấy phép kinh doanh" => "Check business license",
            "Chờ pháp chế rà soát" => "Waiting for legal review",
            "Chuẩn bị phụ lục gia hạn" => "Prepare renewal appendix",
            "Quá hạn 4 giờ" => "Overdue by 4 hours",
            "Còn 1 ngày" => "1 day left",
            "Trong SLA" => "Within SLA",
            "Mới tạo" => "Newly created",
            "Chờ kích hoạt" => "Pending activation",
            "Chờ cập nhật" => "Pending update",
            "Đã đẩy sang luồng duyệt" => "Moved to approval flow",
            "Chờ phê duyệt nội bộ" => "Pending internal approval",
            "Nhóm pháp chế" => "Legal team",
            "Nhóm kinh doanh" => "Sales team",
            "Nhóm đối tác" => "Partnership team",
            "Đối tác chiến lược, ưu tiên gia hạn sớm." => "Strategic partner, prioritize early renewal.",
            "Đang chờ bản công chứng mới." => "Waiting for a new notarized copy.",
            "Cần chốt phụ lục vận hành." => "Operational appendix still needs sign-off.",
            "Tạm dừng sau đợt tái cấu trúc." => "Paused after the restructuring phase.",
            "Cần bổ sung giấy ủy quyền và GPKD." => "Need to add power of attorney and business license.",
            "Đã đủ tài liệu kỹ thuật." => "Technical documentation is complete.",
            "Có thể đẩy qua hợp đồng trong ngày." => "Can move to contract stage today.",
            "Đã ký và chờ đồng bộ phụ lục thanh toán." => "Signed and waiting to sync the payment appendix.",
            "Đang đợi đại diện doanh nghiệp ký số." => "Waiting for the company representative to e-sign.",
            "Cần bổ sung điều khoản bảo hành pin." => "Battery warranty clause needs to be added.",
            "Đang mở hồ sơ:" => "Opening dossier:",
            "Đang tạo doanh nghiệp mới." => "Creating a new company.",
            "Đã lưu hồ sơ doanh nghiệp:" => "Saved company dossier:",
            "Đã hoàn tác thay đổi chưa lưu." => "Reverted unsaved changes.",
            "Đã xóa doanh nghiệp:" => "Deleted company:",
            "Đang xử lý hồ sơ:" => "Processing dossier:",
            "Đang tạo hồ sơ đăng ký mới." => "Creating a new registration dossier.",
            "Đã lưu hồ sơ:" => "Saved dossier:",
            "Đã xóa hồ sơ:" => "Deleted dossier:",
            "Đang mở hợp đồng:" => "Opening contract:",
            "Đang tạo hợp đồng mới." => "Creating a new contract.",
            "Đã lưu hợp đồng:" => "Saved contract:",
            "Đã xóa hợp đồng:" => "Deleted contract:",
            _ => value
        };
    }
}
