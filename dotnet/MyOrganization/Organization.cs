using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            // Hashset to keep track of visited nodes while traversing tree
            var visited = new HashSet<Position>();
            int id = 0;
            dfs_traversal(root, visited, title, person, id);

            return null;
        }

        //Implementing recursive solution to traverse tree structure in a depth first search manner
        //and populate information
        public void dfs_traversal(Position root, HashSet<Position> visited, string title, Name person, int id)
        {
            visited.Add(root);
            id++; // id is incremented based on hierarchy
                  // (employees under same boss have similar id with 1 - highest level and 4 - lowest level)

            var reporteees = root.GetDirectReports();
            if (title == root.GetTitle())
            {
                root.SetEmployee(new Employee(id, person));
            }
            foreach (var reportee in reporteees)
            {
                if (!visited.Contains(reportee))
                {
                    // Setting current reportee to root during call
                    dfs_traversal(reportee, visited, title, person, id);
                }
            }
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
