using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naveego.DataQuality;
using Naveego.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Naveego.Sync
{
    public class SyncStreamEventJsonConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SyncStreamEvent);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var json = JObject.Load(reader);
            var syncEvent = new SyncStreamEvent();
            syncEvent.Id = (long)json["id"];
            syncEvent.Timestamp = (DateTime)json["ts"];
            syncEvent.Action = (string)json["action"];
            syncEvent.ContentType = (string)json["contentType"];
            syncEvent.Key = Guid.Parse((string)json["key"]);


            Type type = null;
            switch (syncEvent.ContentType)
            {
                case "sync.client":
                    type = typeof(SyncClient);
                    break;
                case "connection":
                    type = typeof(Connection);
                    break;
                case "dataquality.rule":
                    type = typeof(Rule);
                    break;
                case "dataquality.query":
                    type = typeof(Query);
                    break;
                case "dataquality.queuedrun":
                    type = typeof(QueuedRun);
                    break;
                case "sync.writeback":
                    type = typeof(WriteBack);
                    break;
                case "dataquality.dimension":
                    type = typeof(Dimension);
                    break;
                case "sync.client.reset":
                    type = typeof(ResetClientMessage);
                    break;
            }

            if (json["content"] != null && json["content"].Type == JTokenType.Object)
            { 
                syncEvent.Content = ((JObject)json["content"]).ToObject(type, serializer);
            }

            return syncEvent;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

    }
}
