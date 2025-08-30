//using StarLab.Domain;

//namespace StarLba.Domain
//{
//    public class SpectralTypeTests
//    {
//        public static string[][] SpectralTypeCases =
//        {
//            ["k-m", "" ,"", "k-m"],
//            //["(G3w)F7", "(G3w)F7", "", ""],
//            ["A/F", "A/F", "", ""],
//            //["A/Fe-K", "A", "", "/Fe-K"],
//            ["A0V", "A0", "V", ""],
//            ["B1Vnne", "B1", "V", "nne"],
//            ["B1Vnne...", "B1", "V", "nne..."],
//            ["B2.5Ib", "B2.5", "Ib", ""],
//            ["B2.5IV-V", "B2.5", "IV-V", ""],
//            ["B2/3IV:n:", "B2/3", "IV", ":n:"],
//            ["B2/B3II/III", "B2/B3", "II/III", ""],
//            ["B2:IIIpshev", "B2", "", ":IIIpshev"],
//            ["B2V + B2V", "B2", "V", "+ B2V"],
//            ["B8 (SrCr)Hg:", "B8", "", "(SrCr)Hg:"],
//            ["C", "C", "", ""],
//            ["C(R)e", "C", "", "(R)e"],
//            ["C0", "C0", "", ""],
//            ["C0,0 (F8pe)", "C0,0", "", "(F8pe)"],
//            ["C3.2", "C3.2", "", ""],
//            ["C3II", "C3", "II", ""],
//            ["C5,4(N3)", "C5,4", "", "(N3)"],
//            ["C7Iab", "C7", "Iab", ""],
//            ["CII...", "C", "II", "..."],
//            ["CVIIe+", "C", "VII", "e+"],
//            ["DA", "DA", "", ""],
//            ["DA1", "DA1", "", ""],
//            ["DA2.5", "DA2.5", "", ""],
//            ["DA13", "DA13", "", ""],
//            ["DAwe...", "DA", "", "we..."],
//            ["DB:p", "DB", "", ":p"],
//            ["DC:", "DC", "", ":"],
//            ["DAB", "DAB", "", ""],
//            ["DAO", "DAO", "", ""],
//            ["DAZ", "DAZ", "", ""],
//            ["DBZ", "DBZ", "", ""],
//            ["F0/F2III/IV", "F0/F2", "III/IV", ""],
//            ["F0p (CrEu)", "F0", "", "p (CrEu)"],
//            ["F0V+F/G", "F0", "V", "+F/G"],
//            ["F8Ve-K1Ve(T)", "F8", "V", "e-K1Ve(T)"],
//            ["G(5)V", "G(5)", "V", ""],
//            ["G(wp)", "G", "", "(wp)"],
//            ["K1+V", "K1+", "V", ""],
//            ["K0IV/V+...", "K0", "IV/V+", "..."],
//            ["K0IV/V + G2III", "K0", "IV/V", "+ G2III"],
//            ["L2", "L2", "", ""],
//            ["M0Iab-Ib SB", "M0", "Iab-Ib", "SB"],
//            ["M1/2II/III+A", "M1/2", "II/III", "+A"],
//            ["N3v", "N3", "", "v"],
//            ["Of?p", "O", "", "f?p"],
//            ["O(8)fe", "O(8)", "", "fe"],
//            ["O5.5((f))", "O5.5", "", "((f))"],
//            ["O5/6(e)", "O5/6", "", "(e)"],
//            ["O5/O6", "O5/O6", "", ""],
//            ["R4", "R4", "", ""],
//            ["R6pv", "R6", "", "pv"],
//            ["sdO", "O", "sd", ""],
//            ["sdB2", "B2", "sd", ""],
//            ["sdA3:", "A3", "sd", ":"],
//            ["sdF5:", "F5", "sd", ":"],
//            ["sdG6::", "G6", "sd", "::"],
//            ["sdK7:p", "K7", "sd", ":p"],
//            ["sdM9", "M9", "sd", ""],
//            ["sdB(Nova)", "B", "sd", "(Nova)"],
//            ["S1,5ev", "S1", "", ",5ev"],
//            ["S5.3 SB", "S5.3", "", "SB"],
//            ["T4", "T4", "", ""],
//            ["Y0", "Y0", "", ""],
//            ["WC", "WC", "", ""],
//            ["WC+...", "WC", "", "+..."],
//            ["WC4-N6", "WC4", "", "-N6"],
//            ["WC4 + O5", "WC4", "", "+ O5"],
//            ["WC4 + M3III", "WC4", "", "+ M3III"],
//            ["WN4 (SB1)", "WN4", "", "(SB1)"],
//            ["WN7 + A(SB1)", "WN7", "", "+ A(SB1)"],
//            ["WR", "WR", "", ""]
//        };


//        [TestCaseSource(nameof(SpectralTypeCases))]
//        public void TestStringStringStringConstructor(string spectralType, string spectralClass, string magnitudeClass, string peculiarities)
//        {
//            var s = new SpectralType(spectralClass, magnitudeClass, peculiarities);

//            Assert.That(s, Is.Not.Null);

//            Assert.That(s.SpectralClass, Is.EqualTo(spectralClass));
//            Assert.That(s.MagnitudeClass, Is.EqualTo(magnitudeClass));
//            Assert.That(s.Peculiarities, Is.EqualTo(peculiarities));
//            Assert.That(s.ToString(), Is.EqualTo(spectralType));
//        }

//        [TestCaseSource(nameof(SpectralTypeCases))]
//        public void TestStringConstructor(string spectralType, string spectralClass, string magnitudeClass, string peculiarities)
//        {
//            var s = new SpectralType(spectralType);

//            Assert.That(s, Is.Not.Null);

//            Assert.That(s.SpectralClass, Is.EqualTo(spectralClass));
//            Assert.That(s.MagnitudeClass, Is.EqualTo(magnitudeClass));
//            Assert.That(s.Peculiarities, Is.EqualTo(peculiarities));
//            Assert.That(s.ToString(), Is.EqualTo(spectralType));
//        }
//    }
//}
