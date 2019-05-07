using Farol.Utils;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace XmlReaderTest
{
    public class ReaderOneClass
    {
        public IXMIReader GetReaderStarUML_LoadedWith_1Classes()
        {
            var reader = new XMIReaderStarUML();
            string workingDirectory = Path.GetFullPath(@"..\..\..\");
            string file = "xmi/one-class.xmi";
            string path = Path.Combine(workingDirectory + file);
            XElement doc = XElement.Load($"{path}");
            reader.LoadXmi(doc);
            return reader;
        }

        [Fact]
        public void MustRead_Name_Class_A()
        {
            var reader = GetReaderStarUML_LoadedWith_1Classes();
            var model = reader.GetModel();
            Assert.Equal("A", model.Classes[0].Name);
        }

        [Fact]
        public void MustRead_Id_Class_A()
        {
            var reader = GetReaderStarUML_LoadedWith_1Classes();
            var model = reader.GetModel();
            Assert.Equal("AAAAAAFqfzSoEVy/JIk=", model.Classes[0].Id);
        }

        [Fact]
        public void MustRead_Name_Model()
        {
            var reader = GetReaderStarUML_LoadedWith_1Classes();
            var model = reader.GetModel();
            Assert.Equal("RootModel", model.Name);
        }

        [Fact]
        public void MustRead_Count_1_Classes()
        {
            var reader = GetReaderStarUML_LoadedWith_1Classes();
            var model = reader.GetModel();
            Assert.Single(model.Classes);
        }
    }
}
