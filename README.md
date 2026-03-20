**Hotel Reservation System**
C# Console Application ve SQL Server ile geliştirilmiş otel rezervasyon yönetim sistemi.

**Özellikler**

Oda ekleme, silme ve listeleme (müsait/dolu)

Müşteri ekleme ve listeleme

Rezervasyon alma ve iptal etme

Trigger: Rezervasyon alınınca oda otomatik dolar

Trigger: Rezervasyon iptal edilince oda otomatik müsait olur

Stored Procedure ile müsait/dolu oda sorgulama

Tarih aralığına göre müsait oda sorgulama


**Admin giriş**

Projeyi çalıştır

Giriş şifresi: 1234


**Kullanılan Teknolojiler**

C# (.NET Framework 4.7.2)

SQL Server (MSSQL)

Microsoft.Data.SqlClient



Proje Yapısı

├── Oda.cs               → Oda class

├── Musteri.cs           → Musteri class

├── Rezervasyon.cs       → Rezervasyon class

├── Yonetici.cs          → Yonetici class (SQL bağlantıları)

├── Program.cs           → Ana menü

├── OtelProjesi.csproj   → Proje dosyası

└── OtelRezervasyon.sql  → Veritabanı kurulum scripti
