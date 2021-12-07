namespace Advent2021
{
    class Utils
    {
        public static void White() { Console.ForegroundColor = ConsoleColor.White; }
        public static void Red() { Console.ForegroundColor = ConsoleColor.Red; }
        public static void Green() { Console.ForegroundColor = ConsoleColor.Green; }
        public static void Yellow() { Console.ForegroundColor = ConsoleColor.Yellow; }

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


        public static void Day( int day )
        {
            Utils.Green();
            Console.WriteLine( $"\nDay {day}");
            Utils.Yellow();
        }

        public static void Result(string result)
        {
            Utils.White();
            Console.WriteLine(result);
        }
    }
}