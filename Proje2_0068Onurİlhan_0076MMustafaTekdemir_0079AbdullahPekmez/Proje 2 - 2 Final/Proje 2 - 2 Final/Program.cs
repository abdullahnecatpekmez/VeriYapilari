
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calisma_1
{
    class Kuyruk
    {
        private int kapasite;
        private Musteri[] m;
        private int ön;
        private int arka;
        private int kişi;
        public Kuyruk(int s)
        {
            kapasite = s;
            m = new Musteri[kapasite];
            ön = 0;
            arka = -1;
            kişi = 0;
        }
        public void ekle(Musteri j)
        {
            if (arka == kapasite - 1)
                arka = -1;
            m[++arka] = j;
            kişi++;
        }
        public Musteri sil()
        {
            Musteri temp = m[ön++];
            if (ön == kapasite)
                ön = 0;
            kişi--;
            return temp;
        }
        public int sayı()
        {
            return kişi;
        }
    }
    class Musteri
    {
        public int no;
        public int sure;
    }
    class OncelikKuyruk
    {
        public ArrayList liste;
        public OncelikKuyruk()
        {
            liste = new ArrayList();
        }
        public void ekle(Musteri eklenen)  // ekle metodu
        {
            if (Bosmu())
                liste.Add(eklenen);
            else
            {
                for (int i = 0; i < liste.Count; i++)
                {
                    if (eklenen.sure > ((Musteri)liste[liste.Count - 1]).sure)
                        liste.Add(eklenen);
                    if (eklenen.sure < ((Musteri)liste[i]).sure)
                    {
                        liste.Insert(i, eklenen);
                        break;
                    }
                }
            }
        }
        public Musteri sil()  // sil metodu
        {
            Musteri m;
            if (!Bosmu())
            {
                m = (Musteri)liste[0];
                liste.RemoveAt(0);
                return m;
            }
            else
                return null;
        }
        public bool Bosmu()
        {
            return (liste.Count == 0);
        }
        public int eleman_say()
        {
            return liste.Count;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int N, secim, top_bek1, top_bek2;
            Random rnd = new Random();

            secim = 0;
            while (secim != 3)
            {
                secim = menu();
                switch (secim)
                {
                    case 1:
                        Console.Write("\tLütfen N değerini giriniz:");
                        N = Convert.ToInt32(Console.ReadLine());

                        Kuyruk Q = new Kuyruk(N);
                        OncelikKuyruk oncelik_k = new OncelikKuyruk();

                        for (int i = 0; i < N; i++)
                        {
                            Musteri m1 = new Musteri();
                            m1.no = i;
                            m1.sure = rnd.Next(10, 600);
                            Q.ekle(m1);
                            oncelik_k.ekle(m1);
                        }
                        top_bek1 = fifo_bilgi_goster(Q, 1);
                        top_bek2 = oncelik_bilgi_goster(oncelik_k, 1);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\nFIFO Kuyrugundaki ORTALAMA bekleme suresi : " + (top_bek1 / N) / 60 + " dk " + (top_bek1 / N) % 60 + " sn ");
                        Console.WriteLine("\nONCELIK Kuyrugundaki ORTALAMA bekleme suresi : " + (top_bek2 / N) / 60 + " dk " + (top_bek2 / N) % 60 + " sn ");
                        Console.WriteLine("\n Fark  Suresi: " + ((top_bek1 - top_bek2) / 60) + " dk " + ((top_bek1 - top_bek2) % 60) + " sn "); //genel süre farkı hesaplaniyor.
                        Console.WriteLine("\n Fark Oranı: % " + (float)(100 * (top_bek1 - top_bek2)) / top_bek1); //fark oranı hesaplaniyor
                        Console.ResetColor(); // sıfırladık
                        break;

                    case 2:
                        Console.Write("\tLutfen N değerini giriniz:");
                        N = Convert.ToInt32(Console.ReadLine());

                        OncelikKuyruk oncelik_k1 = new OncelikKuyruk();
                        OncelikKuyruk oncelik_k2 = new OncelikKuyruk();
                        OncelikKuyruk oncelik_k3 = new OncelikKuyruk();

                        Kuyruk Q1 = new Kuyruk(N);
                        Kuyruk Q2 = new Kuyruk(N);
                        Kuyruk Q3 = new Kuyruk(N);

                        for (int i = 0; i < N; i++)
                        {
                            Musteri m = new Musteri();
                            m.no = i;
                            m.sure = rnd.Next(10, 600);
                            if (oncelik_k1.eleman_say() < oncelik_k2.eleman_say() && oncelik_k1.eleman_say() < oncelik_k3.eleman_say())
                            {
                                oncelik_k1.ekle(m);
                                Q1.ekle(m);
                            }
                            else if (oncelik_k2.eleman_say() < oncelik_k3.eleman_say())
                            {
                                oncelik_k2.ekle(m);
                                Q2.ekle(m);
                            }
                            else
                            {
                                oncelik_k3.ekle(m);
                                Q3.ekle(m);
                            }
                        }

                        int[] top_islm_sure = new int[6];
                        top_islm_sure[0] = fifo_bilgi_goster(Q1, 1);
                        top_islm_sure[1] = fifo_bilgi_goster(Q2, 2);
                        top_islm_sure[2] = fifo_bilgi_goster(Q3, 3);
                        top_islm_sure[3] = oncelik_bilgi_goster(oncelik_k1, 1);
                        top_islm_sure[4] = oncelik_bilgi_goster(oncelik_k2, 2);
                        top_islm_sure[5] = oncelik_bilgi_goster(oncelik_k3, 3);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-----GENEL SONUCLAR-----");
                        Console.WriteLine("--FIFO--   -- ONCELIK--   --FARK(sn)--  -- FARK ORANI( % )--");

                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine("  " + top_islm_sure[i] + "\t\t " + top_islm_sure[i + 3] + " \t\t" + (top_islm_sure[i] - top_islm_sure[i + 3]) + "\t\t" + (float)(100 * ((top_islm_sure[i] - top_islm_sure[i + 3]))) / top_islm_sure[i]);
                        }
                        Console.ResetColor(); // sıfırladık
                        break;

                }
            }

            Console.ReadKey();
        }

        public static int oncelik_bilgi_goster(OncelikKuyruk k, int k_no)
        {
            Console.WriteLine("\n\n\n\t\t{0,2}. ÖNCELİK KUYRUKTAKİ gerçekleşen işlemler...", k_no);
            çizgi();
            int n = k.eleman_say(), t = 0, gen_top = 0;
            for (int i = 0; i < n; i++)
            {
                int s;
                Musteri cikan = new Musteri();
                cikan = k.sil();
                s = cikan.sure;
                t += s;
                gen_top += t;
                Console.WriteLine("{0,2}. Müşteri KUYRUKTA kendi işlemi dahil {1,4} sn. bekledi.(kendi işl. {2,4} sn.)", cikan.no + 1, t, s);
                çizgi();
            }
            Console.WriteLine("\n\t{0,2} müşterinin toplam bekleme süresi: {1,4} dk. {2,2} sn. dir.", n, gen_top / 60, gen_top % 60);
            Console.WriteLine("\n\tOrtalama bekleme süresi: {0,4} dk. {1,2} sn. dir.", (gen_top / n) / 60, (gen_top / n) % 60);
            return gen_top;
        }

        public static int fifo_bilgi_goster(Kuyruk k, int k_no)
        {
            Console.WriteLine("\n\n\n\t\t\t{0,2}. KUYRUKTAKİ gerçekleşen işlemler...", k_no);
            çizgi();
            int N = k.sayı(), top1 = 0, gen_top = 0;
            for (int i = 0; i < N; i++)
            {
                int s;
                Musteri cikan1 = new Musteri();
                cikan1 = k.sil();
                s = cikan1.sure;
                top1 += s;
                gen_top += top1;
                Console.WriteLine("{0,2}. Müşteri KUYRUKTA kendi işlemi dahil {1,4} sn. bekledi.(kendi işl. {2,4} sn.)", cikan1.no + 1, top1, s);
                çizgi();
            }
            Console.WriteLine("\n\t{0,2} müşterinin toplam bekleme süresi: {1,4} dk. {2,2} sn. dir.", N, gen_top / 60, gen_top % 60);
            Console.WriteLine("\n\tOrtalama bekleme süresi: {0,4} dk. {1,2} sn. dir.", (gen_top / N) / 60, (gen_top / N) % 60);
            return gen_top;
        }

        public static int menu()
        {
            int sec;
            Console.WriteLine("\n***********************************SEÇENEKLER***********************************");
            Console.WriteLine("\t1. N kişi için FIFO ve Öncelik Kuyruğu arasındaki farkı gör.");
            Console.WriteLine("\t2. N kişi için 3'er adet FIFO ve Öncelik Kuyruğu arasındaki farkı gör.");
            Console.WriteLine("\t3. ÇIKIŞ...\n");
            Console.WriteLine("***********************************SEÇENEKLER***********************************\n");
            Console.Write("\tSeçiminizi giriniz:");
            sec = Convert.ToInt32(Console.ReadLine());
            return sec;
        }
        public static void çizgi()
        {
            string str = "";
            Console.Write(str.PadLeft(80, '-'));
        }
    }
}
