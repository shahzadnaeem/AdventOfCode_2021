namespace Advent2021
{
    class Day11
    {
        public class Point
        {
            public int X { get; set; } = 0;
            public int Y { get; set; } = 0;

            public Point() {}

            public Point( int x, int y )
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"{X},{Y}";
            }
        }

        public class Cavern {
            public int SIZE { get; private set; } = 0;

            private int[,] Board { get; set; } = new int[0,0];
            private bool[,] Flashes { get; set; } = new bool[0,0];

            public Cavern( string input )
            {
                var lines = input.Split( Environment.NewLine );

                SIZE = lines.Length;
                Board = new int[SIZE,SIZE];
                Flashes = new bool[SIZE,SIZE];

                var row = 0;
                var col = 0;

                foreach ( var line in lines ) {
                    col = 0;
                    foreach ( var ch in line ) {
                        Board[col++,row] = ch - '0';
                    }
                    row++;
                }
            }

            public override string ToString()
            {
                StringWriter wr = new StringWriter();

                var maxRow = SIZE < 15 ? SIZE : 40;
                var maxCol = maxRow;

                for ( var row = 0; row < maxRow; row ++ ) {
                    wr.Write( "\t" );

                    for ( var col = 0; col < maxRow; col ++ ) {
                        var val = Board[col,row];

                        if ( val == 0 ) {
                            wr.Write('.');
                        } else if ( val < 10 ) {
                            wr.Write((char)('0' + val));
                        } else {
                            wr.Write('+');
                        }

                        wr.Write(' ');
                    }

                    wr.WriteLine( maxRow != SIZE ? " ..." : "" );
                }

                if ( maxRow != SIZE ) {
                    wr.WriteLine( $"... [SIZE={SIZE}]" );
                }

                return wr.ToString();
            }

            private List<Point> SurroundingPoints( Point p )
            {
                var points = new List<Point>();

                Point[] deltas = new Point[] {
                    new Point( -1, -1 ),
                    new Point( 0, -1 ),
                    new Point( 1, -1 ),
                    new Point( -1, 0 ),
                    new Point( 1, 0 ),
                    new Point( -1, 1 ),
                    new Point( 0, 1 ),
                    new Point( 1, 1 )
                };

                foreach ( var d in deltas ) {
                    var np = new Point( p.X + d.X, p.Y + d.Y );

                    if ( np.X >= 0 && np.X < SIZE && np.Y >= 0 && np.Y < SIZE )
                    {
                        points.Add( np );
                    }
                }

                return points;
            }

            private List<Point> CheckOctopus( Point p )
            {
                var otherOctopii = new List<Point>();

                if ( !Flashes[p.X,p.Y] ) {
                    Board[p.X, p.Y] ++;

                    if (Board[p.X, p.Y] > 9)
                    {
                        Flashes[p.X,p.Y] = true;

                        otherOctopii = SurroundingPoints( p );
                    }
                }

                return otherOctopii;
            }

            public int Step( int steps = 100, bool findAllFlashStep = false )
            {
                var totalFlashes = 0;
                var allFlashStep = 0;

                for (var i = 0; i < steps; i++)
                {
                    // Reset Flashes each Step
                    Flashes = new bool[SIZE,SIZE];

                    var extraChecks = new List<Point>();

                    for (var y = 0; y < SIZE; y++)
                    {
                        for (var x = 0; x < SIZE; x++)
                        {
                            var octopus = new Point(x, y);

                            var checkResult = CheckOctopus( octopus );

                            extraChecks.AddRange( checkResult );
                        }
                    }

                    while ( extraChecks.Count > 0 ) {

                        // Console.WriteLine( $"  Extra checks: {extraChecks.Count}" );

                        var evenMoreChecks = new List<Point>();

                        foreach( var octopus in extraChecks ) {

                            var checkResult = CheckOctopus( octopus );

                            evenMoreChecks.AddRange( checkResult );
                        }

                        extraChecks = evenMoreChecks;
                    }

                    var stepFlashes = 0;

                    for (var y = 0; y < SIZE; y++)
                    {
                        for (var x = 0; x < SIZE; x++)
                        {
                            if ( Flashes[x,y] ) {
                                stepFlashes ++;
                                Board[x,y] = 0;
                            }
                        }
                    }

                    if ( findAllFlashStep ) {
                        if ( stepFlashes == SIZE * SIZE ) {
                            allFlashStep = i + 1;
                            break;
                        }
                    } else {
                        totalFlashes += stepFlashes;
                    }

                    // Console.WriteLine( $"\nSTEP {i+1}\n\n{this}\n" );
                }

                return findAllFlashStep ? allFlashStep : totalFlashes;
            }
        }

        public Day11()
        {
        }

        private Cavern GetData()
        {
            return new Cavern( Day11Data.INPUT );
        }

        public ( long, long ) Answer()
        {
            var cavern = GetData();

            Console.WriteLine( $"\nINPUT\n\n{cavern}\n" );

            // Part 1
            
            var flashes = cavern.Step();

            Console.WriteLine( $"\nAfter STEP 100\n\n{cavern}\n" );

            var result1 = ( 0, flashes );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            cavern = GetData();

            var allFlashStep = cavern.Step( 10000, true );

            Console.WriteLine( $"\nAll Flash STEP {allFlashStep}\n\n{cavern}\n" );

            var result2 = ( 0, allFlashStep );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
