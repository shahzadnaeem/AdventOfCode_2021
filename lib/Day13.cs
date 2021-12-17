namespace Advent2021
{
    class Day13
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

        public class Board
        {
            private int[,] Data { get; set; } = new int[0,0];
            private Point[] Folds { get; set; } = new Point[0];
            public int Cols { get; private set; } = 0;
            public int Rows { get; private set; } = 0;

            public int FoldedCols { get; private set; } = 0;
            public int FoldedRows { get; private set; } = 0;

            public Board( string input )
            {
                var lines = input.Split( '\n' );

                var onPoints = true;
                var numFolds = 0;

                foreach (var line in lines)
                {
                    // Console.WriteLine( $"{line}" );

                    if ( onPoints ) {
                        if (line.Length == 0) {
                            onPoints = false;
                        } else {
                            var coords = line.Split(',');

                            var x = Convert.ToInt32( coords[0] );
                            var y = Convert.ToInt32( coords[1] );

                            if ( x > Cols ) Cols = x;
                            if ( y > Rows ) Rows = y;
                        }
                    } else {
                        numFolds ++;
                    }
                }

                // Console.WriteLine( $"Cols={Cols}, Rows={Rows}, Folds={numFolds}" );

                FoldedCols = Cols;
                FoldedRows = Rows;

                Data = new int[Cols+1,Rows+1];
                Folds = new Point[numFolds];

                onPoints = true;
                var f = 0;

                foreach ( var line in lines ) {
                    if ( onPoints ) {
                        if ( line.Length == 0 ) {
                            onPoints = false;
                        } else {
                            var coords = line.Split(',');

                            var x = Convert.ToInt32(coords[0]);
                            var y = Convert.ToInt32(coords[1]);

                            Data[x,y] = 1;
                        }
                    } else {                        
                        var d = line.Split(' ');
                        var fd = d[2].Split('=');

                        if ( fd[0] == "x" ) {
                            Folds[f++] = new Point( Convert.ToInt32(fd[1]), 0 );
                        } else {
                            Folds[f++] = new Point( 0, Convert.ToInt32(fd[1]) );
                        }
                    }
                }
            }

            public override string ToString()
            {
                var wr = new StringWriter();

                wr.WriteLine( $"{FoldedCols+1} x {FoldedRows+1}" );

                for ( var y = 0; y <= FoldedRows; y ++ ) {
                    for ( var x = 0; x <= FoldedCols; x ++ ) {
                        wr.Write( Data[x,y] != 0 ? '#' : '.' );

                        if ( x > 100 ) {
                            wr.Write( " ..." );
                            break;
                        }
                    }
                    wr.WriteLine();

                    if ( y > 10 ) {
                        wr.WriteLine( "...\n" );
                        break;
                    }
                }

                wr.WriteLine();

                foreach ( var fold in Folds ) {
                    wr.WriteLine( $"Fold: {fold}" );
                }

                return wr.ToString();
            }

            public void Fold( bool firstOnly = true )
            {
                foreach ( var line in Folds ) {
                    if (line.X == 0)
                    {
                        // Fold along X axis (Y)
                        for (var y = 0; y < line.Y; y++)
                        {
                            for (var x = 0; x <= FoldedCols; x++)
                            {
                                Data[x, y] = Data[x, y] + Data[x, 2 * line.Y - y];
                            }
                        }

                        FoldedRows = line.Y - 1;
                    }
                    else
                    {
                        // Fold along Y axis (X)
                        for (var x = 0; x < line.X; x++)
                        {
                            for (var y = 0; y <= FoldedRows; y++)
                            {
                                Data[x, y] = Data[x, y] + Data[2 * line.X - x, y];
                            }
                        }

                        FoldedCols = line.X - 1;
                    }

                    if ( firstOnly ) {
                        break;
                    }
                }
            }

            public int VisiblePoints()
            {
                var visiblePoints = 0;

                for ( var y = 0; y <= FoldedRows; y ++ ) {
                    for ( var x = 0; x <= FoldedCols; x ++ ) {
                        if ( Data[x,y] != 0 ) visiblePoints ++;
                    }
                }

                return visiblePoints;
            }
        }

        public Day13()
        {
        }

        private Board GetModel()
        {
            return new Board( Day13Data.INPUT );
        }

        public ( long, long ) Answer()
        {
            var input = GetModel();

            Console.WriteLine( $"INPUT\n\n{input}" );

            // Part 1
            input.Fold();

            Console.WriteLine( $"INPUT\n\n{input}" );

            var visiblePoints = input.VisiblePoints();

            var result1 = ( 0, visiblePoints );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            input = GetModel();

            input.Fold( false );

            Console.WriteLine( $"INPUT\n\n{input}" );

            visiblePoints = input.VisiblePoints();

            var result2 = ( 0, visiblePoints );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
