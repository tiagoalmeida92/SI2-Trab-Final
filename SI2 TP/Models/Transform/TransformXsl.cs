using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace SI2_TP.Models.Transform
{
    public class TransformXsl
    {
        //CreateNavigator para navegar no documento
        public static string ToHtml(XmlReader reader, string xslPath)
        {
            using (var sw = new StringWriter())
            {
                var xml = new XmlDocument();
                xml.Load(reader);

                var xsl2 = new XslCompiledTransform();
                xsl2.Load(xslPath);
                xsl2.Transform(xml.CreateNavigator(), new XsltArgumentList(),sw);
                return sw.ToString();
            }
        }
    }
}