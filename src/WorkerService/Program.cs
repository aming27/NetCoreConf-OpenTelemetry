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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Utils.Messaging;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddSingleton<MessageReceiver>();

                    var url = Environment.GetEnvironmentVariable("OTEL_COLLECTOR_URL");
                    services.AddOpenTelemetryMetrics(builder =>
                    {
                        builder
                            .SetResourceBuilder(ResourceBuilder
                                .CreateDefault()
                                .AddService("Worker"));
                        builder.AddAspNetCoreInstrumentation();
                        builder.AddMeter(nameof(MessageReceiver));
                        builder.AddOtlpExporter(options =>
                        {
                            options.Endpoint = new Uri(url);
                        });
                        builder.AddConsoleExporter();
                    });

                    // Configure tracing
                    services.AddOpenTelemetryTracing(builder =>
                    {
                        builder
                            .SetResourceBuilder(ResourceBuilder
                                .CreateDefault()
                                .AddService("Worker"));
                        builder.AddSource(nameof(MessageReceiver));
                        builder.AddOtlpExporter(options => options.Endpoint = new Uri(url));
                        builder.AddConsoleExporter();
                    });
                }).ConfigureLogging(logging =>
                {
                    logging.AddOpenTelemetry(builder =>
                    {
                        builder
                            .SetResourceBuilder(ResourceBuilder
                                .CreateDefault()
                                .AddService("Worker"));
                        var url = Environment.GetEnvironmentVariable("OTEL_COLLECTOR_URL");
                        builder.IncludeFormattedMessage = true;
                        builder.IncludeScopes = true;
                        builder.ParseStateValues = true;
                        builder.AddOtlpExporter(options => options.Endpoint = new Uri(url));
                        builder.AddConsoleExporter();
                    });
                });
    }
}
