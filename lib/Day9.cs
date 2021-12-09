namespace Advent2021
{
    class Day9
    {
        public class Point
        {
            public int X { get; private set; } = 0;
            public int Y { get; private set; } = 0;

            public Point( int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object? obj)
            {
                if ( (obj == null) || ! this.GetType().Equals( obj.GetType() ) )
                {
                    return false;
                } else {
                    Point p = (Point) obj;

                    return X == p.X && Y == p.Y;
                }
            }

            public override int GetHashCode()
            {
                return ( X << 2 ) ^ Y;
            }

            public override string ToString()
            {
                return $"({X},{Y})";
            }
        }

        public class HeightMap
        {
            private int[,] Data { get; set; } = new int[0,0];
            public int Cols { get; private set; } = 0;
            public int Rows { get; private set; } = 0;

            public HeightMap() {}

            public void Init( string input )
            {
                var lines = input.Split( '\n' );

                Cols = lines[0].Length;
                Rows = lines.Length;

                Data = new int[Cols,Rows];

                var y = 0;

                foreach ( var row in lines ) {
                    var x = 0;
                    foreach( var n in row ) {
                        Data[x++, y] = n - '0';
                    }
                    y ++;
                }

            }

            public override string ToString()
            {
                var wr = new StringWriter();

                for ( var y = 0; y < Rows; y ++ ) {
                    for ( var x = 0; x < Cols; x ++ ) {
                        wr.Write( Data[x,y] );
                    }
                    wr.WriteLine();
                }


                return wr.ToString();
            }

            public int LowPointValue( int x, int y )
            {
                var isLowPoint = true;
                var val = Data[x,y];

                if ( x > 0 ) if ( Data[x-1,y] <= val ) isLowPoint = false;
                if ( x < Cols - 1 ) if ( Data[x+1,y] <= val ) isLowPoint = false;
                if ( y > 0 ) if ( Data[x,y-1] <= val ) isLowPoint = false;
                if ( y < Rows - 1 ) if ( Data[x,y+1] <= val ) isLowPoint = false;

                return isLowPoint ? val + 1 : 0;
            }

            public int RiskLevel()
            {
                var riskLevel = 0;
            
                for ( var y = 0; y < Rows; y ++ ) {
                    for ( var x = 0; x < Cols; x ++ ) {
                        riskLevel += LowPointValue( x, y );
                    }
                }

                return riskLevel;
            }

            public List<Point> LowPoints()
            {
                var lowPoints = new List<Point>();

                for ( var y = 0; y < Rows; y ++ ) {
                    for ( var x = 0; x < Cols; x ++ ) {
                        if ( LowPointValue( x, y ) != 0 )
                        {
                            lowPoints.Add( new Point( x, y ) );
                        }
                    }
                }

                return lowPoints;
            }

            public void BasinAt( Point point, List<Point> basin )
            {
                if ( ! basin.Contains( point ) ) {
                    basin.Add( point );

                    if ( point.X > 0 ) if ( Data[point.X-1,point.Y] < 9 ) BasinAt( new Point( point.X-1, point.Y ), basin );
                    if ( point.X < Cols - 1 ) if ( Data[point.X+1,point.Y] < 9 ) BasinAt( new Point( point.X+1, point.Y ), basin );
                    if ( point.Y > 0 ) if ( Data[point.X,point.Y-1] < 9 ) BasinAt( new Point( point.X, point.Y-1 ), basin );
                    if ( point.Y < Rows - 1 ) if ( Data[point.X,point.Y+1] < 9 ) BasinAt( new Point( point.X, point.Y+1 ), basin );
                }
            }
        }

        public Day9()
        {
        }

        public HeightMap GetHeightMap()
        {
            var heightMap = new HeightMap();

            heightMap.Init( Day9Data.INPUT );

            return heightMap;
        }

        public ( int, int ) Answer()
        {
            var heightMap = GetHeightMap();

            // Part 1
            var result1 = ( heightMap.Rows, heightMap.RiskLevel() );

            // System.Console.WriteLine( heightMap.ToString() );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            var lowPoints = heightMap.LowPoints();

            // System.Console.WriteLine( $"Low points: {Utils.ArrayToString(lowPoints.ToArray())}" );

            var basins = new List<List<Point>>();

            foreach ( var point in lowPoints ) {
                var basin = new List<Point>();

                heightMap.BasinAt( point, basin );

                basins.Add( basin );
            }

            foreach ( var basin in basins ) {
                // System.Console.WriteLine( $"basin: {Utils.ArrayToString( basin.ToArray() )}" );
            }

            var answer = basins
                .Select( ( b, i ) => b.Count )
                .OrderByDescending( n => n )
                .Take( 3 )
                .Aggregate( 1, ( acc, next ) => acc * next );

            var result2 = ( lowPoints.Count, answer );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
