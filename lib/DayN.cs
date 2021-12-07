namespace Advent2021
{
    class DayN
    {
        public DayN()
        {
        }

        public ( int, int ) Answer()
        {
            var inputs = DayNData.INPUT.Split( '\n' );

            var parsed = inputs.Select( ( d, i ) => {

                // Do whatever parsing is needed here

                // Tuple example output
                return ( 1, 2 );
            }).ToArray();

            Console.WriteLine( $"Parsed inputs = {parsed.Length}" );

            // Part 1

            //
            var result1 = ( 0,   0 );

            Console.WriteLine( $"Result1 = {result1}" );

            //
            var result2 = ( 0,   0,     0 );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item1 * result1.Item2, result2.Item1 * result2.Item2 );
        }
    }
}
