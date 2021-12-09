namespace Advent2021
{
    public class SevenSegment
    {
        const int NUM_SEGMENTS = 7;

        public const int A = 1;
        public const int B = 2;
        public const int C = 4;
        public const int D = 8;
        public const int E = 16;
        public const int F = 32;
        public const int G = 64;

        // If you (me) don't get this right, then nothign works :)

        public const int ZERO = A + B + C + E + F + G;
        public const int ONE = C + F;
        public const int TWO = A + C + D + E + G;
        public const int THREE = A + C + D + F + G;
        public const int FOUR = B + C + D + F;
        public const int FIVE = A + B + D + F + G;
        public const int SIX = A + B + D + E + F + G;
        public const int SEVEN = A + C + F;
        public const int EIGHT = A + B + C + D + E + F + G;
        public const int NINE = A + B + C + D + F + G;

        public SevenSegment()
        {
        }

        public Dictionary<int,int> ValuesLookup()
        {
            return new Dictionary<int, int> {
                { ZERO, 0 },
                { ONE, 1 },
                { TWO, 2 },
                { THREE, 3 },
                { FOUR, 4 },
                { FIVE, 5 },
                { SIX, 6 },
                { SEVEN, 7 },
                { EIGHT, 8 },
                { NINE, 9 }
            };
        }

        public int[] StdMapping()
        {
            return new int[] { A, B, C, D, E, F, G };
        }

        public Dictionary<int,bool> TargetValues()
        {
            return new Dictionary<int, bool> {
                { ZERO, false },
                { ONE, false },
                { TWO, false },
                { THREE, false },
                { FOUR, false },
                { FIVE, false },
                { SIX, false },
                { SEVEN, false },
                { EIGHT, false },
                { NINE, false }
            };
        }

        private void DoPermute( int[] mapping, int start, int end, List<int[]> permutedMappings )
        {
            if ( start == end ) {
                permutedMappings.Add( mapping.ToArray() );
            } else {
                for ( var i = start; i <= end; i++ ) {
                    int tmp;

                    tmp = mapping[start]; mapping[start] = mapping[i]; mapping[i] = tmp;
                    DoPermute( mapping, start + 1, end, permutedMappings );
                    tmp = mapping[start]; mapping[start] = mapping[i]; mapping[i] = tmp;
                }
            }
        }

        public List<int[]> AllMappings()
        {
            var stdMappings = StdMapping();
            var allMappings = new List<int[]>();

            DoPermute( stdMappings, 0, stdMappings.Length - 1, allMappings );

            return allMappings;
        }

        public int GetMappedValue( string input, int[] mapping )
        {
            var value = 0;

            foreach (var segment in input)
            {
                var i = segment - 'a';

                value += mapping[ i ];
            }

            // System.Console.WriteLine( $"  Mapped \"{input}\" to {value}" );

            return value;
        }

        public bool MappingWorks( string[] inputs, int[] mapping )
        {
            var mappingWorks = true;

            var targetValues = TargetValues();

            // System.Console.WriteLine( $"  Checking: {Utils.ArrayToString(inputs)} with {Utils.ArrayToString(mapping)} ..." );

            foreach ( var digit in inputs ) {
                var value = GetMappedValue( digit, mapping );

                if ( targetValues.ContainsKey( value ) && ! targetValues[ value ] ) {
                    targetValues[ value ] = true;
                } else {
                    mappingWorks = false;
                    break;
                }
            }

            if ( mappingWorks ) {
                mappingWorks = targetValues.All( ( KeyValuePair<int, bool> kv ) => kv.Value );
            }

            return mappingWorks;
        }

        public int FindValue( string[] inputs, string[] outputs )
        {
            var value = -1;

            var allMappings = AllMappings();

            foreach (var mapping in allMappings)
            {
                if ( MappingWorks( inputs, mapping ) ) {
                    var valuesLookup = ValuesLookup();
                    value = 0;

                    foreach( var digit in outputs ) {
                        var rawValue = GetMappedValue( digit, mapping );

                        value = 10 * value + valuesLookup[ rawValue ];
                    }

                    // Console.WriteLine( $"Value: {value}" );

                    break;
                }
            }

            return value;
        }
    }

    public class Data
    {
        public string[] Inputs { get; private set; } = new string[0];
        public string[] Outputs { get; private set; } = new string[0];

        public Data()
        {
        }

        public void Init( string s )
        {
            var parts = s.Split( " | " );

            Inputs  = parts[0].Split(' ');
            Outputs = parts[1].Split(' ');
        }

        public int Output_1_4_7_8s()
        {
            return Outputs.Where( s =>
                s.Length == 2 || s.Length == 3 ||
                s.Length == 4 || s.Length == 7 ).Count();
        }
    }

    class Day8
    {
        public Day8()
        {
        }

        public Data[] GetData()
        {
            return Day8Data.INPUT.Split('\n')
                .Select ( ( d ) => {
                    var data = new Data();
                    data.Init(d);
                    return data;
                })
                .ToArray();
        }

        public ( int, int ) Answer()
        {
            var data = GetData();
            var sseg = new SevenSegment();

            Console.WriteLine( $"#Data rows = {data.Length}" );

            // Part 1

            var total = data.Select( d => d.Output_1_4_7_8s() ).Sum();

            var result1 = ( data.Length, total );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            total = 0;

            foreach( var display in data ) {
                var value = sseg.FindValue( display.Inputs, display.Outputs );

                if ( value > 0 ) {
                    total += value;
                }
            }

            var result2 = ( data.Length, total );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
