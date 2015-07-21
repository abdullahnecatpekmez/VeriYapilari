using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calisma_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            int secim = 0;
            while (secim != 3)
            {
                secim = menu();
                switch (secim)
                {
                    case 1:
                        Console.Write("\n\tDairesel bağlı listeler icin n değerini giriniz ( atlama sayısı ):");
                        n = Convert.ToInt32(Console.ReadLine());
                        while (n < 1)
                        {
                            Console.Write("\n\tn değerini 0'dan büyük giriniz ( atlama sayısı ):");
                            n = Convert.ToInt32(Console.ReadLine());
                        }
                        prg(n, secim);
                        break;
                    case 2:
                        Console.Write("\n\tDairesel bağlı listeler için n değerini giriniz ( atlama sayısı ):");
                        n = Convert.ToInt32(Console.ReadLine());
                        while (n < 1)
                        {
                            Console.Write("\n\tn değerini 0'dan büyük giriniz ( atlama sayısı ):");
                            n = Convert.ToInt32(Console.ReadLine());
                        }

                        DateTime ilk_zaman = new DateTime();
                        ilk_zaman = DateTime.Now;

                        DateTime son_zaman = new DateTime();
                        son_zaman = DateTime.Now;

                        TimeSpan fark = son_zaman - ilk_zaman;  // fark adında zaman farkını hesaplayan nesne olusturuluyor.                      
                        int sayac = 0;
                        while (fark.Seconds < 3)
                        {
                            prg(n, secim);
                            son_zaman = DateTime.Now;
                            fark = son_zaman - ilk_zaman;
                            sayac++;
                        }
                        çizgi();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("\tYapılan deney sonucunda bilgisayarınız 3 sn. de " + sayac + " kere çözmüştür...");
                        Console.ResetColor();
                        çizgi(); break;
                }
            }
            Console.ReadKey();
        }
        public static void prg(int n, int secim)
        {
            int kat;
            string str;
            string[] arabalar = { "Beyaz", "Kirmizi", "Siyah", "Mavi", "Metalik", "Yesil", "Pembe", "Turuncu", "Turkuaz", "Eflatun" };
            Random rnd = new Random();
            ArrayList liste = new ArrayList(9);
            Stack[] y = new Stack[3];
            Queue[] k = new Queue[3];
            Bliste[] l = new Bliste[3];
            int ys = 0, ks = 0, ls = 0;

            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                {
                    y[ys] = new Stack(10);
                    for (int j = 0; j < 10; j++)
                        y[ys].Push(arabalar[j]);
                    liste.Add(y[ys]);
                    ys++;
                }
                if (i % 3 == 1)
                {
                    k[ks] = new Queue(10);
                    for (int j = 0; j < 10; j++)
                        k[ks].Enqueue(arabalar[j]);
                    liste.Add(k[ks]);
                    ks++;
                }
                if (i % 3 == 2)
                {
                    l[ls] = new Bliste();
                    for (int j = 0; j < 10; j++)
                    {
                        l[ls].ekleme(arabalar[j]);
                        l[ls].n = n;
                    }
                    liste.Add(l[ls]);
                    ls++;
                }
            }
            int tur_say = 1;
            while (k[0].Count != 0 || k[1].Count != 0 || k[2].Count != 0 || y[0].Count != 0 || y[1].Count != 0 || y[2].Count != 0 || !l[0].bosmu() || !l[1].bosmu() || !l[2].bosmu())
            {
                kat = rnd.Next(0, 9);
                switch (kat % 3)
                {
                    case 0:
                        if (k[kat / 3].Count != 0)
                        {
                            str = k[kat / 3].Dequeue().ToString();
                            if (secim == 1)
                            {
                                Console.WriteLine(tur_say++ + ". Tur\nKat No:" + (kat + 1) + "\nCikan Arac :" + str);
                                Console.WriteLine("Kalanlar");
                                kuyruk_listele(k[kat / 3]);
                                Console.WriteLine("\n");
                            }
                        } break;
                    case 1:
                        if (y[(kat - 1) / 3].Count != 0)
                        {
                            str = y[(kat - 1) / 3].Pop().ToString();
                            if (secim == 1)
                            {
                                Console.WriteLine(tur_say++ + ". Tur\nKat No:" + (kat + 1) + "\nCikan Arac :" + str);
                                Console.WriteLine("Kalanlar");
                                yigin_listele(y[(kat - 1) / 3]);
                                Console.WriteLine("\n");
                            }
                        } break;
                    case 2:
                        if (!l[(kat - 2) / 3].bosmu())
                        {
                            str = l[(kat - 2) / 3].çıkar();
                            if (secim == 1)
                            {
                                Console.WriteLine(tur_say++ + ". Tur\nKat No:" + (kat + 1) + "\nCikan Arac :" + str);
                                Console.WriteLine("Kalanlar");
                                l[(kat - 2) / 3].listele();
                                Console.WriteLine("\n");
                            }
                        } break;
                }
            }
        }
        public static void çizgi()
        {
            string str = "";
            Console.Write(str.PadLeft(80, '-'));
        }
        public static int menu()
        {
            int sec;
            Console.WriteLine("\n***********************************SEÇENEKLER***********************************");
            Console.WriteLine("\t 1. Katlara yerleşen arabaları ve deney sonuçlarını gör... ");
            Console.WriteLine("\t 2. Bilgisayarınızın 3 sn. içinde bu problemi çözme sayısını bul... ");
            Console.WriteLine("\t 3. ÇIKIŞ...\n");
            Console.WriteLine("***********************************SEÇENEKLER***********************************\n");
            Console.Write("\tSeçiminizi giriniz: ");
            sec = Convert.ToInt32(Console.ReadLine());
            return sec;
        }
        public static void kuyruk_listele(Queue kuyruk)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Queue gecici = (Queue)kuyruk.Clone();

            if (gecici.Count != 0)
            {
                while (gecici.Count > 0)
                    Console.Write("--" + gecici.Dequeue().ToString());
            }
            else
                Console.WriteLine("Bu katta hicbir arac bulunmamaktadir!");
            Console.ResetColor(); // sıfırladık
        }
        public static void yigin_listele(Stack yigin)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Stack gecici = (Stack)yigin.Clone();

            if (yigin.Count != 0)
            {
                while (gecici.Count > 0)
                    Console.Write("--" + gecici.Pop().ToString());
            }
            else
                Console.WriteLine("Bu katta hicbir arac bulunmamaktadir!");
            Console.ResetColor(); // sıfırladık
        }
    }
    class dugum
    {
        public object veri;
        public dugum sonraki;
        public dugum(object o)
        {
            veri = o;
        }
        public void yazdir() // Düğümün verisini yazdırır
        {
            if (veri != null)
                Console.Write("--" + veri);
        }
    }
    class Bliste
    {
        public dugum bas = new dugum(null);
        public dugum son = new dugum(null);
        public int n = -1;
        public void ekleme(object o)
        {
            dugum y = new dugum(o);
            if (bas.veri == null)
            { y.sonraki = y; bas = y; }
            else
            {
                dugum onceki = bas;
                dugum gecici = bas.sonraki;
                while (gecici.veri != bas.veri)
                {
                    onceki = gecici;
                    gecici = gecici.sonraki;
                }
                onceki.sonraki = y;
                y.sonraki = bas;
                son = y;
            }
        }
        public bool bosmu()
        {
            return bas.veri == null;
        }
        public string çıkar()
        {
            dugum önceki = bas;
            dugum gecici = bas;
            string str;
            if (bas.sonraki == bas)
            {
                str = (string)bas.veri;
                bas.veri = null;
                return str;
            }
            else
            {
                if (n > 1)
                    for (int i = 0; i < n - 1; i++)
                    {
                        önceki = gecici;
                        gecici = gecici.sonraki;
                    }
                if (gecici.veri == bas.veri)
                {
                    bas = gecici.sonraki;
                    son.sonraki = bas;
                }
                else
                {
                    önceki.sonraki = gecici.sonraki;
                    son = önceki;
                    bas = önceki.sonraki;
                }
                return (string)gecici.veri;
            }
        }
        public void listele()
        { // Bağlı Listeyi Baştan Sona Listeler
            Console.ForegroundColor = ConsoleColor.Cyan;
            dugum etkin = bas;
            etkin.yazdir(); etkin = etkin.sonraki;
            while (etkin.veri != bas.veri)
            { etkin.yazdir(); etkin = etkin.sonraki; }
            if (bas.veri == null)
                Console.WriteLine("Bu katta baska hicbir arac kalmamistir!");
            Console.ResetColor(); // sıfırladık
        }
    }
}