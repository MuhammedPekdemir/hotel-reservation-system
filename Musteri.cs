using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane
{
    internal class Musteri
    {

        public int musteriId;
        public string ad;
        public string soyAd;
        public string telNo;

        public Musteri(int _musteriId, string _ad, string _soyAd, string _telNo)
        {
            musteriId = _musteriId;
            ad = _ad;
            soyAd = _soyAd;
            telNo = _telNo;
        }

        public void MusterileriGoster()
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            Console.WriteLine($"Musteri Id: {musteriId}\nMusteri Ad: {ad}\nMusteri Soyad: {soyAd}\nTelefon Numarası: {telNo}");

            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        }

    }
}