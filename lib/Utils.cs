namespace Advent
{
    class Utils
    {
        public static void White() { Console.ForegroundColor = ConsoleColor.White; }
        public static void Red() { Console.ForegroundColor = ConsoleColor.Red; }
        public static void Green() { Console.ForegroundColor = ConsoleColor.Green; }

        public static void WriteLineStringArray(string[] items)
        {
            var num = 0;
            Console.Write("[");
            foreach (var s in items)
            {
                Console.Write($"{s}");
                if (++num != items.Length)
                {
                    Console.Write(",");
                }
            }
            Console.WriteLine("]");
        }

        public static void Title(string title)
        {
            Utils.Red();
            Console.WriteLine(title);
            Utils.White();
        }

        public static void Result(string day, string result)
        {
            Utils.Green();
            Console.WriteLine(day);
            Utils.White();
            Console.WriteLine(result);
        }
    }
}