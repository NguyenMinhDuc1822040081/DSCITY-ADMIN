# DSCITY Admin

Web admin cho doanh nghiệp hợp tác, hồ sơ đăng ký và hợp đồng của DSCITY, xây dựng bằng Blazor Web App trên .NET.

## Chức năng hiện tại

- Bảng điều khiển tổng quan
- Quản lý doanh nghiệp đối tác
- Quản lý hồ sơ đăng ký
- Quản lý hợp đồng
- Phê duyệt và báo cáo
- Thêm, sửa, xóa, cập nhật trạng thái trực tiếp trên giao diện

## Lưu ý hiện tại

Dữ liệu đang được lưu bằng `in-memory state service`.
Điều này có nghĩa là:

- Thao tác thêm, sửa, xóa hoạt động ngay trong phiên chạy hiện tại
- Khi khởi động lại app, dữ liệu sẽ quay về bộ mẫu mặc định

Nếu cần lưu thật, bước tiếp theo nên kết nối `SQL Server + EF Core`.

## Cấu trúc chính

- `Components/Layout`: layout, menu, topbar
- `Components/Pages`: các màn hình admin
- `Services/AdminWorkspace.cs`: dữ liệu tạm và logic CRUD cơ bản
- `wwwroot/app.css`: giao diện và design system

## Yêu cầu môi trường

- .NET SDK 10
- Windows + PowerShell / CMD

## Cách chạy

Mở terminal tại thư mục project:

```powershell
cd "D:\New folder\DSCITY-AD\DSCITY-ADMIN\DSCITY"
```

Chạy app:

```powershell
cmd /c ".\start.cmd"
```

Dừng app:

```powershell
cmd /c ".\stop.cmd"
```

Nếu muốn chạy trực tiếp bằng dotnet:

```powershell
dotnet run
```

## Địa chỉ local

- http://localhost:5285

## Build project

```powershell
dotnet build
```

## Các file hỗ trợ đã tạo

- `start.ps1`
- `stop.ps1`
- `start.cmd`
- `stop.cmd`

## Hướng phát triển tiếp theo

1. Kết nối database thật bằng EF Core
2. Thêm validation cho form
3. Thêm modal xác nhận xóa
4. Thêm tìm kiếm, lọc, sắp xếp
5. Thêm đăng nhập và phân quyền
