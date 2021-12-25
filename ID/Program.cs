// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists
using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Xml.Serialization;

public class REUTERS
{
    public string PLACES { get; set; }
    public string BODY { get; set; }
    public string TOPIC { get; set; }

}


public class RecursiveFileProcessor
{
    public static int westgermany = 0;
    public static int usa = 0;
    public static int france = 0;
    public static int uk = 0;
    public static int canada = 0;
    public static int japan = 0;
    public static int wszystkieK = 0;

    private static int i;

    public static void Main(string[] args)
    {
        foreach (string path in args)
        {
            if (File.Exists(path))
            {
                // This path is a file
                ProcessFile(path);
            }
            else if (Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }


        }


        Console.WriteLine("Ilosc artykulow z West-Germany " + westgermany);
        Console.WriteLine("Ilosc artykulow z USA " + usa);
        Console.WriteLine("Ilosc artykulow z France " + france);
        Console.WriteLine("Ilosc artykulow z UK " + uk);
        Console.WriteLine("Ilosc artykulow z Canada " + canada);
        Console.WriteLine("Ilosc artykulow z Japan " + japan);
        Console.WriteLine("Ilość artykułów razem:" + wszystkieK);
        Console.ReadLine();
    }

    // Process all files in the directory passed in, recurse on any directories
    // that are found, and process the files they contain.
    public static void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
            ProcessFile(fileName);

