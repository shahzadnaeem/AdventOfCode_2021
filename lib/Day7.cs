namespace Advent2021
{
    class Day7
    {
        public class Crabs
        {
            public Dictionary<int,int> Data = new Dictionary<int, int>();

            public Crabs() {}

            public void Init( int[] data )
            {
                Data = new Dictionary<int, int>();

                foreach ( var d in data ) {
                    if ( !Data.ContainsKey(d) ) {
                        Data[d] = 1;
                    } else {
                        Data[d]++;
                    }
                }
            }

            public int MaxPos()
            {
                return Data.Keys.Max();
            }

            public int SumTo( int n )
            {
                return n * ( n + 1 ) / 2;
            }

            public int FuelTo( int pos, bool expensive = false )
            {
                int fuelTo = 0;

                foreach ( var k in Data.Keys )
                {
                    var dist = Math.Abs( pos - k );
                    var cost = expensive ? SumTo( dist ) : Math.Abs( dist );

                    // Console.WriteLine( $"{k} to {pos}: {cost} * {Data[k]}" );

                    fuelTo += ( cost * Data[k] );
                }

                // Console.WriteLine( $"Total cost to {pos}: {fuelTo}\n" );

                return fuelTo;
            }
        }


        public Day7()
        {
        }

        public int[] GetData()
        {
            return Day7Data.INPUT.Split(',')
                .Select( d => Convert.ToInt32( d ) )
                .ToArray();
        }

        public ( int, int ) Answer()
        {
            var data = GetData();

            Console.WriteLine( $"#Data items = {data.Length}" );

            var crabs = new Crabs();

            crabs.Init( data );

            // Part 1
            var bestPos = 0;
            var bestFuel = 0;

            var maxPos = crabs.MaxPos();

            Console.WriteLine( $"  Max pos = {maxPos}" );

            for ( var pos = 0; pos <= maxPos; pos ++ )
            {
                var fuelTo = crabs.FuelTo( pos );

                if ( pos == 0 || fuelTo < bestFuel ) {
                    bestPos = pos;
                    bestFuel = fuelTo;
                }
            }

            var result1 = ( bestPos, bestFuel );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            bestPos = 0;
            bestFuel = 0;

            for ( var pos = 0; pos <= maxPos; pos ++ )
            {
                var fuelTo = crabs.FuelTo( pos, true );

                if ( pos == 0 || fuelTo < bestFuel ) {
                    bestPos = pos;
                    bestFuel = fuelTo;
                }
            }

            var result2 = ( bestPos, bestFuel );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
