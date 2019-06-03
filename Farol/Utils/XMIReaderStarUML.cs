using Farol.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Farol.Utils
{
    public class XMIReaderStarUML : IXMIReader
    {
        private Model _model;
        private XElement _doc;
        private XNamespace _xmi;
        private XNamespace _uml;

        public void LoadXmi(XElement doc)
        {
            _doc = doc;
            _model = new Model();
            _xmi = "http://schema.omg.org/spec/XMI/2.1";
            _uml = "http://schema.omg.org/spec/UML/2.0";

            LoadModel();
            LoadClasses();
            LoadAssociations();
            LoadGeneralizations();
            LoadDependency();
        }

        private void LoadDependency()
        {
            var dependency = from item in _doc.Descendants("ownedMember")
                             where item.Attribute(_xmi + "type").Value == "uml:Dependency"
                             select item;

            foreach(var d in dependency)
            {
                var client = d.Attribute("client").Value;
                var supplier = d.Attribute("supplier").Value;

                var classClient = _model.Classes.Single(x => x.Id == client);
                var classSupplier = _model.Classes.Single(x => x.Id == supplier);

                classClient.ItDepends.Add(classSupplier);
                classSupplier.Releases.Add(classClient);

            }
        }

        private void LoadGeneralizations()
        {
            var generalization = from item in _doc.Descendants("generalization")
                                 where item.Attribute(_xmi + "type").Value == "uml:Generalization"
                                 select item;

            foreach(var g in generalization)
            {
                var specific = g.Attribute("specific").Value;
                var general = g.Attribute("general").Value;

                var classSpecific = _model.Classes.Single(x => x.Id == specific);
                var classGeneral = _model.Classes.Single(x => x.Id == general);

                classSpecific.ItDepends.Add(classGeneral);
                classGeneral.Releases.Add(classSpecific);
            }

        }

        private void LoadAssociations()
        {
            foreach (var c in _model.Classes)
            {
                var ids = from item in _doc.Descendants("ownedMember")
                          where item.Parent.Attribute(_xmi + "id").Value == c.Id &&
                          item.Attribute(_xmi + "type").Value == "uml:Association"
                          select item.Elements("ownedEnd").ToList()[1].Attribute("type").Value;

                foreach (var id in ids)
                {
                    var classesWithAssociationWithC = _model.Classes.Where(x => x.Id == id);

                    c.ItDepends.AddRange(classesWithAssociationWithC);
                    c.Releases.AddRange(classesWithAssociationWithC);

                    foreach (var classeAssociation in classesWithAssociationWithC)
                    {
                        classeAssociation.Releases.Add(c);
                        classeAssociation.ItDepends.Add(c);
                    }

                }
            }
        }

        private void LoadModel()
        {
            var modelNode = _doc.Element(_uml + "Model");
            _model.Name = modelNode.Attribute("name").Value;
        }

        private void LoadClasses()
        {
            var classes = from item in _doc.Descendants("packagedElement")
                          where item.Attribute(_xmi + "type").Value == "uml:Class"
                          select item;

            foreach (var node in classes)
            {
                _model.Classes.Add(
                new Class
                {
                    Id = node.Attribute(_xmi + "id").Value,
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
