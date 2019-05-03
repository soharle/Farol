using Farol.Models;
using System.Xml.Linq;

namespace Farol.Utils
{
    public interface IXMIReader
    {
        void LoadXmi(XElement doc);
        Model GetModel();
    }
}
