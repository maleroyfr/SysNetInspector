using System.IO;
using System.Xml.Serialization;

public static class XmlExporter
{
    public static void ExportToXml<T>(T data, string filename)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var writer = new StreamWriter(filename))
        {
            serializer.Serialize(writer, data);
        }
    }
}
