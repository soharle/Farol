using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farol.Models
{
    public class OrderList
    {
        public List<Step> Order { get; set; }
        public Model Model { get; private set; }
        
        public OrderList( Model model)
        {
            Order = new List<Step>();
            Model = model;
            CalculateOrder();
        }

        private void CalculateOrder()
        {
            List<Class> classes = Model.Classes;

            classes.Sort();
            while(classes.Count > 0)
            {
                Step step = new Step();

                step.Class = classes.First();
                step.Stubs.AddRange(step.Class.ItDepends);
                step.Releases.AddRange(step.Class.Releases);

                foreach(Class d in step.Class.Releases)
                {
                    d.ItDepends.Remove(step.Class);
                }

                classes.Remove(step.Class);
                classes.Sort();
                Order.Add(step);
            }
        }

    }
}
