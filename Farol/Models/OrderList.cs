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
            for(int i = 0; i < classes.Count; i++)
            {
                Step step = new Step();

                step.Class = classes[i];
                step.Stubs.AddRange(classes[i].ItDepends);
                step.Releases.AddRange(classes[i].Releases);

                foreach(Class d in classes[i].Releases)
                {
                    d.ItDepends.Remove(d);
                }

                classes.Sort();
                Order.Add(step);
            }
        }

    }
}
