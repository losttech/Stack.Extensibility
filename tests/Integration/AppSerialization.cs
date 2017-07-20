namespace LostTech.Stack.Extensibility
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using LostTech.Stack.Extensibility.Filters;
    using LostTech.Stack.Extensibility.Metadata;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AppSerialization
    {
        static readonly App App = new App {
            Windows = {
                new Window {
                    Filters = {
                        new WindowFilter(),
                    },
                    Layout = new WindowLayout {
                        MinWidth = 200,
                        CustomBorders = true,
                        Margin = new Borders {
                            Left = 1,
                        }
                    },
                },

                new Window {
                    Filters = {
                        new WindowFilter{ProcessFilter = new CommonStringMatchFilter{Match = CommonStringMatchFilter.MatchOption.Suffix, Value = "42"}},
                    },
                    Layout = new WindowLayout {
                        MinHeight = 42,
                    }
                }
            },
        };

        const string SampleAppXml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<App>
    <UnusedMetadata/>
    <Window>
        <Filter />
        <Layout MinWidth = ""200"" CustomBorders=""true"">
            <Margin Left = ""1"" />
        </Layout>
    </Window>

    <Window>
        <Filter>
            <ProcessFilter Value=""42"" Match=""Suffix"" />
        </Filter>
        <Layout MinHeight = ""42"" />
        <Filter>
            <ClassFilter Value=""42"" Match=""Suffix"" />
        </Filter>
    </Window>
</App>";
        static readonly XmlWriterSettings WriterSettings = new XmlWriterSettings {
            Indent = true,
            IndentChars = "  ",
        };
        static readonly XmlSerializer Serializer = new XmlSerializer(typeof(App));

        [TestMethod]
        public void SerializesCorrectly() {
            var resultBuilder = new StringBuilder();
            var writer = XmlWriter.Create(resultBuilder, WriterSettings);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Serializer.Serialize(writer, App, ns);
            string result = resultBuilder.ToString();
            const string expected = @"<?xml version=""1.0"" encoding=""utf-16""?>
<App>
  <Window>
    <Filter />
    <Layout MinWidth=""200"" CustomBorders=""true"">
      <Margin Left=""1"" />
    </Layout>
  </Window>
  <Window>
    <Filter>
      <ProcessFilter Value=""42"" Match=""Suffix"" />
    </Filter>
    <Layout MinHeight=""42"" />
  </Window>
</App>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DeserializesCorrectly() {
            var textReader = new StringReader(SampleAppXml);
            var xmlReader = XmlReader.Create(textReader);
            var app = (App)Serializer.Deserialize(xmlReader);
            Assert.AreEqual(2, app.Windows.Count);
            Assert.AreEqual(2, app.Windows[1].Filters.Count);
        }
    }
}
