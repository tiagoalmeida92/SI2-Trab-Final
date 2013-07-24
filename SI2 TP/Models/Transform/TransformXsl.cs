using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace SI2_TP.Models.Transform
{
    public class TransformXsl
    {
        //CreateNavigator para navegar no documento
        public static string ToHtml(string xmlString, string xslPath)
        {
            using (var sw = new StringWriter())
            {
                var xml = new XmlDocument();
                xml.LoadXml(xmlString);

                var xsl2 = new XslCompiledTransform();
                xsl2.Load(xslPath);
                xsl2.Transform(xml.CreateNavigator(), new XsltArgumentList(),sw);
                return sw.ToString();
            }
        }
    }
}