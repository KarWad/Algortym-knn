// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists
using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Threading.Tasks;


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
    public static int TestK = 0;
    public static int TrainK = 0;

    private static int i;
    private static float[,] SystemTreningowy;
    private static float[,] SystemTestowy;
    private static object ktoraMetryka;

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
            else if (!(File.Exists(path) && !Directory.Exists(path)))    // this code runs when all  app args fails
            {
                ProcessDirectory(Environment.CurrentDirectory + @"\Reuters");  //just checks %CD%\Reuters
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

        Console.WriteLine("Zbiór Testowy:" + TestK);
        Console.WriteLine("Zbiór Uczący:" + TrainK);
        Console.ReadLine();
    }


    // implementation for floating-point  Manhattan Distance
    public static float ManhattanDistance(float x1, float x2, float y1, float y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }
    // implementation for floating-point EuclideanDistance
    public static float EuclideanDistance(float x1, float x2, float y1, float y2)
    {
        float square = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        return square;
    }

    // implementation for floating-point Chebyshev Distance
    public static float ChebyshevDistance(float dx, float dy)
    {
        // not quite sure if the math is correct here
        return 1 * (dx + dy) + (1 - 2 * 1) * (dx - dy);
    }



    // Process all files in the directory passed in, recurse on any directories
    // that are found, and process the files they contain.
