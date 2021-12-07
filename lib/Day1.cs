namespace Advent2021
{
    class Day1
    {
        public Day1()
        {
        }

        public ( int, int ) Answer()
        {
            var inputs = Day1Data.INPUT.Split( '\n' );
            var nums = inputs.Select( ( s, i ) => int.Parse( s )).ToArray();

            Console.WriteLine( $"Input numbers = {nums.Length}" );

            // Part 1

            var result1 = 0;

            var first = true;
            var curr = 0;

            foreach ( var num in nums ) {
                if ( first ) {
                    first = false;
                } else {
                    if ( num > curr ) result1 ++;
                }

                curr = num;
            }

            // Part 2

            const int WIN_SIZE = 3;
            var sums = new List<int>();

            for ( var i = 0; i <= nums.Length - WIN_SIZE; i ++ )
            {
                var sum = nums[i] + nums[i+1] + nums[i+2];
                sums.Add(sum);
            }

            Console.WriteLine( $"Input sums = {sums.Count}" );

            var result2 = 0;
            first = true;
            curr = 0;

            foreach ( var sum in sums ) {
                if ( first ) {
                    first = false;
                } else {
                    if ( sum > curr ) result2 ++;
                }

                curr = sum;
            }

            // Part 2 alt

            var result3 = 0;
            first = true;
            curr = 0;

            for ( var i = 0; i <= nums.Length - WIN_SIZE; i ++ )
            {
                var num = nums[i];

                if ( first ) {
                    first = false;
                } else {
                    if ( nums[i+2] > curr ) result3 ++;
                }

                curr = num;
            }

            if ( result2 != result3 ) {
                Console.WriteLine( $"ERROR: {result3} does not agree with correct value {result2}" );
            }

            return ( result1, result2 ) ;
        }
    }
}
