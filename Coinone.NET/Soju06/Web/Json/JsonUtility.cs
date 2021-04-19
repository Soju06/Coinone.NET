/* ========= Soju06 Web Json Utility =========
 * NAMESPACE: Soju06.Web.Json
 * LICENSE: MIT
 * Copyright by Soju06
 * ========= Soju06 Web Json Utility ========= */
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Soju06.Web.Json {
    public class JsonUtility {
        public static string Convert(object obj) {
            var serializer = new DataContractJsonSerializer(obj?.GetType());
            using (var ms = new MemoryStream()) {
                serializer.WriteObject(ms, obj);
                var json = Encoding.UTF8.GetString(ms.ToArray());
                return json;
            }
        }

        public static T Convert<T>(string json) => (T)Convert(json, typeof(T));

        public static object Convert(string json, Type type) {
            var serializer = new DataContractJsonSerializer(type);
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                return serializer.ReadObject(ms);
        }

        public static XDocument CreateReaderDocument(string json) {
            var doc = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max));
            var elms = doc.Descendants().Where(d => d.Attribute("type").Value == "null").ToList();
            foreach (var elm in elms) elm.ReplaceWith(new XElement(elm.Name, null, elm.FirstAttribute));
            Console.WriteLine(doc.ToString());
            return doc;
        }

        public static XDocument CreateDocument(string type = "object") {
            var d = new XDocument();
            d.Add(new XElement("root", new XAttribute("type", type)));
            return d;
        }

        public static XElement CreateElement(string name, object obj = null, string type = "object") =>
            new XElement(name, new XAttribute("type", type)) { Value = obj.ToString() };

        public static string CreateWriterString(XDocument document) {
            using (var ms = new MemoryStream()) {
                if(document.Root is not null) {
                    var t = document.Root.Attribute("type");
                    if (t is null) document.Root.Add(new XAttribute("type", "object"));
                    else t.Value = "object";
                }
                var elms = document.Descendants().Where(d => d.Attribute("type") != null 
                        && d.Attribute("type").Value == "null").ToList();
                foreach (var elm in elms) elm.ReplaceWith(new XElement(elm.Name, 
                    null, elm.FirstAttribute));
                var r = JsonReaderWriterFactory.CreateJsonWriter(ms);
                Console.WriteLine(document.ToString());
                document.WriteTo(r);
                r.Flush();
                Console.WriteLine(document.ToString());
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                    return sr.ReadToEnd();
            }
        }
    }
}
