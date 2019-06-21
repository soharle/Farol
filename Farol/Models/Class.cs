using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farol.Models
{
    public class Class : IComparable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Class> Releases { get; set; }
        public List<Class> ItDepends { get; set; }

        public Class()
        {
            Releases = new List<Class>();
            ItDepends = new List<Class>();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Class comparePart = obj as Class;

            int difference = ItDepends.Count - comparePart.ItDepends.Count;

            if (difference == 0)
            {
                return comparePart.Releases.Count - Releases.Count;
            }
            else
            {
                return difference;
            }


        }
    }
}
