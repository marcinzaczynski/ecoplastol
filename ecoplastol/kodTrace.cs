using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class kodTrace
    {
        private char[] arr = new char[26];

        private string _znak1;
        private string _znak2;
        private string _kategoria;
        private string _smin_p;
        private string _smin_w;
        private string _smax_p;
        private string _smax_w;
        private string _partia;
        private string _producent;
        private string _sdr;
        private string _pem;
        private string _material;
        private string _peo;
        private string _mfr;

        private string _kod;

        public string znak1 { get { return _znak1; } set { _znak1 = value; } }
        public string znak2 { get { return _znak2; } set { _znak2 = value; } }
        public string kategoria { get { return _kategoria; } set { _kategoria = value; } }
        public string smin_p { get { return _smin_p; } set { _smin_p = value; } }
        public string smin_w { get { return _smin_w; } set { _smin_w = value; } }
        public string smax_p { get { return _smax_p; } set { _smax_p = value; } }
        public string smax_w { get { return _smax_w; } set { _smax_w = value; } }
        public string partia { get { return _partia; } set { _partia = value; } }
        public string producent { get { return _producent; } set { _producent = value; } }
        public string sdr { get { return _sdr; } set { _sdr = value; } }
        public string pem { get { return _pem; } set { _pem = value; } }
        public string material { get { return _material; } set { _material = value; } }
        public string peo { get { return _peo; } set { _peo = value; } }
        public string mfr { get { return _mfr; } set { _mfr = value; } }

        public string kod { get { return _kod; } set { _kod = value; } }

        public kodTrace()
        {
            int licznik = 0;
            foreach (int i in arr)
            {
                arr[licznik] = Convert.ToChar("0");
                licznik++;
            }
        }

        public void GenerujKod()
        // buduje kod kreskowy
        {
            // znak1
            arr[0] = _znak1[0];
            arr[1] = _znak1[1];

            // znak2
            // do wartości pierwszej pozycji znaku2 dodaję 3 - oznacza to że kod zawsze będzie miał cyfrę kontrolną
            var _i_znak2_poz1 = Int32.Parse(_znak2[0].ToString()) + 3;
            arr[2] = _i_znak2_poz1.ToString()[0];
            arr[3] = _znak2[1];

            // kategoria
            arr[4] = _kategoria[0];
            arr[5] = _kategoria[1];

            // smin & smax
            int _i_smax_p = Int32.Parse(_smax_p);
            int _i_smin_p = Int32.Parse(_smin_p);
            string result;

            if (_i_smin_p == _i_smax_p)
            // takie same średnice
            {
                // do wartości pierwszej pozycji znaku1 dodaję 3 - oznacza to że średnice są takie same
                var _i_znak1_poz1 = Int32.Parse(_znak1[0].ToString()) + 3;
                arr[0] = _i_znak1_poz1.ToString()[0];

                // teraz podaję średnicę wprost, na odpowiednich miejscach
                int _i_smin_w = Int32.Parse(_smin_w);
                result = String.Format("{0,3:000}", _i_smin_w);
                arr[6] = result[0];
                arr[7] = result[1];
                arr[8] = result[2];
            }
            else
            // różne średnice
            {
                int _i_s = _i_smax_p * 31 + _i_smin_p;
                result = String.Format("{0,3:000}", _i_s);
                arr[6] = result[0];
                arr[7] = result[1];
                arr[8] = result[2];

                // jakby co to ustawiam znak1 od nowa
                arr[0] = _znak1[0];
                arr[1] = _znak1[1];
            }

            // partia
            arr[9] = _partia[0];
            arr[10] = _partia[1];
            arr[11] = _partia[2];
            arr[12] = _partia[3];
            arr[13] = _partia[4];
            arr[14] = _partia[5];

            // producent
            int _i_producent = Int32.Parse(_producent);
            result = String.Format("{0,2:00}", _i_producent);
            arr[15] = result[0];
            arr[16] = result[1];

            // sdr
            arr[17] = _sdr[0];

            // pem
            arr[18] = _pem[0];
            arr[19] = _pem[1];
            arr[20] = _pem[2];
            arr[21] = _pem[3];

            // material
            arr[22] = _material[0];

            // peo
            arr[23] = _peo[0];

            // mfr
            arr[24] = _mfr[0];

            arr[25] = GenerujCK();

            // konweruje kod z arraya do stringów
            _kod = new string(arr);
        }

        private char GenerujCK()
        {
            // założenie - wszystkie cyfry kody trzeba ponumerować od 1 do 24
            // 1 - zsumować wartości z pozycji nieparzystych 1, 3, 5, 7 ...
            // 2 - zsumować wartości z pozycji parzystych 2, 4, 6, 8 ...

            int _suma_parz = 0;
            int _suma_nieparz = 0;
            int _wartosc = 0;
            int _licznik = 0;
            while (_licznik < 25)
            {
                _wartosc = Int32.Parse(arr[_licznik].ToString());
                // !!!!  nieparzyste będą miały wynik modulo 2 = 0 bo array numerowany jest od 0 
                if ((_licznik % 2) == 0)
                {
                    _suma_nieparz = _suma_nieparz + _wartosc;
                }
                // parzyste
                if ((_licznik % 2) == 1)
                {
                    _suma_parz = _suma_parz + _wartosc;
                }

                _licznik++;
            }

            int _wynik = 10 - ((_suma_nieparz * 3 + _suma_parz) % 10);
            if (_wynik == 10) { _wynik = 0; }

            string _result = String.Format("{0,1:0}", _wynik);

            return _result[0];
        }
    }
}
