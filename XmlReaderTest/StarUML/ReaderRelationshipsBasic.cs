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
        [Fact]
        public void MustRead_ClassAa_Release_Ba()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassB = model.Classes.Single(c => c.Name == "Ba");
            Class ClassA = model.Classes.Single(c => c.Name == "Aa");
            Assert.Contains(ClassB, ClassA.Releases);
        }
        [Fact]
        public void MustRead_ClassBa_Release_Aa()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassB = model.Classes.Single(c => c.Name == "Ba");
            Class ClassA = model.Classes.Single(c => c.Name == "Aa");
            Assert.Contains(ClassA, ClassB.Releases);
        }
        [Fact]
        public void MustRead_ClassAd_Depends_Bd()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ad");
            Class ClassB = model.Classes.Single(c => c.Name == "Bd");
            Assert.Contains(ClassB, ClassA.ItDepends);
        }
        [Fact]
        public void MustRead_ClassAd_NotDepends_Bd()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ad");
            Class ClassB = model.Classes.Single(c => c.Name == "Bd");
            Assert.DoesNotContain(ClassA, ClassB.ItDepends);
        }

        [Fact]
        public void MustRead_ClassBd_Releases_Ad()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ad");
            Class ClassB = model.Classes.Single(c => c.Name == "Bd");
            Assert.Contains(ClassA, ClassB.Releases);

        }
        [Fact]
        public void MustRead_ClassAg_Depends_Bg()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ag");
            Class ClassB = model.Classes.Single(c => c.Name == "Bg");
            Assert.Contains(ClassB, ClassA.ItDepends);
        }
        [Fact]
        public void MustRead_ClassAag_Depends_Bag()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Aag");
            Class ClassB = model.Classes.Single(c => c.Name == "Bag");
            Assert.Contains(ClassB, ClassA.ItDepends);

        }
        [Fact]
        public void MustRead_ClassBag_Releases_Aag()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Aag");
            Class ClassB = model.Classes.Single(c => c.Name == "Bag");
            Assert.Contains(ClassA, ClassB.Releases);

        }
        [Fact]
        public void MustRead_ClassBc_Depends_Ac()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ac");
            Class ClassB = model.Classes.Single(c => c.Name == "Bc");
            Assert.Contains(ClassA, ClassB.ItDepends);

        }
        [Fact]
        public void MustRead_ClassAc_Releases_Bc()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ac");
            Class ClassB = model.Classes.Single(c => c.Name == "Bc");
            Assert.Contains(ClassB, ClassA.Releases);

        }
        [Fact]
        public void MustRead_ClassBg_Releases_Ag()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ag");
            Class ClassB = model.Classes.Single(c => c.Name == "Bg");
            Assert.Contains(ClassA, ClassB.Releases);

        }
        [Fact]
        public void MustRead_ClassAi_Depends_Bi()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ai");
            Class ClassB = model.Classes.Single(c => c.Name == "Bi");
            Assert.Contains(ClassB, ClassA.ItDepends);
        }

        [Fact]
        public void MustRead_ClassBi_Releases_Ai()
        {
            IXMIReader reader = GetReaderStarUML_LoadedWith_RelationshipsBasic();
            Model model = reader.GetModel();
            Class ClassA = model.Classes.Single(c => c.Name == "Ai");
            Class ClassB = model.Classes.Single(c => c.Name == "Bi");
            Assert.Contains(ClassA, ClassB.Releases);
        }

    }
}
