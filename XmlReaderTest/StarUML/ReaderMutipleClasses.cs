using Farol.Models;
using Farol.Utils;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace XmlReaderTest
{
    public class ReaderMutipleClasses
    {
        public IXMIReader GetReaderStarUML_LoadedWith_2Classes()
        {
            var reader = new XMIReaderStarUML();
            string workingDirectory = Path.GetFullPath(@"..\..\..\");
            string file = "xmi/two-classes.xmi";
            string path = Path.Combine(workingDirectory + file);
            XElement doc = XElement.Load($"{path}");
            reader.LoadXmi(doc);
            return reader;
        }

        [Fact]
        public void MustRead_Count_2_Classes()
        {
            var reader = GetReaderStarUML_LoadedWith_2Classes();
            var model = reader.GetModel();
            Assert.Equal(2, model.Classes.Count);
        }

        [Fact]
        public void MustRead_Name_Class_B()
        {
            var reader = GetReaderStarUML_LoadedWith_2Classes();
            var model = reader.GetModel();
            Assert.Equal("B", model.Classes[1].Name);
        }

        [Fact]
        public void MustRead_Id_Class_B()
        {
            var reader = GetReaderStarUML_LoadedWith_2Classes();
            var model = reader.GetModel();
            Assert.Equal("AAAAAAFqkDXt/dTx3oU=", model.Classes[1].Id);
        }

    }
}