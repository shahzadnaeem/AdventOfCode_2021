namespace Advent2021
{
    class Day12
    {
        public const string START = "start";
        public const string END = "end";

        public class Node
        {
            public string Name { get; private set; } = "";
            public bool MultiPath { get; private set; } = false;
            public bool Traversed { get; private set; } = false;
            public Dictionary<string, Node> Connections { get; private set; } = new Dictionary<string, Node>();

            public Node(string name)
            {
                if (name.Length > 0)
                {
                    Name = name;
                    MultiPath = Char.IsUpper(Name.First());
                    Traversed = false;
                }
                else
                {
                    throw new Exception($"Invalid Node name '{name}'");
                }
            }

            public void AddConnection(Node node)
            {
                if (!Connections.ContainsKey(node.Name))
                {
                    Connections.Add(node.Name, node);
                }
            }
        }

        public class Graph
        {
            public Dictionary<string, Node> Nodes { get; private set; } = new Dictionary<string, Node>();

            public Graph()
            {
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

            // Part 1
            var result1 = ( 0, 0 );

            Console.WriteLine( $"Result1 = {result1}" );

            // Part 2
            var result2 = ( 0, 0 );

            Console.WriteLine( $"Result2 = {result2}" );

            // Final result in a tuple
            return ( result1.Item2, result2.Item2 );
        }
    }
}