        // Recurse into subdirectories of this directory.
        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach (string subdirectory in subdirectoryEntries)
            ProcessDirectory(subdirectory);
    }

    // Insert logic for processing found files here.
    public static void ProcessFile(string path)
    {

        string text = File.ReadAllText(path);
        text = "<Articles>" + text + "</Articles>";
        text = text.Replace("<D>", "").Replace("</D>", " ").Replace("<!DOCTYPE lewis SYSTEM \"lewis.dtd\">", "");
        text = text.Replace("</TEXT>", "");
        text = Regex.Replace(text, "&#.*?;", string.Empty);
        text = Regex.Replace(text, "<TEXT.*?>", string.Empty);
        XmlSerializer serializer = new XmlSerializer(typeof(List<REUTERS>), new XmlRootAttribute("Articles"));
        StringReader stringReader = new StringReader(text);
        List<REUTERS> productList = (List<REUTERS>)serializer.Deserialize(stringReader);

        Console.WriteLine("Processed file '{0}'.", path);
        for (int i = 0; i < productList.Count; i++)
        {
            // Console.WriteLine(productList[i].BODY);


        }
        int allp = 0;

        for (int i = 0; i < productList.Count; i++)
        {
            if (productList[i].BODY != null)
            {
                int x = productList[i].BODY.Split(',').Length - 1;
                allp += x;

            }
        }
        Console.WriteLine(allp);

        //-----------------------------------------------------
        int pwestgermany = 0;
        int pusa = 0;
        int pfrance = 0;
        int puk = 0;
        int pcanada = 0;
        int pjapan = 0;

        //-----------------------------------------------------

        for (int i = 0; i < productList.Count; i++)
        {
            if (productList[i].BODY != null)
            {
                // Console.WriteLine(productList[i].BODY.Length);

                int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                allp += x;


                //Liczenie liter w danym tekście
                int numberOfLetters = productList[i].BODY.Count(c => char.IsLetter(c));
                Console.WriteLine("Ilość liter w danym tekście: +" + numberOfLetters);

                //Licczenie wyrazów w danym tekście
                string articles = productList[i].BODY;
                string[] words = productList[i].BODY.Split(' ');
                Console.WriteLine("Ilość wyrazów w danym tekście: " + words.Length);

                //Liczenie liczb w danym tekście
                int NumberOfNumbers = productList[i].BODY.Count(c => Char.IsNumber(c));
                Console.WriteLine("Ilość liczb w danym tekście: " + NumberOfNumbers);

                //Liczenie znaków specjalnych w danym tekście
                int NumberOfSpecialCharacters = articles.Count(c => !char.IsLetterOrDigit(c));
                Console.WriteLine("Ilość znaków specjalnych w danym tekście: " + NumberOfSpecialCharacters);

                //Liczenie wielkich liter w danym tekście
                int NumberOfUpperLetters = productList[i].BODY.Count(c => char.IsUpper(c));
                Console.WriteLine("Ilość wielkich liter w danym tekście: " + NumberOfUpperLetters);

                //Liczenie małych liter w danym tekście
                int NumberOfSmalLetters = productList[i].BODY.Count(c => char.IsLower(c));
                Console.WriteLine("Ilość małych liter w danym tekście: " + NumberOfSmalLetters);

                //Liczenie spacji w danym tekście
                int NumberOfSpaces = productList[i].BODY.Count(c => char.IsWhiteSpace(c));
                Console.WriteLine("Ilość spacji w danym tekście: " + NumberOfSpaces);

                //liczenie lini tekstu w artykułach
                int NumberOfLines = productList[i].BODY.Split('\n').Length;
                Console.WriteLine("Ilość Lini w danym tekście: " + NumberOfLines);
                Console.WriteLine(" ");





























            }
            if (String.Equals(productList[i].PLACES, "west-germany "))
            {
                westgermany++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {


                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pwestgermany += x;


                }
            }
            else if (String.Equals(productList[i].PLACES, "usa "))
            {
                wszystkieK++;
                usa++;
                if (productList[i].BODY != null)
                {
                    // Console.WriteLine(productList[i].BODY.Length);

                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pusa += x;
                    //Console.WriteLine(x);

                }
            }
            else if (String.Equals(productList[i].PLACES, "france "))
            {
                france++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {
                    // Console.WriteLine(productList[i].BODY.Length);

                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pfrance += x;
                    //Console.WriteLine(x);

                }

            }
            else if (String.Equals(productList[i].PLACES, "uk "))
            {
                uk++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {
                    // Console.WriteLine(productList[i].BODY.Length);

                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    puk += x;
                    //Console.WriteLine(x);

                }

            }
            else if (String.Equals(productList[i].PLACES, "canada "))
            {
                canada++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {
                    // Console.WriteLine(productList[i].BODY.Length);

                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pcanada += x;
                    //Console.WriteLine(x);

                }

            }
            else if (String.Equals(productList[i].PLACES, "japan "))
            {
                japan++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {
                    // Console.WriteLine(productList[i].BODY.Length);

                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pjapan += x;
                    //Console.WriteLine(x);

                }


                //knn algortym


                //Metryka Euklidesowa (Euclidean)
                static int M1(int x1, int x2, int y1, int y2)
                {
                    int square = (x1 - x2) * (x1 - x2) + (y1 = y2) * (y1 - y2);
                    return square;
                }
                //Metryka Uliczna (Manhattan)
                static int M2(int x1, int x2, int y1, int y2)
                {
                    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
                }
                //Metryka Czebyszewa (Chebyshev)
                static int M3(int x1, int x2, int y1, int y2)
                {
                    var dx = Math.Abs(x2 - x1);
                    var dy = Math.Abs(y2 - y1);
                    return (dx + dy) - Math.Min(dx, dy);

                }

                string sciezkaDoPliku = productList[i].BODY;
                float[,] westgermany;
                float[,] usa;
                float[,] france;
                float[,] uk;
                float[,] canada;
                float[,] japonia;
                double ktorametryka;
                int k;



                float[,] WczytajSystem(string sciezkaDoPliku)
                {
                    float[,] SystemDecyzyjny;
                    var linie = System.IO.File.ReadAllLines(sciezkaDoPliku);
                    int iloscKolumn = 0;
                    int iloscWierszy = 0;
                    var linia2 = linie[0].Trim();
                    var liczby2 = linia2.Split(' ');
                    iloscWierszy = linie.Length;
                    iloscKolumn = liczby2.Length;
                    SystemDecyzyjny = new float[iloscWierszy, iloscKolumn];
                    for (int i = 0; i < linie.Length; i++)
                    {
                        var linia = linie[1].Trim();
                        var liczby = linia.Split(' ');
                        for (int j = 0; j < liczby.Length; j++)
                        {
                            SystemDecyzyjny[i, j] = float.Parse(liczby[j].Trim());
                        }
                    }
                    return SystemDecyzyjny;
                }
                float[,] JakieDecyzje(float[,] systemDoWczytywania)
                {
                    var listadecyzji = new List<float>();
                    bool wystepuje = false;
                    for (int w = 0; i < systemDoWczytywania.GetLength(0); i++)
                    {
                        if (systemDoWczytywania[i, systemDoWczytywania.GetLength(1) - 1] == listadecyzji[w])
                        {
                            wystepuje = true;
                            w = listadecyzji.Count;
                        }
                    }
                    if (wystepuje == false)
                    {
                        listadecyzji.Add(systemDoWczytywania[i, systemDoWczytywania.GetLength(1) - 1]);
                    }
                    int ileKolumn = listadecyzji.Count;
                    float[,] decyzjeIIchIlosc = new float[2, ileKolumn];
                    for (int i = 0; i < decyzjeIIchIlosc.GetLength(1); i++)
                    {
                        decyzjeIIchIlosc[0, i] = listadecyzji[i];
                    }
                    for (int i = 0; i < decyzjeIIchIlosc.GetLength(1); i++)
                    {
                        float krotnosc = 0;
                        for (int ss = 0; ss < systemDoWczytywania.GetLength(0); ss++)
                        {
                            if (listadecyzji[i] == systemDoWczytywania[ss, systemDoWczytywania.GetLength(1) - 1])
                            {
                                krotnosc++;
                            }
                        }
                        decyzjeIIchIlosc[i, 1] = krotnosc;
                    }
                    return decyzjeIIchIlosc;
                }

                void westgermanyknn()
                {
                    westgermany = WczytajSystem(sciezkaDoPliku);
                    float kMAX = 0;
                    float[,] tmp = JakieDecyzje(westgermany);
                    for (int i = 0; i < tmp.GetLength(1); i++)
                    {
                        if (tmp[1, i] > kMAX)
                        {
                            kMAX = tmp[1, i];
                        }
                    }
                }
                void usaknn()
                {
                    westgermany = WczytajSystem(sciezkaDoPliku);
                    float kMAX = 0;
                    float[,] tmp = JakieDecyzje(usa);
                    for (int i = 0; i < tmp.GetLength(1); i++)
                    {
                        if (tmp[1, i] > kMAX)
                        {
                            kMAX = tmp[1, i];
                        }

                    }
                }

                void franceknn()
                {
                    westgermany = WczytajSystem(sciezkaDoPliku);
                    float kMAX = 0;
                    float[,] tmp = JakieDecyzje(france);
                    for (int i = 0; i < tmp.GetLength(1); i++)
                    {
                        if (tmp[1, i] > kMAX)
                        {
                            kMAX = tmp[1, i];
                        }
                    }
                }
                void ukknn()
                {
                    westgermany = WczytajSystem(sciezkaDoPliku);
                    float kMAX = 0;
                    float[,] tmp = JakieDecyzje(uk);
                    for (int i = 0; i < tmp.GetLength(1); i++)
                    {
                        if (tmp[1, i] > kMAX)
                        {
                            kMAX = tmp[1, i];
                        }
                    }
                }
                void canadaknn()
                {
                    westgermany = WczytajSystem(sciezkaDoPliku);
                    float kMAX = 0;
                    float[,] tmp = JakieDecyzje(canada);
                    for (int i = 0; i < tmp.GetLength(1); i++)
                    {
                        if (tmp[1, i] > kMAX)
                        {
                            kMAX = tmp[1, i];
                        }

                    }
                }
                void japanknn()
                {
                    westgermany = WczytajSystem(sciezkaDoPliku);
                    float kMAX = 0;
                    float[,] tmp = JakieDecyzje(japonia);
                    for (int i = 0; i < tmp.GetLength(1); i++)
                    {
                        if (tmp[1, i] > kMAX)
                        {
                            kMAX = tmp[1, i];
                        }
                    }
                }
                float [,,,,,] ObliczD (float[,] westgermanyknn, float[,] usaknn, float[,] franceknn, float[,] ukknn, float[,] canadaknn, float[,] japanknn) 
                {
                    float[,,,,,] obliczoneD = new float[westgermanyknn.GetLength(0), usaknn.GetLength(0), franceknn.GetLength(0), ukknn.GetLength(0), canadaknn.GetLength(0), japanknn.GetLength(0)];

                    for (int i = 0; 1 < westgermanyknn.GetLength(0); i++) 
                    {
                        for (int j = 0; j < usaknn.GetLength(0); j++) 
                        {
                            for (int k = 0; k < franceknn.GetLength(0); k++) 
                            {
                                for (int l = 0; l  < ukknn.GetLength(0); l++) 
                                {
                                    for (int m = 0; 1 < canadaknn.GetLength(0); m++) 
                                    {
                                        for (int n = 0; 1 < japanknn.GetLength(0); n++) 
                                        {
                                            float tmp = 0;
                                            for (int s = 0; s < westgermanyknn.GetLength(1) - 1; s++) 
                                            {
                                                for (int t = 0; t < usaknn.GetLength(1) - 1; t++)
                                                {
                                                    for (int r = 0; r < franceknn.GetLength(1) - 1; t++) 
                                                    {
                                                        for (int w = 0; w < ukknn.GetLength(1) - 1; w++) 
                                                        {
                                                            for (int y = 0; y < canadaknn.GetLength(1) - 1; y++) 
                                                            {
                                                                for (int z = 0; z < japanknn.GetLength(1) - 1; z++) 
                                                                {
                                                                    if (ktorametryka == 0) 
                                                                    {
                                                                        tmp = tmp + (westgermanyknn[i, s] - usaknn[j, t] * westgermanyknn[i, s] - usaknn[j, t]);
                                                                        tmp = tmp + (franceknn[k, r] - ukknn[l, w] * franceknn[k, r] - ukknn[l, w]);
                                                                        tmp = tmp + (canadaknn[m, y] - japanknn[n, z] * canadaknn[m, y] - japanknn[n, z]);
                                                                    }
                                                                    if (ktorametryka == 1) 
                                                                    {
                                                                        float tmp2 = (westgermanyknn[i, s] - usaknn[j, t]);
                                                                        if (tmp2 < 0) 
                                                                        {
                                                                            tmp2 = tmp2 * (-1);
                                                                        }
                                                                        float tmp3 = (franceknn[k, r] - ukknn[l, w]);
                                                                        if (tmp3 < 0)
                                                                        {
                                                                            tmp3 = tmp3 * (-1);
                                                                        }
                                                                        float tmp4 = (canadaknn[m, y] - japanknn[n, z]);
                                                                        if (tmp4 < 0)
                                                                        {
                                                                            tmp4 = tmp4 * (-1);
                                                                        }
                                                                        tmp = tmp + tmp2 + tmp3 + tmp4;
                                                                    }
                                                                    if (ktorametryka == 2) 
                                                                    {
                                                                        float tmp2 = (westgermanyknn[i, s] - usaknn[j, t]) / (westgermanyknn[i, s] + usaknn[j, t]);
                                                                        if (tmp2 < 0)
                                                                        {
                                                                            tmp2 = tmp2 * (-1);
                                                                        }
                                                                        float tmp3 = (franceknn[k, r] - ukknn[l, w]) / (franceknn[k, r] + ukknn[l, w]);
                                                                        if (tmp3 < 0)
                                                                        {
                                                                            tmp3 = tmp3 * (-1);
                                                                        }
                                                                        float tmp4 = (canadaknn[m, y] - japanknn[n, z]) / (canadaknn[m, y] + japanknn[n, z]);
                                                                        if (tmp4 < 0)
                                                                        {
                                                                            tmp4 = tmp4 * (-1);
                                                                        }
                                                                        tmp = tmp + tmp2 + tmp3 + tmp4;
                                                                    }
                                                                }
                                                            }obliczoneD[i,j,k,l,m,n] = tmp;
                                                            
                                                        }
                                                    }
                                                }
                                                return obliczoneD;


                                            }
                                            float [,] KlasyfikujKNN(float[,] obliczoneD, float KNN) 
                                            {
                                                float[,] decyzje = JakieDecyzje(westgermanyknn);
                                                float[,] decyzje1 = JakieDecyzje(franceknn);
                                                float[,] decyzje2 = JakieDecyzje(usaknn);
                                                float[,] decyzje3 = JakieDecyzje(ukknn);
                                                float[,] decyzje4 = JakieDecyzje(canadaknn);
                                                float[,] decyzje5 = JakieDecyzje(japanknn);

                                                float[,] sklasyfikowane = new float[westgermanyknn.GetLength(0) + 1, decyzje.GetLength(1) + 1];
                                                float[,] sklasyfikowane1 = new float[franceknn.GetLength(0) + 1, decyzje1.GetLength(1) + 1];
                                                float[,] sklasyfikowane2 = new float[usaknn.GetLength(0) + 1, decyzje2.GetLength(1) + 1];
                                                float[,] sklasyfikowane3 = new float[ukknn.GetLength(0) + 1, decyzje3.GetLength(1) + 1];
                                                float[,] sklasyfikowane4 = new float[canada.GetLength(0) + 1, decyzje4.GetLength(1) + 1];
                                                float[,] sklasyfikowane5 = new float[japanknn.GetLength(0) + 1, decyzje5.GetLength(1) + 1];

                                                float tmpDecyzja;
                                                for (int i = 0; i < decyzje.GetLength(1); i++) 
                                                {
                                                    sklasyfikowane[0, i] = -1;

                                                }
                                                for (int i = 0; i < decyzje.GetLength(1); i++)
                                                {
                                                    sklasyfikowane[0, i] = decyzje[0, i];

                                                }
                                                //liczenie tych sąsiadów
                                                for (int i = 0; i < westgermanyknn.GetLength(0); i++) 
                                                {
                                                    for (int w = 0; w < decyzje.GetLength(1); w++) 
                                                    {
                                                        float[,] najblizszeWartości = new float[2, k];
                                                        for (int l = 0; l < k; i++) 
                                                        {
                                                            najblizszeWartości[0, l] = 999;
                                                            najblizszeWartości[1, l] = 999;
                                                        }
                                                        tmpDecyzja = decyzje[0, w];
                                                        for (int we = 0; we < k; we++) 
                                                        {
                                                            for (int ss = 0; ss < westgermany.GetLength(0); ss++) 
                                                            {
                                                                bool wystepuje = true;
                                                                for (int y = 0; y < k; y++) 
                                                                {
                                                                    if (najblizszeWartości[1, y] == ss)
                                                                    {
                                                                        wystepuje = true;
                                                                        y = k;
                                                                    }
                                                                    else
                                                                    {
                                                                        wystepuje = false;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }






                                                for (int j = 0; j < decyzje1.GetLength(1); j++)
                                                {
                                                    sklasyfikowane1[0, i] = -1;

                                                }
                                                for (int j = 0; j < decyzje1.GetLength(1); j++)
                                                {
                                                    sklasyfikowane1[0, j] = decyzje1[0, j];

                                                }
                                                for (int k = 0; k < decyzje2.GetLength(1); k++)
                                                {
                                                    sklasyfikowane2[0, k] = -1;

                                                }
                                                for (int k = 0; k < decyzje2.GetLength(1); k++)
                                                {
                                                    sklasyfikowane2[0, k] = decyzje2[0, k];

                                                }
                                                for (int l = 0; l < decyzje3.GetLength(1); l++)
                                                {
                                                    sklasyfikowane3[0, l] = -1;

                                                }
                                                for (int l = 0; l < decyzje3.GetLength(1); l++)
                                                {
                                                    sklasyfikowane3[0, l] = decyzje3[0, l];

                                                }
                                                for (int m = 0; m < decyzje4.GetLength(1); m++)
                                                {
                                                    sklasyfikowane4[0, m] = -1;

                                                }
                                                for (int m = 0; m < decyzje4.GetLength(1); m++)
                                                {
                                                    sklasyfikowane4[0, m] = decyzje4[0, m];

                                                }
                                                for (int n = 0; n < decyzje5.GetLength(1); n++)
                                                {
                                                    sklasyfikowane5[0, n] = -1;

                                                }
                                                for (int n = 0; n < decyzje5.GetLength(1); n++)
                                                {
                                                    sklasyfikowane5[0, n] = decyzje5[0, n];

                                                }
                                            }
                                            }
                                        }
                                    }
                                }
                            }
                    }
                    
                }
               



            }
        }
        }
    }



