using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Yonetici yonetici = new Yonetici("Admin", "1234");

            Console.WriteLine("=== OTEL YÖNETİM SİSTEMİ ===");
            Console.Write("Yönetici Şifresi: ");
            string sifre = Console.ReadLine();

            if (!yonetici.SifreDogrula(sifre))
            {
                Console.WriteLine("Yanlış şifre! Çıkılıyor...");
                return;
            }

            Console.WriteLine($"Hoş geldiniz, {yonetici.Ad}!");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== OTEL YÖNETİM PANELİ ===");
                Console.WriteLine("1. Tüm Odaları Listele");
                Console.WriteLine("2. Müsait Odaları Listele");
                Console.WriteLine("3. Dolu Odaları Listele");
                Console.WriteLine("4. Oda Ekle");
                Console.WriteLine("5. Oda Sil");
                Console.WriteLine("6. Müşteri Ekle");
                Console.WriteLine("7. Müşterileri Listele");
                Console.WriteLine("8. Rezervasyon Al");
                Console.WriteLine("9. Rezervasyon İptal Et");
                Console.WriteLine("10. Tüm Rezervasyonları Listele");
                Console.WriteLine("0. Çıkış");
                Console.Write("\nSeçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1": yonetici.TumOdalariGoster(); break;
                    case "2": yonetici.MusaitOdalariGoster(); break;
                    case "3": yonetici.DoluOdalariGoster(); break;
                    case "4": yonetici.OdaEkle(); break;
                    case "5": yonetici.OdaSil(); break;
                    case "6": yonetici.MusteriEkle(); break;
                    case "7": yonetici.TumMusterileriGoster(); break;
                    case "8": yonetici.rezAl(); break;
                    case "9": yonetici.rezIptal(); break;
                    case "10": yonetici.TumRezervasyonlariGoster(); break;
                    case "0": return;
                    default: Console.WriteLine("Geçersiz seçim!"); break;
                }

                Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                Console.ReadKey();
            }


        }
    }
}