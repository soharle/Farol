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
            LoadInterfaces();
        }

        private void LoadDependency()
        {
            var dependency = from item in _doc.Descendants("ownedMember")
                             where item.Attribute(_xmi + "type").Value == "uml:Dependency"
                             select item;

            foreach (var d in dependency)
            {
                var client = d.Attribute("client").Value;
                var supplier = d.Attribute("supplier").Value;

                var classClient = _model.Classes.Single(x => x.Id == client);
                var classSupplier = _model.Classes.Single(x => x.Id == supplier);

                classClient.ItDepends.Add(classSupplier);
                classSupplier.Releases.Add(classClient);

            }
        }

        private void LoadInterfaces()
        {
            var interfaces = from item in _doc.Descendants("interfaceRealization")
                             where item.Attribute(_xmi + "type").Value == "uml:InterfaceRealization"
                             select item;
            foreach(var i in interfaces)
            {
                var implementing = i.Attribute("implementingClassifier").Value;
                var contract = i.Attribute("contract").Value;

                var classImplementing = _model.Classes.Single(x => x.Id == implementing);
                var classContract = _model.Classes.Single(x => x.Id == contract);

                classImplementing.ItDepends.Add(classContract);
                classContract.Releases.Add(classImplementing);
            }

        }

        private void LoadGeneralizations()
        {
            var generalization = from item in _doc.Descendants("generalization")
                                 where item.Attribute(_xmi + "type").Value == "uml:Generalization"
                                 select item;

            foreach (var g in generalization)
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
            var associations = from item in _doc.Descendants("ownedMember")
                               where item.Attribute(_xmi + "type").Value == "uml:Association"
                               select item;

            foreach (var a in associations)
            {
                var classId = a.Parent.Attribute(_xmi + "id").Value;
                string typeAssociation = a.Descendants("ownedEnd").First().Attribute("aggregation").Value;
                string idDependent = a.Descendants("ownedEnd").ToList()[1].Attribute("type").Value;

                if (typeAssociation == "none") //Association
                {
                    var owner = _model.Classes.Single(x => x.Id == classId);
                    var dependent = _model.Classes.Single(x => x.Id == idDependent);

                    owner.Releases.Add(dependent);
                    owner.ItDepends.Add(dependent);
                    dependent.Releases.Add(owner);
                    dependent.ItDepends.Add(owner);
                }
                else if (typeAssociation == "shared") //Agreggation
                {
                    var owner = _model.Classes.Single(x => x.Id == classId);
                    var dependent = _model.Classes.Single(x => x.Id == idDependent);

                    owner.ItDepends.Add(dependent);
                    dependent.Releases.Add(owner);
                }
                else if (typeAssociation == "composite") //Compositions
                {
                    var owner = _model.Classes.Single(x => x.Id == classId);
                    var dependent = _model.Classes.Single(x => x.Id == idDependent);

                    owner.Releases.Add(dependent);
                    dependent.ItDepends.Add(owner);
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
                          where item.Attribute(_xmi + "type").Value == "uml:Class" ||
                          item.Attribute(_xmi + "type").Value == "uml:Interface"
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
