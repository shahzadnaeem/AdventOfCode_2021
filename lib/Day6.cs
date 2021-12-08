namespace Advent2021
{
    class Day6
    {
        public class LanternFish
        {
            public const int NEW_FISH_SPAWN_DAYS = 8;
            public const int RESPAWN_DAYS = 6;

            public int DaysToSpawn { get; set; } = NEW_FISH_SPAWN_DAYS;

            public LanternFish( int daysToSpawn = NEW_FISH_SPAWN_DAYS )
            {
                DaysToSpawn = daysToSpawn;
            }

            public LanternFish? NewDay() {

                LanternFish? result = null;

                if ( DaysToSpawn == 0 ) {
                    result = new LanternFish();
                    DaysToSpawn = 6;

                } else {
                    DaysToSpawn --;
                }

                return result;
            }
        }

        public class Tank
        {
            public const int MAX_DAYS = 8;

            private long[] Fishes = new long[MAX_DAYS + 1];

            public Tank()
            {
            }

            public void Init( List<LanternFish> fishes )
            {
                foreach ( var fish in fishes ) {
                    Fishes[fish.DaysToSpawn] ++;
                }
            }

            public override string ToString()
            {
                var wr = new StringWriter();

                for ( var i = 0; i <= MAX_DAYS; i ++ ) {
                    wr.Write( $"{i}: {Fishes[i]}" );

                    if ( i != MAX_DAYS ) {
                        wr.Write( ", " );
                    }
                }

                wr.WriteLine();

                return wr.ToString();
            }

            public long NumFishes()
            {
                return Fishes.Aggregate( 0L, ( long acc, long val ) => acc + val );
            }

            public void IterateDays( int days )
            {
                for ( var day = 0; day < days; day ++ ) {
                    long newFishes = Fishes[0];

                    for (var i = 0; i < MAX_DAYS; i++)
                    {
                        Fishes[i] = Fishes[i + 1];
                    }

                    Fishes[LanternFish.NEW_FISH_SPAWN_DAYS] = newFishes;
                    Fishes[LanternFish.RESPAWN_DAYS] += newFishes;
                }
            }
        }

        public Day6()
        {
        }

        public List<LanternFish> IterateDays( List<LanternFish> fishes, int days )
        {
            var ourFishes = fishes.Select( x => x ).ToList();

            for ( int i = 0; i < days; i ++ ) {
                var extraFishes = new List<LanternFish>();

                foreach ( var fish in ourFishes ) {
                    var spawnResult = fish.NewDay();

                    if ( spawnResult != null ) {
                        extraFishes.Add( spawnResult );
                    }
                }

                ourFishes.AddRange( extraFishes );
            }

            return ourFishes;
        }

        public void ShowFishes( List<LanternFish> fishes )
        {
            var numFishes = fishes.Count();
            var i = 0;

            Console.Write( $"#Fishes = {numFishes}: " );

            fishes.ForEach( f => {
                i++;
                Console.Write( f.DaysToSpawn );
                if ( i < numFishes ) {
                    Console.Write(',');
                }
            });

            Console.WriteLine();
        }

        public List<LanternFish> GetFishes( string[] data )
        {
            var parsed = data.Select( ( d, i ) => {

                // Do whatever parsing is needed here
                return new LanternFish( Convert.ToInt32( d ) );

            }).ToArray();

            Console.WriteLine( $"Parsed inputs = {parsed.Length}" );

            var fishes = new List<LanternFish>();

            fishes.AddRange( parsed );

            return fishes;
        }

        public ( int, long ) Answer()
        {
            var inputs = Day6Data.INPUT.Split( ',' );

            var fishes = GetFishes( inputs );

            // Part 1
            ShowFishes( fishes );

            var newFishes = IterateDays( fishes, 80 );

            // ShowFishes( newFishes );

            //
            var result1 = ( 80, newFishes.Count );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            var tank = new Tank();

            fishes = GetFishes( inputs );

            tank.Init( fishes );

            Console.WriteLine( $"{tank}" );

            Console.WriteLine( $"#Fishes in tank = {tank.NumFishes()}" );

            tank.IterateDays( 256 );

            var result2 = ( 256, tank.NumFishes() );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
