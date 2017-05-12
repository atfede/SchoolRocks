using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace ImageSource.Concrete
{
    public class SerializerImp : ISerializer
    {
        public string ToJson<T>(T source)
        {
            //TODO: Implement a Serialize method from Object to JSON string.
            string json = JsonConvert.SerializeObject(source, Formatting.Indented);
            return json;

        }

        public T ToObject<T>(string source)
        {
            //TODO: Implement a Deserialize method from JSON string to Object.
            T photo = JsonConvert.DeserializeObject<T>(source);
            return photo;
        }
    }
}
