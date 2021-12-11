namespace Advent2021
{
    class Day11
    {
        class Input
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

        public Day11()
        {
        }

        private Input GetInput()
        {
            return new Input( Day11Data.INPUT );
        }

        public ( long, long ) Answer()
        {
            var input = GetInput();


            Console.WriteLine( $"\nINPUT\n{input}\n" );

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
