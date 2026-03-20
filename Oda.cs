using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane
{
    internal class Oda
    {

        public int odaId;
        public string odaTipi;
        public double gecelikFiyat;
        public bool musaitMi;

        public Oda(int _odaId, string _odaTipi, double _gecelikFiyat)
        {
            odaId = _odaId;
            odaTipi = _odaTipi;
            gecelikFiyat = _gecelikFiyat;
            musaitMi = true;
        }

        public void OdaBilgiGoster()
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            if (musaitMi == true)
            {
                Console.WriteLine($"Oda durumu: {musaitMi}\nOda Id: {odaId}\nOda Tipi: {odaTipi}\nGecelik Fiyat: {gecelikFiyat}");
            }
            else
            {
                Console.WriteLine($"Oda durumu: {musaitMi}\nOda Id: {odaId}\nOda Tipi: {odaTipi}\nGecelik Fiyat: {gecelikFiyat}");
            }

            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        }
    }
}