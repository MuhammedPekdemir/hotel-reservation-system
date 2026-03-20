using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane
{
    internal class Rezervasyon
    {

        public int rezId;
        public Oda oda;
        public Musteri musteri;
        public DateTime girisTarih;
        public DateTime cikisTarih;

        public Rezervasyon(int _rezId, Oda _oda, Musteri _musteri, DateTime _girisTarih, DateTime _cikisTarih)
        {
            rezId = _rezId;
            oda = _oda;
            musteri = _musteri;
            girisTarih = _girisTarih;
            cikisTarih = _cikisTarih;
        }

        public double ToplamUcret()
        {
            int gun = (cikisTarih - girisTarih).Days;
            return gun * oda.gecelikFiyat;
        }

        public void RezBilgiGoster()
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            Console.WriteLine($"Rezervasyon Id: {rezId}\n" + $"Musteri Ad: {musteri.ad}\n" + $"Musteri Soyad: {musteri.soyAd}\n" + $"Oda Id : {oda.odaId}\n"
            + $"Giris Tarihi: {girisTarih.ToShortDateString()}\n" + $"Cikis Tarihi: {cikisTarih.ToShortDateString()}\n" + $"Toplam Ucret: {ToplamUcret()}₺");

            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        }
    }
}