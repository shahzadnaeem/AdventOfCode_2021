using System.Text.RegularExpressions;

namespace Advent2021
{
    class BingoBoard
    {
        // 5x5 board
        public const int SIZE = 5;

        private bool HaveWon { get; set; } = false;

        private (int,bool)[,] Board { get; set; } = new (int,bool)[SIZE,SIZE];

        public BingoBoard()
        {
        }

        public override string ToString()
        {
            StringWriter wr = new StringWriter();

            for ( var row = 0; row < SIZE; row ++ ) {
                for ( var col = 0; col < SIZE; col ++ ) {
                    wr.Write( Board[row,col] );
                    if ( col != SIZE - 1 ) {
                        wr.Write( ", " );
                    }
                }
                wr.WriteLine();
            }

            return wr.ToString();
        }

        public void Init( int row, int col, int num )
        {
            Board[row,col].Item1 = num;
            Board[row,col].Item2 = false;
        }

        public bool HasWinningRow( int rowNum )
        {
            var result = true;

            for ( var colNum = 0; colNum < SIZE; colNum ++ )
            {
                if ( ! Board[rowNum,colNum].Item2 )
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public bool HasWinningCol( int colNum )
        {
            var result = true;

            for ( var rowNum = 0; rowNum < SIZE; rowNum ++ )
            {
                if ( ! Board[rowNum,colNum].Item2 )
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public bool CheckIfWinner()
        {
            if (!HaveWon) {

                for (var rowColNum = 0; rowColNum < SIZE; rowColNum++)
                {
                    if (HasWinningRow(rowColNum))
                    {
                        HaveWon = true;
                        break;
                    }

                    if (HasWinningCol(rowColNum))
                    {
                        HaveWon = true;
                        break;
                    }
                }
            }

            return HaveWon;
        }

        public int WinningScore( int num )
        {
            var winningScore = 0;

            for ( var rowNum = 0; rowNum < SIZE; rowNum ++ ) {
                for ( var colNum = 0; colNum < SIZE; colNum++ ) {
                    if ( !Board[rowNum,colNum].Item2 ) {
                        winningScore += Board[rowNum,colNum].Item1;
                    }
                }
            }

            winningScore = winningScore * num;

            return winningScore;
        }

        public int CallNumber( int num )
        {
            var result = 0;

            // Search for num on Board, set it and determine if board is now winner
            for ( var rowNum = 0; rowNum < SIZE; rowNum ++ ) {
                for ( var colNum = 0; colNum < SIZE; colNum++ ) {
                    if ( Board[rowNum,colNum].Item1 == num ) {
                        Board[rowNum,colNum].Item2 = true;
                        if ( CheckIfWinner() ) {
                            result = WinningScore( num );
                        }
                        break;
                    }
                }
             }


            return result;
        }
    }

    class Day4
    {
        private int WinningBoardNum = 0;

        public Day4()
        {
        }

        private BingoBoard[] InitBoards( string[] boardsData )
        {
            var numBingoBoards = boardsData.Length / BingoBoard.SIZE;

            if ( numBingoBoards % BingoBoard.SIZE != 0 ) {
                throw new Exception( "Invalid input: Extra Bingo Board rows" );
            }

            var bingoBoards = new BingoBoard[ numBingoBoards ];

            Console.WriteLine( $"InitBoards: #Boards = {bingoBoards.Length}" );

            for ( var boardNum = 0; boardNum < numBingoBoards; boardNum ++ ) {

                bingoBoards[boardNum] = new BingoBoard();

                for ( var row = 0; row < BingoBoard.SIZE; row ++ ) {

                    var boardDataRow = boardNum * BingoBoard.SIZE + row;

                    // Console.WriteLine( $"Data row: {boardsData[boardDataRow]}" );

                    // Sneaky additional spaces in data - see also Trim() below :)
                    var re = new Regex(@"\s+");

                    var cols = re.Split( boardsData[ boardDataRow ].Trim() ).Select( (d,i) => Convert.ToInt32( d ) ).ToArray();

                    // Console.WriteLine( $"Board #{boardNum}" );

                    for ( var col = 0; col < BingoBoard.SIZE; col ++ ) {
                        bingoBoards[boardNum].Init(row,col,cols[col]);
                    }
                }
            }

            return bingoBoards;
        }

        private (int,int) CallNumber( BingoBoard[] bingoBoards, int number, bool lastWinner = false )
        {
            var result = ( 0, 0 );

            var numBoards = bingoBoards.Length;

            for (var boardNum = 0; boardNum < numBoards; boardNum++)
            {
                var res = 0;

                if (!lastWinner || (lastWinner && !bingoBoards[boardNum].CheckIfWinner()))
                {
                    res = bingoBoards[boardNum].CallNumber(number);

                    if (res != 0)
                    {
                        WinningBoardNum++;
                        // Console.WriteLine( $"Winning board #{WinningBoardNum}" );

                        if (!lastWinner || ( lastWinner && ( WinningBoardNum == numBoards ))) {
                            result = (boardNum, res);
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public ( int, int ) Answer()
        {
            var inputs = Day4Data.INPUT.Split( '\n' );

            var numbers = inputs[0].Split(',').Select( (d,i) => Convert.ToInt32( d ) ).ToArray();

            Console.WriteLine( $"#Called numbers = {numbers.Length}" );

            var boardsData = inputs.Where( (d) => d.Length < inputs[0].Length && d != "" ).ToArray();

            Console.WriteLine( $"Board inputs = {boardsData.Length}" );

            var bingoBoards = InitBoards( boardsData );
            WinningBoardNum = 0;

            // Console.WriteLine( $"Board[0] = {bingoBoards[0].ToString()}" );

            // Part 1 - Play bingo!

            var result1 = ( 0, 0 );

            foreach ( var num in numbers ) {
                result1 = CallNumber( bingoBoards, num );

                if ( result1.Item2 != 0 ) {
                    break;
                }
            }

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2 - Play all games!

            var result2 = ( 0, 0 );

            bingoBoards = InitBoards( boardsData );
            WinningBoardNum = 0;

            foreach ( var num in numbers ) {
                result2 = CallNumber( bingoBoards, num, true );

                if ( result2.Item2 != 0 ) {
                    break;
                }
            }

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
