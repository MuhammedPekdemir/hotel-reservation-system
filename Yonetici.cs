using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlDataReader = Microsoft.Data.SqlClient.SqlDataReader;

namespace Kutuphane
{
    internal class Yonetici
    {
        public string Ad;
        private string Sifre;

        private string connectionString = "Server=MUHPC\\SQLEXPRESS;Database=OtelDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public Yonetici(string ad, string sifre)
        {
            Ad = ad;
            Sifre = sifre;
        }

        public bool SifreDogrula(string girilen)
        {
            return Sifre == girilen;
        }

        public void OdaEkle()
        {
            Console.Write("Oda Id: ");
            int odaId = int.Parse(Console.ReadLine());
            Console.Write("Tip (Tek, Çift, Suit): ");
            string tip = Console.ReadLine();
            Console.Write("Gecelik Fiyat: ");
            double fiyat = double.Parse(Console.ReadLine());

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Odalar (OdaNo, Tip, GecelikFiyat) VALUES (@OdaNo, @Tip, @GecelikFiyat)", conn);
            cmd.Parameters.AddWithValue("@OdaNo", odaId);
            cmd.Parameters.AddWithValue("@Tip", tip);
            cmd.Parameters.AddWithValue("@GecelikFiyat", fiyat);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Oda başarıyla eklendi!");
        }

        public void OdaSil()
        {
            Console.Write("Silinecek Oda Id: ");
            int silinecekOdaId = int.Parse(Console.ReadLine());

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Odalar WHERE OdaNo = @OdaNo", conn);
            cmd.Parameters.AddWithValue("@OdaNo", silinecekOdaId);
            int etkilenen = cmd.ExecuteNonQuery();

            if (etkilenen > 0) Console.WriteLine("Oda silindi!");
            else Console.WriteLine("Oda bulunamadı!");
        }

