using Farol.Models;
using Farol.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace XmlReaderTest.StarUML
{
    public class ReaderRelationshipsBasic
    {
        public IXMIReader GetReaderStarUML_LoadedWith_RelationshipsBasic()
        {
            var reader = new XMIReaderStarUML();
            string workingDirectory = Path.GetFullPath(@"..\..\..\");
            string file = "xmi/relationships-basic.xmi";
            string path = Path.Combine(workingDirectory + file);
            XElement doc = XElement.Load($"{path}");
            reader.LoadXmi(doc);
            return reader;
        }

        [Fact]
        public void MustRead_ClassAa_Depends_Ba()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassB = model.Classes.Single(c => c.Name == "Ba");
            Class ClassA = model.Classes.Single(c => c.Name == "Aa");
            Assert.Contains(ClassB, ClassA.ItDepends);
        }
        [Fact]
        public void MustRead_ClassBa_Depends_Aa()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassB = model.Classes.Single(c => c.Name == "Ba");
            Class ClassA = model.Classes.Single(c => c.Name == "Aa");
            Assert.Contains(ClassA, ClassB.ItDepends);
        }

        public void MustRead_ClassAa_Release_Ba()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassB = model.Classes.Single(c => c.Name == "Ba");
            Class ClassA = model.Classes.Single(c => c.Name == "Aa");
            Assert.Contains(ClassA, ClassA.Releases);
        }
        public void MustRead_ClassBa_Release_Aa()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassB = model.Classes.Single(c => c.Name == "Ba");
            Class ClassA = model.Classes.Single(c => c.Name == "Aa");
            Assert.Contains(ClassA, ClassB.Releases);
        }
    }
}
