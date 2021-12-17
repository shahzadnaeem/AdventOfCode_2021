namespace Advent2021
{
    class Day12
    {
        public const string START = "start";
        public const string END = "end";

        public partial class Graph {};

        public class Node
        {
            public const int STD_TRAVERSE_COUNT = 1;
            public const int V2_TRAVERSE_COUNT = 2;

            public string Name { get; private set; } = "";
            public bool MultiPath { get; private set; } = false;
            public int TraverseCount { get; private set; } = STD_TRAVERSE_COUNT;
            public bool Traversed {
                get
                {
                    var traversed = false;

                    if ( TraverseCount > 0 ) {
                        if ( ParentGraph.ExtraTraversalEnabled && IsStdCave() )
                        {
                            if ( !ParentGraph.ExtraTraversalAvailable && TraverseCount < V2_TRAVERSE_COUNT )
                            {
                                traversed = true;
                            }
                        }
                    } else {
                        traversed = true;
                    }

                    return traversed;
                }
            }

            private readonly Graph ParentGraph;

            public Dictionary<string, Node> Connections { get; private set; } = new Dictionary<string, Node>();

            public Node(string name, Graph graph)
            {
                ParentGraph = graph;

                if (name.Length > 0)
                {
                    Name = name;
                    MultiPath = Char.IsUpper(Name.First());

                    if (graph.ExtraTraversalEnabled && IsStdCave() )
                    {
                        TraverseCount = V2_TRAVERSE_COUNT;
                    }
                }
                else
                {
                    throw new Exception($"Invalid Node name '{name}'");
                }
            }

            public override string ToString()
            {
                return $"name: {Name}, multi: {MultiPath}, traversed: {Traversed}";
            }

            public void AddConnection(Node node)
            {
                if (!Connections.ContainsKey(node.Name))
                {
                    Connections.Add(node.Name, node);
                }
            }

            public List<Node> GetTraversableConnections()
            {
                var connections = new List<Node>();

                foreach( var c in Connections ) {
                    if ( ! c.Value.Traversed || c.Value.MultiPath ) {
                        connections.Add( c.Value );
                    }
                }

                return connections;
            }

            private bool IsStdCave()
            {
                return Name != START && Name != END && ! MultiPath;
            }

            public bool ClearTraversed()
            {
                var extraTraversalAvailable = false;

                TraverseCount ++;

                if ( ParentGraph.ExtraTraversalEnabled ) {
                    if ( IsStdCave() && ( TraverseCount == 1 ) ) {
                        extraTraversalAvailable = true;
                    }
                }

                return extraTraversalAvailable;
            }

            public bool SetTraversed()
            {   var extraTraversalAvailable = true;

                if ( TraverseCount > 0 )
                    TraverseCount --;

                if ( ParentGraph.ExtraTraversalEnabled && ParentGraph.ExtraTraversalAvailable ) {
                    if ( IsStdCave() && ( TraverseCount == 0 ) ) {
                        extraTraversalAvailable = false;
                    }
                }

                return extraTraversalAvailable;
            }
        }

        public partial class Graph
        {
            public Dictionary<string, Node> Nodes { get; private set; } = new Dictionary<string, Node>();

            public bool ExtraTraversalEnabled { get; private set; } = false;
            public bool ExtraTraversalAvailable { get; private set; } = false;

            public Graph( bool extraTraversalEnabled )
            {
                ExtraTraversalEnabled = extraTraversalEnabled;

                if ( ExtraTraversalEnabled ) {
                    ExtraTraversalAvailable = true;
                }
            }

            public int NumNodes()
            {
                return Nodes.Count;
            }

            public void AddPair(string pair)
            {
                var nodes = pair.Split('-');
                var (a, b) = (nodes[0], nodes[1]);

                if (!Nodes.ContainsKey(a))
                {
                    var nodeA = new Node(a, this);
                    Nodes.Add(a, nodeA);
                }

                if (!Nodes.ContainsKey(b))
                {
                    var nodeB = new Node(b, this);
                    Nodes.Add(b, nodeB);
                }

                var A = Nodes[a];
                var B = Nodes[b];

                A.AddConnection(B);
                B.AddConnection(A);
            }

            public override string ToString()
            {
                var wr = new StringWriter();

                foreach (var node in Nodes)
                {
                    wr.Write($"{node.Value.Name}: ");

                    wr.WriteLine(string.Join(", ", node.Value.Connections.Values.Select( n => n.Name )));
                }
    
                return wr.ToString();
            }

            private string PathToString( LinkedList<Node> path )
            {
                return string.Join( '-', path.Select( d => d.Name ) );
            }

            private void DoFindPaths( Node from, Node to, LinkedList<Node> path, List<string> paths )
            {
                path.AddLast( from );
                var res = from.SetTraversed();

                if ( ExtraTraversalEnabled && ! res ) {
                    ExtraTraversalAvailable = false;
                }

                if ( from.Name == to.Name ) {
                    paths.Add( PathToString( path ) );                 
                } else {
                    var searchNodes = from.GetTraversableConnections();

                    foreach (var next in searchNodes)
                    {
                        DoFindPaths( next, to, path, paths );
                    }
                }

                res = from.ClearTraversed();

                if ( ExtraTraversalEnabled && res ) {
                    ExtraTraversalAvailable = true;
                }

                path.RemoveLast();
            }

            public List<string> FindPaths( string from, string to )
            {
                var paths = new List<string>();
                var path = new LinkedList<Node>();

                var fromNode = Nodes.First( kv => kv.Key == from ).Value;
                var toNode = Nodes.First( kv => kv.Key == to ).Value;

                DoFindPaths( fromNode, toNode, path, paths );

                return paths;
            }
        }

        private Graph GetModel( bool extraTraversalEnabled = false )
        {
            var graph = new Graph( extraTraversalEnabled );

            var input = Day12Data.INPUT.Split( Environment.NewLine );

            foreach ( var line in input ) {
                graph.AddPair( line );
            }

            return graph;
        }

        public ( long, long ) Answer()
        {
            var input = GetModel();

            // Console.WriteLine( $"\nINPUT\n\n{input}\n" );

            var paths = input.FindPaths( START, END );

            // paths.Sort();
            // Console.WriteLine( $"paths:\n{Utils.ArrayToString(paths.ToArray())}\n" );

            // Part 1
            var result1 = ( input.NumNodes(), paths.Count );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            input = GetModel( true );

            paths = input.FindPaths( START, END );

            // paths.Sort();
            // Console.WriteLine( $"V2 paths:\n{Utils.ArrayToString(paths.ToArray())}\n" );

            var result2 = ( input.NumNodes(), paths.Count );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
