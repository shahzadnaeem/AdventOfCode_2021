namespace Advent2021
{
    class Day10
    {
        public class Line
        {
            private static readonly Dictionary<char,int>  ErrorScores;
            private static readonly Dictionary<char,int>  CompletionScores;
            private static readonly List<char> Openers;
            private static readonly Dictionary<char,char> Closers;
            private string Data { get; set; } = "";

            public Line ( string s )
            {
                Data = s;
            }

            static Line()
            {
                ErrorScores = new Dictionary<char,int> {
                    { ')', 3 },
                    { ']', 57 },
                    { '}', 1197 },
                    { '>', 25137 }
                };

                CompletionScores = new Dictionary<char,int> {
                    { '(', 1 },
                    { '[', 2 },
                    { '{', 3 },
                    { '<', 4 }
                };

                Openers = new List<char> {
                    '(', '[', '{', '<'
                };

                Closers = new Dictionary<char,char> {
                    { ')', '(' },
                    { ']', '[' },
                    { '}', '{' },
                    { '>', '<' }
                };
            }

            public long Check( bool completeCorruptScore = false )
            {
                long result = 0;

                var tracker = new LinkedList<char>();

                foreach ( var c in Data ) {

                    if ( Openers.Contains( c ) ) {
                        tracker.AddLast( c );
                    } else if ( Closers.ContainsKey( c ) ) {
                        if ( tracker.Last != null && tracker.Last.Value == Closers[c] )
                        {
                            tracker.RemoveLast();
                        } else {
                            result += ErrorScores[c];
                            break;
                        }
                    }
                }

                if ( result == 0 && completeCorruptScore ) {
                    if ( tracker.Count != 0 ) {
                        List<char> fixes = tracker.ToList();

                        fixes.Reverse();

                        foreach ( var c in fixes ) {
                            result = result * 5 + CompletionScores[c];
                        }

                        // System.Console.WriteLine( $"Fixing '{Data}' - score={result}" );
                    }
                }

                return result;
            }
        }

        public Day10()
        {
        }

        public Line[] GetData()
        {
            return Day10Data.INPUT.Split('\n')
                    .Select( (d, i) => new Line(d) )
                    .ToArray();
        }

        public ( long, long ) Answer()
        {
            var data = GetData();

            Console.WriteLine( $"#Lines in input = {data.Length}" );

            // Part 1

            long errors = 0;

            foreach( var line in data ) {
                errors += line.Check();
            }

            var result1 = ( data.Length, errors );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            var corruptData = data.Where( d => d.Check() == 0 ).ToArray();

            Console.WriteLine( $"#Corrupt lines in input = {corruptData.Length}" );

            var completionScores = corruptData
                    .Select( d => d.Check( true ) )
                    .OrderBy( n => n )
                    .ToArray();

            var result2 = ( corruptData.Length, completionScores[completionScores.Length/2] );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
