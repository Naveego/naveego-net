using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naveego.Sync;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Naveego.Json
{
    
    public class SerializationTests
    {

        [TestFixture]
        public class Deserializing_SyncClient
        {

            [Test]
            public void Should_Succeed()
            {
                string json = File.ReadAllText("SyncClient.json");
                var serializer = JsonSerializerFactory.CreateSerializer();

                using (var jr = new JsonTextReader(new StringReader(json)))
                {
                    var result = serializer.Deserialize<SyncClient>(jr);
                    Assert.That(result, Is.Not.Null);
                }
            }

            [Test]
            public void Should_Include_Metadata()
            {
                string json = File.ReadAllText("SyncClient.json");
                var serializer = JsonSerializerFactory.CreateSerializer();

                using (var jr = new JsonTextReader(new StringReader(json)))
                {
                    var result = serializer.Deserialize<SyncClient>(jr);
                    Assert.That(result.Metadata, Is.Not.Null);
                }
            }
        }

    }
}
