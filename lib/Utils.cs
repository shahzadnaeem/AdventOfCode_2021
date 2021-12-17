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
                if ( c > 0 ) wr.WriteLine(',');
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
            var adventDay = DateTime.Now.Day;
            var live = DateTime.Now.Month == 12 && DateTime.Now.Year == 2021;

            if ( live && adventDay == day ) {
                Title( $"👍 Today is Day {day} 👍" );
            }

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
