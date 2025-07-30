using System;
using System.Collections.Generic;

class Program
{
    // Huvudlistan som lagrar alla blogginlägg (titel, text, datum)
    static List<string[]> minBlogg = new List<string[]>();

    static void Main()
    {
        // Lägger till tre exempelinlägg (i oordning för att visa sortering)
        minBlogg.Add(new string[] { "Zebra", "Sista posten", DateTime.Now.AddDays(-2).ToString() });
        minBlogg.Add(new string[] { "Alpha", "Första posten", DateTime.Now.AddDays(-3).ToString() });
        minBlogg.Add(new string[] { "Mellan", "Mitteninlägg", DateTime.Now.AddDays(-1).ToString() });

        bool igang = true;
        while (igang)
        {
            // Visar menyval för användaren
            Console.WriteLine("\nBLOGG - MENY");
            Console.WriteLine("1. Visa alla inlägg");
            Console.WriteLine("2. Skapa nytt inlägg");
            Console.WriteLine("3. Sök inlägg (titel)");
            Console.WriteLine("4. Redigera inlägg");
            Console.WriteLine("5. Ta bort inlägg");
            Console.WriteLine("6. Sortera inlägg (titel)");
            Console.WriteLine("7. Binär sökning (titel)");
            Console.WriteLine("8. Avsluta");
            Console.Write("Välj: ");

            string val = Console.ReadLine();
            Console.Clear();

            // Menyvalshantering
            switch (val)
            {
                case "1":
                    VisaAlla();
                    break;
                case "2":
                    SkapaInlagg();
                    break;
                case "3":
                    SokInlagg();
                    break;
                case "4":
                    Redigera();
                    break;
                case "5":
                    TaBort();
                    break;
                case "6":
                    Sortera();
                    break;
                case "7":
                    BinarSokning();
                    break;
                case "8":
                    igang = false;
                    break;
                default:
                    Console.WriteLine("Fel val, försök igen.");
                    break;
            }
        }
    }

    // Visar alla blogginlägg i listan
    static void VisaAlla()
    {
        if (minBlogg.Count == 0)
        {
            Console.WriteLine("Inga inlägg finns.");
            return;
        }

        foreach (var inlagg in minBlogg)
        {
            Console.WriteLine($"\nTitel: {inlagg[0]}\nText: {inlagg[1]}\nDatum: {inlagg[2]}");
        }
    }

    // Låter användaren skapa ett nytt inlägg
    static void SkapaInlagg()
    {
        Console.Write("Skriv titel: ");
        string titel = Console.ReadLine();
        Console.Write("Skriv text: ");
        string text = Console.ReadLine();
        string datum = DateTime.Now.ToString();

        minBlogg.Add(new string[] { titel, text, datum });
        Console.WriteLine("Inlägget har sparats!");
    }

    // Linjär sökning efter inlägg baserat på titel
    static void SokInlagg()
    {
        Console.Write("Sök titel: ");
        string sokning = Console.ReadLine();
        bool hittad = false;

        foreach (var inlagg in minBlogg)
        {
            if (inlagg[0] == sokning)
            {
                Console.WriteLine($"\nTitel: {inlagg[0]}\nText: {inlagg[1]}\nDatum: {inlagg[2]}");
                hittad = true;
            }
        }

        if (!hittad)
        {
            Console.WriteLine("Inlägg hittades inte.");
        }
    }

    // Redigerar ett befintligt inlägg genom att byta titel/text
    static void Redigera()
    {
        Console.Write("Titel att redigera: ");
        string titel = Console.ReadLine();

        for (int i = 0; i < minBlogg.Count; i++)
        {
            if (minBlogg[i][0] == titel)
            {
                Console.Write("Ny titel: ");
                minBlogg[i][0] = Console.ReadLine();
                Console.Write("Ny text: ");
                minBlogg[i][1] = Console.ReadLine();
                minBlogg[i][2] = DateTime.Now.ToString(); // Uppdaterar datum
                Console.WriteLine("Inlägg uppdaterat.");
                return;
            }
        }

        Console.WriteLine("Titel hittades inte.");
    }

    // Tar bort ett inlägg med angiven titel
    static void TaBort()
    {
        Console.Write("Titel att ta bort: ");
        string titel = Console.ReadLine();

        for (int i = 0; i < minBlogg.Count; i++)
        {
            if (minBlogg[i][0] == titel)
            {
                minBlogg.RemoveAt(i);
                Console.WriteLine("Inlägg borttaget.");
                return;
            }
        }

        Console.WriteLine("Titel hittades inte.");
    }

    // Sorterar listan alfabetiskt med Bubble Sort
    static void Sortera()
    {
        for (int i = 0; i < minBlogg.Count - 1; i++)
        {
            for (int j = 0; j < minBlogg.Count - i - 1; j++)
            {
                if (minBlogg[j][0].CompareTo(minBlogg[j + 1][0]) > 0)
                {
                    var temp = minBlogg[j];
                    minBlogg[j] = minBlogg[j + 1];
                    minBlogg[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Blogginlägg har sorterats alfabetiskt.");
    }

    // Binär sökning efter inläggstitel (kräver sorterad lista)
    static void BinarSokning()
    {
        Console.Write("Titel att söka (lista måste vara sorterad): ");
        string sok = Console.ReadLine();
        int low = 0, high = minBlogg.Count - 1;
        bool hittad = false;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            int cmp = minBlogg[mid][0].CompareTo(sok);

            if (cmp == 0)
            {
                Console.WriteLine($"\nTitel: {minBlogg[mid][0]}\nText: {minBlogg[mid][1]}\nDatum: {minBlogg[mid][2]}");
                hittad = true;
                break;
            }
            else if (cmp < 0)
                low = mid + 1;
            else
                high = mid - 1;
        }

        if (!hittad)
            Console.WriteLine("Inlägget hittades inte med binär sökning.");
    }
}
