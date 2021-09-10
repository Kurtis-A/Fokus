using AutoMapper;
using System;

namespace Fokus.Helpers
{
    public static class Globals
    {
        public static IMapper Mapper { get; set; }

        public static IServiceProvider ServiceProvider { get; set; }
    }
}