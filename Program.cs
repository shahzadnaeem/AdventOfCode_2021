using Advent;

var day1 = new Day1();


Utils.Red();
Console.WriteLine( "Advent of Code 2021" );

Utils.Green();
Console.WriteLine( "Day 1" );
Utils.White();
Console.WriteLine( $"Answer = {day1.Answer()}" );

class Utils
{
    public static void White() { Console.ForegroundColor = ConsoleColor.White; }
    public static void Red() { Console.ForegroundColor = ConsoleColor.Red; }
    public static void Green() { Console.ForegroundColor = ConsoleColor.Green; }

    public static void WriteLineStringArray( string[] items )
    {
        var num = 0;
        Console.Write( "[" );
        foreach( var s in items )
        {
            Console.Write( $"{s}" );
            if ( ++num != items.Length ) {
                Console.Write(",");
            }
        }
        Console.WriteLine( "]" );
    }
}


