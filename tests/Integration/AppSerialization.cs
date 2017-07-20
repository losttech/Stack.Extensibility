namespace LostTech.Stack.Extensibility
{
    using System;
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
                        },
                        HorizontalExpansion = 32,
                    },
                    Categories = { "Office", "Main" },
                },

                new Window {
                    Filters = {
                        new WindowFilter{ProcessFilter = new CommonStringMatchFilter{Match = CommonStringMatchFilter.MatchOption.Suffix, Value = "42"}},
                    },
                    Roles = { "Main" },
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
        <Layout MinWidth = ""200"" CustomBorders=""true"" VerticalExpansion=""32"">
            <Margin Left = ""1"" />
        </Layout>
        <Role>Role!</Role>
    </Window>

    <Window>
        <Category>12</Category>
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
    <Layout MinWidth=""200"" CustomBorders=""true"" HorizontalExpansion=""32"">
      <Margin Left=""1"" />
    </Layout>
    <Category>Office</Category>
    <Category>Main</Category>
  </Window>
  <Window>
    <Filter>
      <ProcessFilter Value=""42"" Match=""Suffix"" />
    </Filter>
    <Layout MinHeight=""42"" />
    <Role>Main</Role>
  </Window>
</App>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DeserializesCorrectly() {
            var textReader = new StringReader(SampleAppXml);
            var xmlReader = XmlReader.Create(textReader);
            var app = (App)Serializer.Deserialize(xmlReader);
            Assert.AreEqual(32, app.Windows[0].Layout.VerticalExpansion);
            Assert.AreEqual(1, app.Windows[1].Categories.Count);
            Assert.AreEqual("Role!", app.Windows[0].Roles[0]);
            Assert.AreEqual(2, app.Windows.Count);
            Assert.AreEqual(2, app.Windows[1].Filters.Count);
        }
    }
}
