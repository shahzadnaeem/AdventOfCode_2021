namespace Advent2021
{
    class Day2
    {
        public Day2()
        {
        }

        public ( int, int ) Answer()
        {
            var inputs = Day2Data.INPUT.Split( '\n' );
            var directions = inputs.Select( ( d, i ) => {
                var dir = d.Split( ' ' );
                switch ( dir[0] ) {
                    case "forward":
                    {
                        return ( int.Parse( dir[1] ), 0 );
                    }

                    case "up":
                    {
                        return ( 0, -1 * int.Parse( dir[1] ) );
                    }

                    case "down":
                    {
                        return ( 0, int.Parse( dir[1] ) );
                    }

                    default:
                    {
                        throw new Exception( $"Invalid input: {dir[0]}" );
                    }
                }
            }).ToArray();

            Console.WriteLine( $"Input directions = {directions.Length}" );

            // Part 1

            //              Pos, Depth
            var result1 = ( 0,   0 );

            foreach ( var dir in directions ) {
                result1.Item1 += dir.Item1;
                result1.Item2 += dir.Item2;
            }

            Console.WriteLine( $"Result1 = {result1}" );

            //              Pos, Depth, Aim
            var result2 = ( 0,   0,     0 );

            foreach ( var dir in directions ) {

                if ( dir.Item1 != 0 ) {
                    result2.Item1 += dir.Item1;
                    result2.Item2 += dir.Item1 * result2.Item3;
                } else {
                    result2.Item3 += dir.Item2;
                }
            }

            Console.WriteLine( $"Result2 = {result2}" );

            return ( result1.Item1 * result1.Item2, result2.Item1 * result2.Item2 );
        }
    }
}
