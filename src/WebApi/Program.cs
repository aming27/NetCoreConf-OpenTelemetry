// <copyright file="Program.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:5000").UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddOpenTelemetry(builder =>
                    {
                        builder
                            .SetResourceBuilder(ResourceBuilder
                                .CreateDefault()
                                .AddService("Api"));
                        var url = Environment.GetEnvironmentVariable("OTEL_COLLECTOR_URL");
                        builder.IncludeFormattedMessage = true;
                        builder.IncludeScopes = true;
                        builder.ParseStateValues = true;
                        builder.AddOtlpExporter(options => options.Endpoint = new Uri(url));
                    });
                });
    }
}
