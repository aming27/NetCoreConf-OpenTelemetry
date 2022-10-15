// <copyright file="Startup.cs" company="OpenTelemetry Authors">
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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Utils.Messaging;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<MessageSender>();

            var url = Environment.GetEnvironmentVariable("OTEL_COLLECTOR_URL");
            services.AddOpenTelemetryMetrics(builder =>
            {
                builder
                    .SetResourceBuilder(ResourceBuilder
                        .CreateDefault()
                        .AddService("Api"));
                builder.AddMeter(nameof(MessageSender));
                builder.AddOtlpExporter(options => options.Endpoint = new Uri(url));
                builder.AddConsoleExporter();
            });

            // Configure tracing
            services.AddOpenTelemetryTracing(builder =>
            {
                
                builder
                    .SetResourceBuilder(ResourceBuilder
                        .CreateDefault()
                        .AddService("Api"));
                builder.AddAspNetCoreInstrumentation();
                builder.AddSource(nameof(MessageSender));
                builder.AddOtlpExporter(options => options.Endpoint = new Uri(url));
                builder.AddConsoleExporter();
            });
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
