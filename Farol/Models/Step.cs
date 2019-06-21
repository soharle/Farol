using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farol.Models
{
    public class Step
    {
        public Class Class { get; set; }
        public List<Class> Stubs { get; set; }
        public List<Class> Releases { get; set; }

        public Step()
        {
            Stubs = new List<Class>();
            Releases = new List<Class>();
        }

        public string getStubs()
        {
            if (Stubs.Count == 0) return "";
            string stubs = "";
            foreach(Class s in Stubs)
            {
                stubs += $"{s.Name}, ";
            }
            stubs = stubs.Trim();
            return stubs.Remove(stubs.Length-1);
        }

        public string getReleases()
        {
            if (Releases.Count == 0) return "";
            string releases = "";
            foreach (Class r in Releases)
            {
                releases += $"{r.Name}, ";
            }
            releases = releases.Trim();
            return releases.Remove(releases.Length-1);
        }
    }
}
