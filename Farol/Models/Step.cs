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

    }
}
