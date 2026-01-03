# 🛒 SecondHandProject – İkinci El Eşya Satış Platformu

## 📌 Proje Tanımı
SecondHandProject, ASP.NET Core MVC kullanılarak geliştirilmiş, kullanıcıların ikinci el ürünlerini ekleyip yönetebildiği; diğer kullanıcıların ise bu ürünleri görüntüleyip filtreleyebildiği bir web uygulamasıdır. 

---

## 🎯 Projenin Amacı
Bu projenin amacı:
- ASP.NET Core MVC mimarisini etkin şekilde uygulamak
- Entity Framework Core ile **Code-First** yaklaşımını ve **Migrations** yapısını kullanmak
- ASP.NET Core Identity ile **Authentication & Authorization** mekanizmalarını kurmak
- Rol bazlı yetkilendirme (Admin / User) uygulamak
- Katmanlı mimari (Controller – Service – Repository) geliştirmek
- ViewModel kullanımı ile güvenli veri aktarımı sağlamak

---

## 🧱 Kullanılan Teknolojiler
- ASP.NET Core MVC (LTS)
- Entity Framework Core
- ASP.NET Core Identity
- MS SQL Server
- Bootstrap
- Razor View Engine
- C#

---

## 🏗️ Mimari Yapı
Proje, **MVC + Service + Repository Pattern** kullanılarak geliştirilmiştir.

- **Controllers**: HTTP isteklerini yönetir
- **Services**: İş mantığını içerir
- **Repositories**: Veritabanı erişimini yönetir
- **Models (Entities)**: Veritabanı tablolarını temsil eder
- **ViewModels**: View’lara özel veri taşır
- **Views**: Kullanıcı arayüzleri
- **Helpers**: Ortak yardımcı sınıflar (ör. resim yükleme)

---

## 👥 Roller ve Yetkilendirme

### 🔹 User
- Kayıt olabilir ve giriş yapabilir
- Ürün ekleyebilir, düzenleyebilir, silebilir
- Kendi ürünlerini görüntüleyebilir
- Profil bilgilerini düzenleyebilir
- Şifre değiştirebilir

### 🔹 Admin
- Tüm kullanıcıları yönetebilir
- Tüm ürünleri silebilir
- Kategoriler üzerinde CRUD işlemleri yapabilir

Rol bazlı yetkilendirme `[Authorize]` ve `[Authorize(Roles = "Admin")]` attribute’ları ile sağlanmıştır.

---

## 🔐 Authentication & Authorization
- ASP.NET Core Identity kullanılmıştır
- Register ve Login işlemleri mevcuttur
- Cookie tabanlı kimlik doğrulama yapılmaktadır
- Yetkisiz erişimler `AccessDenied` sayfasına yönlendirilir

---

## 🗃️ Veritabanı ve EF Core
- **Code-First** yaklaşımı kullanılmıştır
- `ApplicationDbContext` üzerinden Entity tanımlamaları yapılmıştır
- Veritabanı şeması **Migrations** ile oluşturulmuştur
- Proje ilk çalıştırıldığında otomatik `Database.Migrate()` işlemi yapılır

---

## 🌱 SeedData
Uygulama ilk çalıştırıldığında otomatik olarak:
- Admin ve User rolleri oluşturulur
- Varsayılan Admin kullanıcı eklenir
- Kategoriler ve örnek ürünler veritabanına eklenir

---

## 🖼️ Resim Yükleme
- Ürün görselleri `wwwroot/uploads` klasöründe saklanır
- Dosya yükleme işlemleri `ImageHelper` sınıfı ile yapılır
- GUID kullanılarak dosya adı çakışmaları önlenir
- Güncellenen ürünlerde eski görseller silinir

---

## 🗃️ Veritabanı ve Migration

Bu proje **Entity Framework Core Code-First** yaklaşımı ile geliştirilmiştir.

Migration dosyaları GitHub’a yüklenmemiştir.  
Projeyi ilk kez çalıştırmadan önce aşağıdaki adımlar izlenmelidir:

```bash
add-migration InitialCreate
update-database
-------------------
Varsayılan Admin Bilgileri
Email: admin@secondhand.com
Şifre: Admin123!


