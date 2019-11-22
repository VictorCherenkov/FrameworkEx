using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace FrameworkEx
{
    public static class DataContractSerializationUtils
    {
        public static string Serialize(this object toSerialize)
        {
            using (var stream = new MemoryStream())
            {
                new DataContractSerializer(toSerialize.GetType()).WriteObject(stream, toSerialize);
                stream.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        public static T Deserialize<T>(this string toDeserialize)
        {
            using (var textReader = new XmlTextReader(new StringReader(toDeserialize)))
            {
                return (T)new DataContractSerializer(typeof(T)).ReadObject(textReader);
            }
        }
    }
}