using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using NEventStore;
using NEventStore.Serialization.Json;

namespace DotNetCoreHelloWorld
{
    public class Program
    {
        public static IStoreEvents EventStore;
        public const string StreamId = "DefaultStream";
        public const string BucketId = "DefaultBucketId";
        
        public static void Main(string[] args)
        {
            EventStore = Wireup.Init()
            .UsingInMemoryPersistence()
            .UsingJsonSerialization()
            .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
