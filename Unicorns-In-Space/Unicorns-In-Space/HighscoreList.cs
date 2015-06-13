using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class HighscoreList
    {
        readonly string PATH = "HighscoreList.puis"; 
        class Node : IComparable<Node>
        {
            Int64 value;
            string name;

            public Node(Int64 score, string _name)
            {
                value = score;
                name = _name;
            }

            public int CompareTo(Node other)
            {
                return (int)(value - other.value);
            }

            public override string ToString()
            {
                return value.ToString() + " " + name;
            }
        }

        public List<Node> Scores { get; protected set; }

        public HighscoreList()
        {
            if (File.Exists(PATH))
            {
                Load();
            }
            else
            {
                File.Create(PATH);
                Scores = new List<Node>();
            }
        }

        void Load()
        {
            Scores = new List<Node>();
            using (StreamReader r = new StreamReader(PATH))
            {
                string node = r.ReadLine();

                Node item = new Node(Convert.ToInt64(node.Split(' ')[0]), node.Split(' ')[1]);
                Scores.Add(item);
            }
        }

        public void Add(Int64 score, string name)
        {
            Scores.Add(new Node(score, name));
            Scores.Sort();

            if (Scores.Count > 10)
                Scores.RemoveRange(10, Scores.Count - 10);
        }

        public void Save()
        {
            using (StreamWriter w = new StreamWriter(PATH))
            {
                foreach (Node n in Scores)
                    w.WriteLine(n.ToString());
            }
        }

        ~HighscoreList()
        {
            Save();
        }
    }
}