public static void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
        {
            ProcessFile(fileName);
        }

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


        //k-NN
        float[,] wczytajsystem(string path)
        {
            float[,] systemdecyzyjny;
            //var odpowiedzialny za czytanie każdej linijki czytanego systemu
            var linie = System.IO.File.ReadAllLines(path);
            int ilosckolumn = 0, iloscwierszy = 0;
            //Funkcja do usuwania pustych znaków
            var linia2 = linie[0].Trim();
            //Funkcja split dzieli linie na kolejne na podstawie określonego znaku, który tutaj jest spacją
            var liczby2 = linia2.Split(' ');
            iloscwierszy = linie.Length;
            ilosckolumn = liczby2.Length;
            systemdecyzyjny = new float[iloscwierszy, ilosckolumn];
            //wczytywanie systemu do tablicy
            for (int i = 0; i < linie.Length; i++)
            {
                var linia = linie[i].Trim();
                var liczby = linia.Split();
                for (int j = 0; j < liczby.Length; j++)
                {
                    //Usuwamy puste znaki
                    systemdecyzyjny[i, j] = float.Parse(liczby[j].Trim());
                }
            }
            return systemdecyzyjny;
        }
        //Metoda która określa maksymalną ilość sąsiadów, którą możemy wybrać do naszego programu
        //Metoda Jakie decyzje zwraca tablice decyzji i ich krotności
        float[,] Jakiedecyzje(float[,] systemDoWczytywania)
        {
            //lista która przechowuje decyzje w wczytanym systemie
            var listaDecyzji = new List<float>();
            for (int i = 0; i < systemDoWczytywania.GetLength(0); i++)
            {
                //Pętla która wyszukuje deczyje
                bool wystepuje = false;
                for (int w = 0; w < listaDecyzji.Count; w++)
                {
                    if (systemDoWczytywania[i, systemDoWczytywania.GetLength(1) - 1] == listaDecyzji[w])
                    {
                        //Nie dodajemy decyzji występującej na danej liście
                        wystepuje = true;
                        w = listaDecyzji.Count;
                    }
                }
                if (wystepuje == false)
                {
                    listaDecyzji.Add(systemDoWczytywania[i, systemDoWczytywania.GetLength(1) - 1]);
                }
            }//Pobieramy długość listy w celu stworzenia tablicy która będzie zwracana z metody
            int ileKolumn = listaDecyzji.Count;
            //pierwszy wiersz przechowuje wartość decyzji a drugi jej krotność
            float[,] decyzjeIIchIlosc = new float[2, ileKolumn];
            for (int i = 0; i < decyzjeIIchIlosc.GetLength(1); i++)
            {
                //Przypisywanie wartości decyzji
                decyzjeIIchIlosc[0, i] = listaDecyzji[i];
            }
            for (int i = 0; i < decyzjeIIchIlosc.GetLength(1); i++)
            {
                //Przypisywanie krotności decyzji
                float krotnosc = 0;
                for (int ss = 0; ss < systemDoWczytywania.GetLength(0); ss++)
                {
                    if (listaDecyzji[i] == systemDoWczytywania[ss, systemDoWczytywania.GetLength(1) - 1])
                    {
                        //Jeżeli decyzja się powtarza do zwiększamy jej krotność o 1
                        krotnosc++;
                    }
                }
                decyzjeIIchIlosc[1, i] = krotnosc;
            }
            return decyzjeIIchIlosc;

        }

         void systemTreningowy(string path)
        {
            //Zapisujemy ścieżkę do pliku
            path = File.ReadAllText(path);
            SystemTreningowy = wczytajsystem(path);
            float kMAX = 0;
            //Ustalamy maksymalną ilość sąsiadów dla któych może działać program
            float[,] analiza = Jakiedecyzje(SystemTreningowy);

            for (int i = 0; i < analiza.GetLength(1); i++)
            //Maksymalna ilość sąsiadów to ilość powtórzeń danej decyzji
            {
                if (analiza[1, i] > kMAX)
                {
                    kMAX = analiza[1, i];
                }
            }
        }
        void systemTestowy(string path)
        {
            path = File.ReadAllText(path);
            SystemTestowy = wczytajsystem(path);
        }










        float[,] ObliczD (float[,] systemTestowy, float[,] systemTreningowy) 
        {
            //Ilość obiektów systemu treningowego będzie teraz ilością kolumn
            float[,] obliczoneD = new float[systemTestowy.GetLength(0), systemTreningowy.GetLength(0)];
        
            for (int i = 0; i < systemTestowy.GetLength(0); i++) 
            {
                for (int w = 0; w < systemTreningowy.GetLength(0); w++)
                {
                    //Zmienna przechowuje wartośc działania, porównania jednego obiektu systemu treningowego z systemem testowym
                    float tmp = 0;
                    for (int ss = 0; ss < systemTestowy.GetLength(0); ss++) 
                    {
                        tmp = tmp + EuclideanDistance(systemTestowy[i, ss], systemTreningowy[i, ss], systemTestowy[i, ss], systemTreningowy[w, ss]);
                        if (tmp < 0) 
                        {
                            tmp = tmp * (-1);
                        }
                        float tmp2 = ManhattanDistance(systemTestowy[i, ss], systemTreningowy[i, ss], systemTestowy[i, ss], systemTreningowy[w, ss]);
                        if (tmp2< 0) 
                        {
                            tmp2 = tmp2 * (-1);
                        }
                        tmp = tmp + tmp2;
                        float tmp3 = ChebyshevDistance(systemTestowy[i, ss],systemTreningowy[w,ss]);
                        if (tmp3< 0) 
                        {
                            tmp3 = tmp3 * (-1);
                        }
                        tmp = tmp+ tmp2 + tmp3;
                    }
                    obliczoneD[i, w] = tmp;
                }
               
            }
            return obliczoneD;
        }

        //-----------------------------------------------------
        int pwestgermany = 0;
        int pusa = 0;
        int pfrance = 0;
        int puk = 0;
        int pcanada = 0;
        int pjapan = 0;
        float [,]ptestk;
        float [,]ptraink;
        int k;
        int ktorametryka;
        //-----------------------------------------------------


        for (int i = 0; i < productList.Count; i++)
        {
            if (productList[i].BODY != null)
            {
                //Pokaż aktualnie przetwarzany plik
                Console.WriteLine("Aktualnie przetwarzany plik " + path);

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


                int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                allp += x;


            


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


                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pusa += x;

                }
            }
            else if (String.Equals(productList[i].PLACES, "france "))
            {
                france++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {

                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pfrance += x;
                }

            }
            else if (String.Equals(productList[i].PLACES, "uk "))
            {
                uk++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {
                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    puk += x;
                }

            }
            else if (String.Equals(productList[i].PLACES, "canada "))
            {
                canada++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {
                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pcanada += x;
                }

            }
            else if (String.Equals(productList[i].PLACES, "japan "))
            {
                japan++;
                wszystkieK++;
                if (productList[i].BODY != null)
                {
                    int x = productList[i].BODY.Split('.', '?', '!', ' ', ';', ':', ',').Length - 1;
                    pjapan += x;
                }


            }
        }
      
    }
}
