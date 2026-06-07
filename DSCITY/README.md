# DSCITY Admin

Web admin cho doanh nghiep hop tac, ho so dang ky va hop dong cua DSCITY, xay dung bang Blazor Web App tren .NET.

## Chuc nang hien tai

- Dashboard tong quan
- Quan ly doanh nghiep doi tac
- Quan ly ho so dang ky
- Quan ly hop dong
- Phe duyet va bao cao
- Them, sua, xoa, cap nhat trang thai tren giao dien

## Luu y hien tai

Du lieu dang duoc luu bang `in-memory state service`.
Dieu nay co nghia la:

- Thao tac them, sua, xoa hoat dong ngay trong phien chay hien tai
- Khi restart app, du lieu se quay ve bo mau mac dinh

Neu can luu that, buoc tiep theo nen ket noi `SQL Server + EF Core`.

## Cau truc chinh

- `Components/Layout`: layout, menu, topbar
- `Components/Pages`: cac man hinh admin
- `Services/AdminWorkspace.cs`: du lieu tam va logic CRUD co ban
- `wwwroot/app.css`: giao dien va design system

## Yeu cau moi truong

- .NET SDK 10
- Windows + PowerShell / CMD

## Cach chay

Mo terminal tai thu muc project:

```powershell
cd "D:\New folder\DSCITY-AD\DSCITY-ADMIN\DSCITY"
```

Chay app:

```powershell
cmd /c ".\start.cmd"
```

Dung app:

```powershell
cmd /c ".\stop.cmd"
```

Neu muon chay truc tiep bang dotnet:

```powershell
dotnet run
```

## Dia chi local

- http://localhost:5285

## Build project

```powershell
dotnet build
```

## Cac file ho tro da tao

- `start.ps1`
- `stop.ps1`
- `start.cmd`
- `stop.cmd`

## Huong phat trien tiep theo

1. Ket noi database that bang EF Core
2. Them validation cho form
3. Them modal xac nhan xoa
4. Them tim kiem, loc, sap xep
5. Them dang nhap va phan quyen
