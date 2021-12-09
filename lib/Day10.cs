namespace Advent2021
{
    class Day10
    {
        public Day10()
        {
        }

        public ( int, int ) Answer()
        {
            Console.WriteLine( $"INPUT = {Day10Data.INPUT}" );

            // Part 1

            var result1 = ( 0, 0 );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            var result2 = ( 0, 0 );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
