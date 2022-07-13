using System.Xml.Serialization;

namespace Maplestory_Patch_Logs.Views.Actions
{
    public class Serializer<T>
    {
        public string SerializeToString(T obj, string xmlns = "")
        {
            string result = string.Empty;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, xmlns);
            System.IO.StringWriter writer = new System.IO.StringWriter();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            serializer.Serialize(writer, obj, ns);
            result = writer.ToString();

            return result;
        }
        /*
        public T Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(xml))
            {
                var Xml = (T)serializer.Deserialize(reader);
            }

            return Xml;
        }
        */
    }
}
