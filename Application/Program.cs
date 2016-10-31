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
                .SelectMany(x => x.Where(y => y.GetInterfaces().Contains(typeof(IPlugin))))
                .ToList();

            foreach (var type in listPlugins)
            {           
                var ctor = type.GetConstructor(new Type[] {});

                if (ctor == null) continue;

                var instance = ctor.Invoke(new object[] {});

                var propertyInfo = type.GetProperty("Name");

                var name = propertyInfo.GetValue(instance );

                Console.WriteLine(name);
            }

            Console.ReadLine();
        }
    }
}