using System.Text.RegularExpressions;

namespace TextParsing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "sample.txt";

            ParseWords(fileName);

            ParseSentences(fileName);

            ParsePunctuationMarks(fileName);

            FindLongestSentense(ParseSentences(fileName));

            FindShortestSentense(ParseSentences(fileName));

            FindLetter(fileName);
        }

        static List<Match> ParsePunctuationMarks(string fileName)
        {
            string content;
            using (StreamReader file = new StreamReader(fileName))
                content = file.ReadToEnd();
            var regex1 = Regex.Replace(content, "(?i)[^-!@#$%^&*()<>/*_,.]", "");
            string pattern = @"[^-!@#$%^&*()<>/*_,.]";
            Regex regex = new Regex(pattern, RegexOptions.None);
            var punctuationMarks = regex.Matches(regex1).ToList();
            return punctuationMarks;
        }

        static string[] ParseSentences(string fileName)
        {
            string content;
            using (StreamReader file = new StreamReader(fileName))
                content = file.ReadToEnd();
            var regex1 = Regex.Replace(content, "(?i)[^A-Z,.,?,!,\\n ]", "");
            var regex2 = Regex.Replace(regex1, @"(I|V|X)", "");
            string[] sentences = regex2.Split(new char[] { '.', '?', '!', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return sentences;
        }

        static void FindLongestSentense(string[] array)
        {

            try
            {
                using (StreamWriter file = new StreamWriter("specialSearch.txt"))
                {
                    int length = 0;
                    string longestSentence = "";
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        if (array[i].Length > length)
                        {
                            length = array[i].Length;
                            longestSentence = array[i];
                        }
                    }

                    file.WriteLine("Longest sentence by number of symbols - " + longestSentence);
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void FindShortestSentense(string[] array)
        {
            try
            {
                using (StreamWriter file = new StreamWriter("specialSearch.txt", true))
                {
                    int length = 1000;
                    string shortestSentence = "";
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        var words = array[i].Split(" ");
                        for (int j = 0; j < words.Length - 1; j++)
                        {
                            if (words[j].Length < length)
                            {
                                length = array[j].Length;
                                shortestSentence = array[j];
                            }
                        }

                    }
                    file.WriteLine("Shortest sentence by number of words - " + shortestSentence);
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
        }        
        
        static void FindLetter(string fileName)
        {
            string content;
            using (StreamReader file = new StreamReader(fileName))
                content = file.ReadToEnd();
            var regex1 = Regex.Replace(content, "(?i)[^A-Z]", "");
            var regex2 = Regex.Replace(regex1, @"(\d +|I|V|X)", "");
            var characters = regex2.ToList();
            Dictionary<char, int> symbols = new Dictionary<char, int>();
            foreach (var symbol in characters)
            {
                var item = symbol.ToString().ToLower()[0];
                if (!symbols.ContainsKey(item))
                {
                    symbols.Add(item, 1);
                }
                else
                {
                    symbols[item]++;
                }
            }
            try
            {
                using (StreamWriter file = new StreamWriter("specialSearch.txt", true))
                {
                    int count = 0;
                    char mostFrequentChar = 'w';
                    foreach (var symbol in symbols.OrderBy(x => x.Key))
                    {
                        if(symbol.Value > count)
                        {
                            count = symbol.Value;
                            mostFrequentChar = symbol.Key;
                        }                       
                    }
                    file.WriteLine("The most frequent char " + mostFrequentChar + " using " + count + " times");
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void ParseWords(string fileName)
        {
            string content;
            using (StreamReader file = new StreamReader(fileName))
                content = file.ReadToEnd();
            var regex1 = Regex.Replace(content, "(?i)[^A-Z, ]", "");
            var regex2 = Regex.Replace(regex1, @"(\d +|I|V|X)", "");
            string pattern = @"([\wA-Za-z]+)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var words = regex.Matches(regex2).ToList();

            Dictionary<string, int> words2 = new Dictionary<string, int>();
            foreach (var word in words)
            {
                var item = word.ToString().ToLower();
                if (!words2.ContainsKey(item))
                {
                    words2.Add(item, 1);
                }
                else
                {
                    words2[item]++;
                }
            }
            try
            {
                using (StreamWriter file = new StreamWriter("words.txt"))
                {
                    foreach (var word in words2.OrderBy(x => x.Key))
                    {
                        file.WriteLine(word.Key + " - " + word.Value);
                    }
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}