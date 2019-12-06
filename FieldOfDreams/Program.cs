using System;
using System.Collections.Generic;

namespace FieldOfDreams
{
    class Program
    {
        static readonly List<Word> words = new List<Word>
        {
            new Word(0, "ЯКУБОВИЧ", "Кто бессменный ведущий нашей программы?"),
            new Word(1, "КТО ХОЧЕТ СТАТЬ МИЛЛИОНЕРОМ", "Программа конкурент"),
            new Word(2, "ЛИМОн", "Очень кислый фрукт")
        };

        static void Main()
        {
            words.ForEach(w => w.Answer = w.Answer.ToUpperInvariant());
            Console.WriteLine("Машина загадывает, человек отгадывает!");
            Console.WriteLine("Называть можно слово или букву");
            var rnd = new Random();
            rnd.Shuffle(words);
            foreach (var word in words)
            {
                string dialog = null;
                Console.WriteLine(word.Question);
                string[] subStrings = word.Answer.Split(' ');
                foreach (var str in subStrings)
                {
                    for (int i = 0; i < str.Length; i++) { dialog += "_"; }
                    dialog += " ";
                }
            label:
                Console.Write(dialog);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Назовите букву или слово");
                string a = Console.ReadLine().ToUpper();
                if (a.Length > 1 && a == word.Answer)
                {
                    Console.WriteLine("Правильно!");
                    continue;
                }
                else if (a.Length > 1 && a != word.Answer)
                {
                    Console.WriteLine("Не правильно! Попробуйте снова");                    
                    goto label;
                }
                else if (a.Length < 2 && !word.Answer.Contains(a))
                {
                    Console.WriteLine("Такой буквы нет! Попробуйте снова");
                    goto label;
                }
                else if (a.Length < 2 && word.Answer.Contains(a))
                {
                    Console.WriteLine($"Откройте букву {a}!");
                    var index = word.Answer.IndexOf(a);
                    while (index >= 0)
                    {
                        dialog = dialog.Remove(index, 1);
                        dialog = dialog.Insert(index, a);
                        index = word.Answer.IndexOf(a, index + 1);
                    }

                    if (dialog.Trim() == word.Answer.Trim())
                    {
                        Console.WriteLine("Правильно");
                        continue;
                    }
                    goto label;
                }               
            }
            Console.WriteLine("Слова закончились");    
            Console.Read();
        }
    }

    static class RandomExtensions
    {
        /// <summary>
        /// Перемешивает список слов
        /// </summary>
        public static void Shuffle<T>(this Random rng, List<T> array)
        {
            int n = array.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

    class Word
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public Word(int id, string answer, string question)
        {
            Id = id;
            Answer = answer;
            Question = question;
        }
    }
}