        public void TumOdalariGoster()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT OdaNo, Tip, GecelikFiyat, MüsaitMi FROM Odalar", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- TÜM ODALAR ---");
            bool varMi = false;
            while (reader.Read())
            {
                string durum = (bool)reader["MüsaitMi"] ? "Müsait" : "Dolu";
                Console.WriteLine($"Oda No: {reader["OdaNo"]}  Tip: {reader["Tip"]}  Fiyat: {reader["GecelikFiyat"]}₺  Durum: {durum}");
                varMi = true;
            }
            if (!varMi) Console.WriteLine("Hiç oda yok!");
        }

        public void MusaitOdalariGoster()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("EXEC OdaMusaitMi", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- MÜSAİT ODALAR ---");
            bool varMi = false;
            while (reader.Read())
            {
                Console.WriteLine($"Oda No: {reader["OdaNo"]}  Tip: {reader["Tip"]}  Fiyat: {reader["GecelikFiyat"]}₺");
                varMi = true;
            }
            if (!varMi) Console.WriteLine("Müsait oda yok!");
        }

        public void DoluOdalariGoster()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("EXEC proc_DoluOdalariGetir", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- DOLU ODALAR ---");
            bool varMi = false;
            while (reader.Read())
            {
                Console.WriteLine($"Oda No: {reader["OdaNo"]}  Tip: {reader["Tip"]}  Fiyat: {reader["GecelikFiyat"]}₺");
                varMi = true;
            }
            if (!varMi) Console.WriteLine("Dolu oda yok!");
        }

        public void MusteriEkle()
        {
            Console.Write("Ad: ");
            string ad = Console.ReadLine();
            Console.Write("Soyad: ");
            string soyAd = Console.ReadLine();
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Musteriler (Ad, Soyad, Telefon) VALUES (@Ad, @Soyad, @Telefon)", conn);
            cmd.Parameters.AddWithValue("@Ad", ad);
            cmd.Parameters.AddWithValue("@Soyad", soyAd);
            cmd.Parameters.AddWithValue("@Telefon", telefon);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Müşteri başarıyla eklendi!");
        }

        public void TumMusterileriGoster()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT MusteriID, Ad, Soyad, Telefon FROM Musteriler", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- MÜŞTERİLER ---");
            bool varMi = false;
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["MusteriID"]}  Ad: {reader["Ad"]}  Soyad: {reader["Soyad"]}  Telefon: {reader["Telefon"]}");
                varMi = true;
            }
            if (!varMi) Console.WriteLine("Hiç müşteri yok!");
        }

        public Musteri musteri;
        public void rezAl()
        {
            MusaitOdalariGoster();

            Console.Write("\nOda No Giriniz: ");
            int odaNo = int.Parse(Console.ReadLine());

            TumMusterileriGoster();
            Console.Write("\nMusteri ID Giriniz: ");
            int musteriId = int.Parse(Console.ReadLine());

            Console.Write("Giris tarihi (gg.aa.yyyy): ");
            DateTime girisTarih = DateTime.Parse(Console.ReadLine());
            Console.Write("Cikis tarihi (gg.aa.yyyy): ");
            DateTime cikisTarih = DateTime.Parse(Console.ReadLine());

            if (cikisTarih <= girisTarih)
            {
                Console.WriteLine("Hatalı tarih girişi!");
                return;
            }

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand stokCmd = new SqlCommand("SELECT MüsaitMi FROM Odalar WHERE OdaNo = @OdaNo", conn);
            stokCmd.Parameters.AddWithValue("@OdaNo", odaNo);
            bool musait = (bool)stokCmd.ExecuteScalar();
            if (!musait) { Console.WriteLine("Bu oda müsait değil!"); return; }

            SqlCommand fiyatCmd = new SqlCommand("SELECT GecelikFiyat FROM Odalar WHERE OdaNo = @OdaNo", conn);
            fiyatCmd.Parameters.AddWithValue("@OdaNo", odaNo);
            double fiyat = Convert.ToDouble(fiyatCmd.ExecuteScalar());
            int gun = (cikisTarih - girisTarih).Days;
            double toplamUcret = gun * fiyat;

            SqlCommand cmd = new SqlCommand("INSERT INTO Rezervasyonlar (OdaNo, MusteriID, GirisTarih, CikisTarih, ToplamUcret) VALUES (@OdaNo, @MusteriID, @GirisTarih, @CikisTarih, @ToplamUcret)", conn);
            cmd.Parameters.AddWithValue("@OdaNo", odaNo);
            cmd.Parameters.AddWithValue("@MusteriID", musteriId);
            cmd.Parameters.AddWithValue("@GirisTarih", girisTarih);
            cmd.Parameters.AddWithValue("@CikisTarih", cikisTarih);
            cmd.Parameters.AddWithValue("@ToplamUcret", toplamUcret);
            cmd.ExecuteNonQuery();

            Console.WriteLine($"\nRezervasyon oluşturuldu! Toplam Ücret: {toplamUcret}₺");
        }

        public void rezIptal()
        {
            TumRezervasyonlariGoster();

            Console.Write("\nİptal edilecek Rezervasyon ID: ");
            int id = int.Parse(Console.ReadLine());

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Rezervasyonlar WHERE RezervasyonID = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", id);
            int etkilenen = cmd.ExecuteNonQuery();

            if (etkilenen > 0) Console.WriteLine("Rezervasyon iptal edildi!");
            else Console.WriteLine("Rezervasyon bulunamadı!");
        }

        public void TumRezervasyonlariGoster()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"
                SELECT r.RezervasyonID, m.Ad + ' ' + m.Soyad AS Musteri,
                       o.OdaNo, o.Tip, r.GirisTarih, r.CikisTarih, r.ToplamUcret
                FROM Rezervasyonlar r
                JOIN Odalar o ON r.OdaNo = o.OdaNo
                JOIN Musteriler m ON r.MusteriID = m.MusteriID", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- TÜM REZERVASYONLAR ---");
            bool varMi = false;
            while (reader.Read())
            {
                Console.WriteLine($"Rez ID: {reader["RezervasyonID"]}  Müşteri: {reader["Musteri"]}  Oda: {reader["OdaNo"]} ({reader["Tip"]})  Giriş: {reader["GirisTarih"]}  Çıkış: {reader["CikisTarih"]}  Ücret: {reader["ToplamUcret"]}₺");
                varMi = true;
            }
            if (!varMi) Console.WriteLine("Hiç rezervasyon yok!");
        }
    }
}