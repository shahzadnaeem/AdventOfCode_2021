namespace Advent2021
{
    class Day3
    {
        public Day3()
        {
        }

        public bool nthBitSet( uint num, int n )
        {
            return ( num & ( 1 << ( n ) ) ) != 0;
        }

        public int nthBitsSet( uint[] nums, int n )
        {
            return nums.Select( ( num, i ) => {
                return nthBitSet( num, n ) ? 1 : 0;
            }).Sum();
        }

        public int findSingleItem( uint[] parsedInput, int numBits, bool O2 )
        {
            var data = parsedInput.Select( (d, i) => d ).ToArray();
            var numItems = data.Length;

            var result = 0;

            for ( var bitN = numBits - 1; bitN >= 0; bitN -- )
            {
                // Console.WriteLine( $"{(O2 ? "O2" : "CO2")}: bit #{bitN}: #items = {data.Length}" );

                var oneBitCount = nthBitsSet( data, bitN );
                var pickSet = oneBitCount * 2 >= data.Length;

                if ( ! O2 ) pickSet = ! pickSet;

                data = data.Where( ( d ) => {
                    var isSet = nthBitSet( d, bitN );

                    return pickSet ? isSet : ! isSet;
                } ).ToArray();

                if ( data.Length == 1 ) {
                    result = (int) data[0];

                    Console.WriteLine( $"{(O2 ? "O2" : "CO2")} answer at bit #{bitN} = {result}" );

                    break;
                }
            }

            return result;
        }

        public ( int, int ) Answer()
        {
            var inputs = Day3Data.INPUT.Split( '\n' );

            var minBits = 64;
            var maxBits = 0;

            var parsed = inputs.Select( ( d, i ) => {

                if ( d.Length < minBits ) { minBits = d.Length; }
                if ( d.Length > maxBits ) { maxBits = d.Length; }

                var val = Convert.ToUInt32( d, 2 );

                if ( i < 10 ) {
                    // Console.WriteLine( $"#{i} = {d} => {val}" );
                }

                return val;
            }).ToArray();

            if ( minBits != maxBits ) {
                throw new Exception( "Invalid input: minBits ({minBits}) != maxBits ({maxBits})" );
            }

            Console.WriteLine( $"Parsed inputs = {parsed.Length}" );
            Console.WriteLine( $"  Bits per entry: {minBits}" );

            // Part 1
            var gamma = 0u;
            var epsilon = 0u;

            for ( var bitN = minBits - 1; bitN >= 0; bitN -- )
            {
                var oneBitCount = nthBitsSet( parsed, bitN );
                
                // Console.WriteLine( $"Bit#{bitN}: Count = {oneBitCount}" );

                gamma = ( gamma << 1 ) + ( oneBitCount * 2 > parsed.Length ? 1u : 0 );
            }

            epsilon = ~gamma & ( ( 1u << minBits ) - 1 );

            // Console.WriteLine( $"gamma = {Convert.ToString( gamma, 2 )}, epsilon= {Convert.ToString( epsilon, 2 )}"  );

            var result1 = ( (int) gamma, (int) epsilon );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2

            var result2 = ( 0, 0 );

            result2.Item1 = findSingleItem( parsed, minBits, true );
            result2.Item2 = findSingleItem( parsed, minBits, false );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item1 * result1.Item2, result2.Item1 * result2.Item2 );
        }
    }
}
