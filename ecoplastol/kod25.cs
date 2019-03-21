using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecoplastol
{
    class kod25
    {
        private char[] arr = new char[24];
        private char[] arr2 = new char[24];
        private string _kategoria;
        private string _znak1;
        private string _znak2;
        private string _icc;
        private string _cc1;
        private string _cc2;
        private string _smin_p;
        private string _smin_w;
        private string _smax_p;
        private string _smax_w;
        private string _trn;
        private string _prn;
        private string _rez;
        private string _odch;
        private string _cz1;
        private string _cz2;
        private string _ke;

        private string _kod1;
        private string _kod2;

        public string kategoria { get { return _kategoria; } set { _kategoria = value; } }
        public string znak1 { get { return _znak1; } set { _znak1 = value; } }
        public string znak2 { get { return _znak2; } set { _znak2 = value; } }
        public string icc { get { return _icc; } set { _icc = value; } }
        public string cc1 { get { return _cc1; } set { _cc1 = value; } }
        public string cc2 { get { return _cc2; } set { _cc2 = value; } }
        public string smin_p { get { return _smin_p; } set { _smin_p = value; } }
        public string smin_w { get { return _smin_w; } set { _smin_w = value; } }
        public string smax_p { get { return _smax_p; } set { _smax_p = value; } }
        public string smax_w { get { return _smax_w; } set { _smax_w = value; } }
        public string trn { get { return _trn; } set { _trn = value; } }
        public string prn { get { return _prn; } set { _prn = value; } }
        public string rez { get { return _rez; } set { _rez = value; } }
        public string odch { get { return _odch; } set { _odch = value; } }
        public string cz1 { get { return _cz1; } set { _cz1 = value; } }
        public string cz2 { get { return _cz2; } set { _cz2 = value; } }
        public string ke { get { return _ke; } set { _ke = value; } }

        public string kod1 { get { return _kod1; } set { _kod1 = value; } }
        public string kod2 { get { return _kod2; } set { _kod2 = value; } }


        public kod25()
        {
            int licznik = 0;
            foreach (int i in arr)
            {
                arr[licznik] = Convert.ToChar("0");
                arr2[licznik] = Convert.ToChar("0");
                licznik++;
            }
        }

        public void GenerujKody()
        // buduje kod kreskowy 
        {
            // kategoria
            arr[0] = _kategoria[0];
            arr[1] = _kategoria[1];
            arr2[0] = _kategoria[0];
            arr2[1] = _kategoria[1];
            // znak1
            arr[2] = _znak1[0];
            arr[3] = _znak1[1];
            arr2[2] = _znak1[0];
            arr2[3] = _znak1[1];
            // znak2
            arr[4] = _znak2[0];
            arr[5] = _znak2[1];
            arr2[4] = _znak2[0];
            arr2[5] = _znak2[1];
            // icc i cc
            // Jeżeli jest indykacja czasu chłodzenia to na pozycji 7 podajemy symbol przypisany danej wartości
            // a na pozycji 8 symbol przypisany wartości określającej czas chłodzenia.
            // Jeżeli bez indykacji to na pozycji 7 stawiamy cyfre 2 a na pozycji 8 cyfre 1.
            if (_icc == "3")
            {
                arr[6] = Convert.ToChar("3");
                arr[7] = _cc1[0];
                arr2[6] = Convert.ToChar("3");
                arr2[7] = _cc2[0];
            }
            else
            {
                arr[6] = Convert.ToChar("2");
                arr[7] = Convert.ToChar("1");
                arr2[6] = Convert.ToChar("2");
                arr2[7] = Convert.ToChar("1");
            }
            // smin i smax
            // Jeżeli wyrób należy do kategorii która posiada jedną średnicę (SMIN = SMAX) to
            // na pozycjach, 9,10,11 wpisywana jest wartość średnicy w mm.
            // Jeżeli wyrób należy do kategorii posiadającej różne średnice MIN i MAX to
            // na pozycjach 9,10,11 wpisywana jest wartość wyliczana ze wzoru :
            // symbol SMAX * 31 + symbol SMIN;
            if (_kategoria == "93" || _kategoria == "94")
            {
                // różne średnice
                int _i_smax_p = Int32.Parse(_smax_p);
                int _i_smin_p = Int32.Parse(_smin_p);
                int _i_s = _i_smax_p * 31 + _i_smin_p;
                string result = String.Format("{0,3:000}", _i_s);
                arr[8] = result[0];
                arr[9] = result[1];
                arr[10] = result[2];
                arr2[8] = result[0];
                arr2[9] = result[1];
                arr2[10] = result[2];
            }
            else
            {
                // takie same średnice
                int _i_smin_w = Int32.Parse(_smin_w);
                string result = String.Format("{0,3:000}", _i_smin_w);
                arr[8] = result[0];
                arr[9] = result[1];
                arr[10] = result[2];
                arr2[8] = result[0];
                arr2[9] = result[1];
                arr2[10] = result[2];
            }

            // trn
            arr[11] = _trn[0];
            arr2[11] = _trn[0];

            // prn
            arr[12] = _prn[0];
            arr[13] = _prn[1];
            arr2[12] = _prn[0];
            arr2[13] = _prn[1];

            // rez
            arr[14] = _rez[0];
            arr[15] = _rez[1];
            arr[16] = _rez[2];
            arr2[14] = _rez[0];
            arr2[15] = _rez[1];
            arr2[16] = _rez[2];

            // odch
            arr[17] = _odch[0];
            arr2[17] = _odch[0];

            // cz
            arr[18] = _cz1[0];
            arr[19] = _cz1[1];
            arr[20] = _cz1[2];
            arr2[18] = _cz2[0];
            arr2[19] = _cz2[1];
            arr2[20] = _cz2[2];

            // ke
            arr[21] = _ke[0];
            arr[22] = _ke[1];
            arr2[21] = _ke[0];
            arr2[22] = _ke[1];

            arr[23] = GenerujCK();
            arr2[23] = GenerujCK2();

            // konweruje kod z arraya do stringów
            _kod1 = new string(arr);
            _kod2 = new string(arr2);
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
            while (_licznik < 23)
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

        private char GenerujCK2()
        {
            // założenie - wszystkie cyfry kody trzeba ponumerować od 1 do 23
            // 1 - zsumować wartości z pozycji nieparzystych 1, 3, 5, 7 ...
            // 2 - zsumować wartości z pozycji parzystych 2, 4, 6, 8 ...
            int _suma_parz = 0;
            int _suma_nieparz = 0;
            int _wartosc;
            int _licznik = 0;
            while (_licznik < 23)
            {
                _wartosc = Int32.Parse(arr2[_licznik].ToString());
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
