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