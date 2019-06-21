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
            foreach(Class c in classes)
            {
                Step step = new Step();

                step.Class = c;
                step.Stubs.AddRange(c.ItDepends);
                step.Releases.AddRange(c.Releases);

                foreach(Class d in c.Releases)
                {
                    d.ItDepends.Remove(c);
                }

                classes.Sort();
                Order.Add(step);

            }
        }
    }
}
