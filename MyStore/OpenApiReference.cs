// Requires NuGet package: Swashbuckle.AspNetCore (install via 'dotnet add package Swashbuckle.AspNetCore')
using Microsoft.OpenApi;

namespace MyStore
{
    internal class OpenApiReference
    {
        public ReferenceType Type { get; set; }
        public string Id { get; set; }
    }
}