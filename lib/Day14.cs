namespace Advent2021
{
    class Day14
    {
        public class Polymeriser
        {
            private string Start { get; set; } = "";
            private Dictionary<string,char> Rules { get; set; } = new Dictionary<string, char>();

            public Polymeriser(string input)
            {
                var lines = input.Split( Environment.NewLine );

                if ( lines.Length < 2 ) {
                    throw new Exception( "Invalid Day 14 input!" );
                }

                Start = lines[0];

                lines = lines.Skip(2).ToArray();

                foreach ( var rule in lines ) {

                    var s = rule.Split( " -> " );

                    Rules.Add( s[0], s[1][0] );
                }
            }

            public override string ToString()
            {
                var wr = new StringWriter();

                wr.WriteLine( $"{Start}\n" );

                foreach ( var rule in Rules ) {
                    wr.WriteLine( $"{rule.Key} -> {rule.Value}" );
                }

                return wr.ToString();
            }

            public string[] GetInputs( string from  )
            {
                var inputs = new List<string>();

                for ( var i = 0; i < from.Length - 1; i ++ ) {
                    inputs.Add( $"{from[i]}{from[i+1]}" );
                }

                // Last item on its own as terminator
                inputs.Add( $"{from[from.Length-1]}" );

                return inputs.ToArray();
            }

            private string Process( string input )
            {
                var output = $"{input[0]}";

                if ( Rules.ContainsKey( input ) ) {
                    output = $"{output[0]}{Rules[input]}";
                }

                return output;
            }

            public long Run( int times )
            {
                var output = Start;

                for ( var i = 0; i < times; i ++ ) {

                    Console.WriteLine( $"Iteration {i+1}" );

                    var inputs = GetInputs( output );

                    var wr = new StringWriter();

                    foreach( var input in inputs ) {
                        wr.Write( Process( input ) );
                    }

                    output = wr.ToString();
                }

                var chars = output.ToCharArray();

                var counts = chars.GroupBy( c => c )
                                    .Select( grp => grp.Count() );

                var max = counts.Max();
                var min = counts.Min();

                Console.WriteLine( $"Min = {min}, Max = {max}" );

                return max - min;
            }
        }

        public Day14()
        {
        }

        private Polymeriser GetModel()
        {
            return new Polymeriser( Day14Data.INPUT );
        }

        public ( long, long ) Answer()
        {
            var input = GetModel();

            Console.WriteLine( $"INPUT = {input}" );

            var op = input.Run( 10 );

            // Part 1
            var result1 = ( 0, op );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2

            // TODO: Naive implementation won't work!
            // op = input.Run( 40 );

            var result2 = ( 0, 0 );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
