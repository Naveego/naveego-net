using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naveego.DataQuality;
using Naveego.Json;
using Naveego.Streams;
using Naveego.Sync;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Naveego.Sync
{
    [TestFixture]
    public class SyncStreamJsonSerializationTests
    {

        [Test]
        public void PagedStreamResultTest()
        {
            string json = File.ReadAllText("SyncStream.json");
            var serializer = JsonSerializerFactory.CreateSerializer();

            using (var jr = new JsonTextReader(new StringReader(json)))
            {
                var result = serializer.Deserialize<PagedStreamResult<SyncStreamEvent>>(jr);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Events.Count(), Is.EqualTo(4));
                Assert.That(result.Events[0].Content, Is.InstanceOf<Rule>());
                Assert.That(result.Events[1].Content, Is.InstanceOf<Query>());
                Assert.That(result.Events[0].Content, Is.InstanceOf<Rule>());
                Assert.That(result.Events[0].Content, Is.InstanceOf<Rule>());
            }
        }


    }
}
