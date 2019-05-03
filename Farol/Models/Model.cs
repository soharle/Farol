using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farol.Models
{
    public class Model
    {
        public string Name { get; set; }
        public List<Class> Classes { get; set; }

       public Model()
        {
            Classes = new List<Class>();
        }

    }
}
