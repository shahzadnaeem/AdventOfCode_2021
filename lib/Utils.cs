namespace Advent2021
{
    class Utils
    {
        public static void White() { Console.ForegroundColor = ConsoleColor.White; }
        public static void Red() { Console.ForegroundColor = ConsoleColor.Red; }
        public static void Green() { Console.ForegroundColor = ConsoleColor.Green; }
        public static void Yellow() { Console.ForegroundColor = ConsoleColor.Yellow; }

        public static string ArrayToString<T>( T[] items )
        {
            var wr = new StringWriter();

            var c = items.Length;

            wr.Write( "[ " );

            foreach (var item in items)
            {
                wr.Write( $"{item}" );
                c--;
                if ( c > 0 ) wr.Write(',');
            }

            wr.Write( " ]" );

            return wr.ToString();
        }

        public static void Title(string title)
        {
            Console.WriteLine("\n");
            Utils.Red();
            Console.WriteLine(title);
            Console.WriteLine( new string( '=', title.Length ) );
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