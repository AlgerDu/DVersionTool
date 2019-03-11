using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace D.Tool.Version.Core
{
    public abstract class XmlProjectFileTmplate
    {
        protected ILogger _logger;

        XmlDocument _xml;

        public XmlProjectFileTmplate(
            ILogger logger
            )
        {
            _logger = logger;
        }

        protected void LoadFile(string path)
        {
            _xml = new XmlDocument();
            _xml.Load(path);
        }

        protected void Save(string path)
        {
            _xml?.Save(path);
        }

        protected void SetNodeTxt(string nodePath, string value)
        {
            XmlNodeList nodes = _xml.SelectNodes(nodePath);

            if (nodes.Count != 1)
            {
                var node = makeXPath(_xml, _xml, nodePath);
                node.InnerText = value;
            }
            else
            {
                XmlNode node = nodes.Item(0);

                node.InnerText = value;
            }
        }



        private XmlNode makeXPath(XmlDocument doc, XmlNode parent, string xpath)
        {
            // grab the next node name in the xpath; or return parent if empty
            string[] partsOfXPath = xpath.Trim('/').Split('/');
            string nextNodeInXPath = partsOfXPath.First();
            if (string.IsNullOrEmpty(nextNodeInXPath))
                return parent;

            // get or create the node from the name
            XmlNode node = parent.SelectSingleNode(nextNodeInXPath);
            if (node == null)
                node = parent.AppendChild(doc.CreateElement(nextNodeInXPath));

            // rejoin the remainder of the array as an xpath expression and recurse
            string rest = String.Join("/", partsOfXPath.Skip(1).ToArray());
            return makeXPath(doc, node, rest);
        }
    }
}
