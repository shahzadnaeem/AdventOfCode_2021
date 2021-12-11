namespace Advent2021
{
    class DayN
    {
        public class Input
        {
            private string Data { get; set; } = "";

            public Input(string input)
            {
                Data = input;
            }


            public override string ToString()
            {
                return Data;
            }
        }

        public DayN()
        {
        }

        private Input GetData()
        {
            return new Input( DayNData.INPUT );
        }

        public ( long, long ) Answer()
        {
            var input = GetData();


            Console.WriteLine( $"INPUT = ${input}" );

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
