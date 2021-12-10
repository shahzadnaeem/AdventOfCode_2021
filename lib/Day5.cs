namespace Advent2021
{
    class Day5
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
        public class Line
        {
            public Point P1 { get; set; }
            public Point P2 { get; set; }
            public bool flipped { get; }  = false;

            public Line( Point p1, Point p2 )
            {
                if ( p1.X <= p2.X && p1.Y <= p2.Y ) {
                    P1 = p1;
                    P2 = p2;
                    flipped = false;
                } else {
                    P1 = p2;
                    P2 = p1;
                    flipped = true;
                }
            }

            public override string ToString()
            {
                return $"{P1} -> {P2}";
            }

            public bool IsHorizontal()
            {
                return P1.Y == P2.Y;
            }

            public bool IsVertical()
            {
                return P1.X == P2.X;
            }

            public bool IsDiagonal()
            {
                return Math.Abs( P1.X - P2.X) == Math.Abs( P1.Y - P2.Y);
            }

            public bool IsSlope()
            {
                return !IsHorizontal() && !IsVertical() && !IsDiagonal();
            }
        }

        class OceanFloor
        {
            public const int SIZE = 1000;

            private int[,] Board { get; set; } = new int[SIZE,SIZE];

            public void Init()
            {
                for ( var row = 0; row < SIZE; row ++ ) {
                    for ( var col = 0; col < SIZE; col ++ ) {
                        Board[col,row] = 0;
                    }
                }
            }

            public override string ToString()
            {
                StringWriter wr = new StringWriter();

                var maxRow = SIZE < 15 ? SIZE : 40;
                var maxCol = maxRow;

                for ( var row = 0; row < maxRow; row ++ ) {
                    for ( var col = 0; col < maxRow; col ++ ) {
                        var val = Board[col,row];

                        if ( val == 0 ) {
                            wr.Write('.');
                        } else if ( val < 10 ) {
                            wr.Write((char)('0' + val));
                        } else {
                            wr.Write('+');
                        }
                    }

                    wr.WriteLine( maxRow != SIZE ? " ..." : "" );
                }

                if ( maxRow != SIZE ) {
                    wr.WriteLine( $"... [SIZE={SIZE}]" );
                }

                return wr.ToString();
            }

            private void AddPoint( int x, int y )
            {
                Board[x,y]++;
            }

            public void AddLine( Line line, bool addDiagonals = false )
            {
                if ( line.IsHorizontal() )
                {
                    var y = line.P1.Y;

                    for ( var x = line.P1.X; x <= line.P2.X; x++) {
                        AddPoint( x,y );
                    }
                } else if ( line.IsVertical() ) {
                    var x = line.P1.X;

                    for ( var y = line.P1.Y; y <= line.P2.Y; y++) {
                        AddPoint( x,y );
                    }
                } else if ( line.IsDiagonal() && addDiagonals ) {

                    var dx = line.P1.X < line.P2.X ? 1 : -1;
                    var dy = line.P1.Y < line.P2.Y ? 1 : -1;
                    var steps = Math.Abs( line.P1.X - line.P2.X ) + 1;

                    var x = line.P1.X;
                    var y = line.P1.Y;

                    for ( var i = 0; i < steps; i ++ )
                    {
                        AddPoint( x + i * dx, y + i * dy );
                    }
                }
            }

            public int NumCrossedLines()
            {
                var crossedLines = 0;

                for ( var row = 0; row < SIZE; row ++ ) {
                    for ( var col = 0; col < SIZE; col ++ ) {
                        if ( Board[col,row] > 1 ) {
                            crossedLines++;
                        }
                    }
                }

                return crossedLines;
            }
        }

        public Day5()
        {
        }

        public Point ParsePoint( string point )
        {
            var coords = point.Split(',');

            var x = Convert.ToInt32( coords[0] );
            var y = Convert.ToInt32( coords[1] );

            return new Point( x, y );
        }

        public Line ParseLine( string line )
        {
            var points = line.Split(" -> ");

            var p1 = ParsePoint( points[0] );
            var p2 = ParsePoint( points[1] );

            return new Line( p1, p2 );
        }

        public ( int, int ) Answer()
        {
            var inputs = Day5Data.INPUT.Split( '\n' );

            var numLines = 0;
            var maxX = 0;
            var maxY = 0;

            var parsed = inputs.Select( ( d, i ) => {

                numLines ++;

                var line = ParseLine( d );

                if ( line.P1.X > maxX ) maxX = line.P1.X;
                if ( line.P2.X > maxX ) maxX = line.P2.X;
                if ( line.P1.Y > maxX ) maxY = line.P1.Y;
                if ( line.P2.Y > maxX ) maxY = line.P2.Y;

                if ( numLines < 10 ) {
                    // Console.WriteLine( $"Line #{numLines}: {line}" );
                }

                return line;
            }).ToArray();

            Console.WriteLine( $"Parsed inputs = {parsed.Length}" );
            Console.WriteLine( $"  Max X = {maxX}, Max Y = {maxY}" );

            OceanFloor oceanFloor = new OceanFloor();

            // Part 1
            foreach( var line in parsed ) {
                if ( !line.IsSlope() ) {
                    oceanFloor.AddLine( line );
                } else {
                    Console.WriteLine( $"Skipping line: {line}" );
                }
            }

            var numCrossedLines = oceanFloor.NumCrossedLines();

            // Console.WriteLine( $"{oceanFloor}" );

            Console.WriteLine( $"Number of crossed lines: {oceanFloor.NumCrossedLines()}" );

            var result1 = ( parsed.Length, numCrossedLines );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            oceanFloor.Init();

            foreach( var line in parsed ) {
                if ( !line.IsSlope() ) {
                    oceanFloor.AddLine( line, true );
                } else {
                    Console.WriteLine( $"Skipping line: {line}" );
                }
            }

            numCrossedLines = oceanFloor.NumCrossedLines();

            // Console.WriteLine( $"{oceanFloor}" );

            Console.WriteLine( $"Number of crossed lines: {oceanFloor.NumCrossedLines()}" );

            var result2 = ( parsed.Length, numCrossedLines );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
