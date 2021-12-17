namespace Advent2021
{
    class Day12
    {
        public const string START = "start";
        public const string END = "end";

        public class Node
        {
            public const int STD_TRAVERSE_COUNT = 1;
            public const int V2_TRAVERSE_COUNT = 2;

            public string Name { get; private set; } = "";
            public bool MultiPath { get; private set; } = false;
            public bool V2TraverseCount { get; private set; } = false;
            public int TraverseCount { get; private set; } = STD_TRAVERSE_COUNT;
            public bool Traversed { get { return TraverseCount == 0; } }

            public Dictionary<string, Node> Connections { get; private set; } = new Dictionary<string, Node>();

            public Node(string name)
            {
                if (name.Length > 0)
                {
                    Name = name;
                    MultiPath = Char.IsUpper(Name.First());
                    V2TraverseCount = false;
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

            public void ClearTraversed()
            {
                TraverseCount ++;
            }

            public void SetTraversed()
            {
                TraverseCount --;
            }

            public void UseV2TraverseCount()
            {
                if ( ! MultiPath && Name != START && Name != END ) {
                    V2TraverseCount = true;
                    TraverseCount = V2_TRAVERSE_COUNT;
                }
            }
        }

        public class Graph
        {
            public Dictionary<string, Node> Nodes { get; private set; } = new Dictionary<string, Node>();

            public Graph()
            {
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
                    var nodeA = new Node(a);
                    Nodes.Add(a, nodeA);
                }

                if (!Nodes.ContainsKey(b))
                {
                    var nodeB = new Node(b);
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

            public void UseV2TraverseCount()
            {
                foreach (var node in Nodes)
                {
                    node.Value.UseV2TraverseCount();
                }
            }

            private void DoFindPaths( Node from, Node to, LinkedList<Node> path, List<string> paths )
            {
                path.AddLast( from );
                from.SetTraversed();

                if ( from.Name == to.Name ) {
                    paths.Add( PathToString( path ) );                 
                } else {
                    var searchNodes = from.GetTraversableConnections();

                    foreach (var next in searchNodes)
                    {
                        DoFindPaths( next, to, path, paths );
                    }
                }

                from.ClearTraversed();
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

        private Graph GetModel()
        {
            var graph = new Graph();

            var input = Day12Data.INPUT.Split( Environment.NewLine );

            foreach ( var line in input ) {
                graph.AddPair( line );
            }

            return graph;
        }

        public ( long, long ) Answer()
        {
            var input = GetModel();

            Console.WriteLine( $"\nINPUT\n\n{input}\n" );

            var paths = input.FindPaths( START, END );

            Console.WriteLine( $"paths:\n{Utils.ArrayToString(paths.ToArray())}\n" );

            // Part 1
            var result1 = ( input.NumNodes(), paths.Count );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            input = GetModel();
            input.UseV2TraverseCount();

            paths = input.FindPaths( START, END );

            Console.WriteLine( $"V2 paths:\n{Utils.ArrayToString(paths.ToArray())}\n" );

            var result2 = ( input.NumNodes(), paths.Count );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
