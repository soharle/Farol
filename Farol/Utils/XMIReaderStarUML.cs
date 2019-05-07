using Farol.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Farol.Utils
{
    public class XMIReaderStarUML : IXMIReader
    {
        private Model _model;

        public void LoadXmi(XElement doc)
        {
            _model = new Model();

            XNamespace xmi = "http://schema.omg.org/spec/XMI/2.1";
            XNamespace uml = "http://schema.omg.org/spec/UML/2.0";
            var classes = from item in doc.Descendants("packagedElement")
                          where item.Attribute(xmi + "type").Value == "uml:Class"
                          select item;
            var modelNode = doc.Element(uml + "Model");
            _model.Name = modelNode.Attribute("name").Value;

            foreach (var node in classes)
            {
                _model.Classes.Add(
                new Class
                {
                    Id = node.Attribute(xmi + "id").Value,
                    Name = node.Attribute("name").Value,
                }
                );
            }
        }


        public Model GetModel()
        {
            return _model;
        }


    }
}
