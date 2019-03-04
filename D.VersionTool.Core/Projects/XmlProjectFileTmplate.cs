using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

            }
            else
            {
                XmlNode node = nodes.Item(0);

                node.InnerText = value;
            }
        }
    }
}
