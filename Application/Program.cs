using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Framework;

namespace Application
{
    static class Program
    {
        static void Main()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Plugins")
                .Where(x=>x.Contains(".dll"))
                .ToList();

            var listPlugins = files
                .Select(x => Assembly.LoadFile(x).GetTypes())
                .SelectMany(x => x.Where(y => y.GetInterfaces().Contains(typeof(IPlugin)) && y.GetConstructor(Type.EmptyTypes) != null))
                .ToList();

            foreach (var type in listPlugins)
            {
                var plugin = Activator.CreateInstance(type) as IPlugin;

                if(plugin == null) continue;

                Console.WriteLine(plugin.Name);
            }

            Console.ReadLine();
        }
    }
}