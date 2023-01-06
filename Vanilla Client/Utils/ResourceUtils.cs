using System.IO;
using System.Reflection;

namespace Vanilla
{



    internal class ResourceUtils
    {
        internal static byte[] ExtractResource(string filename)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            using Stream stream = executingAssembly.GetManifestResourceStream(filename);
            if (stream == null)
            {
                return null;
            }
            byte[] array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            return array;
        }

        internal static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            Dev("Resolver", "Game Attempted to Resolve: " + args.Name);
            Assembly a1 = Assembly.GetExecutingAssembly();
            Stream s = a1.GetManifestResourceStream(args.Name);
            byte[] block = new byte[s.Length];
            s.Read(block, 0, block.Length);
            Assembly a2 = Assembly.Load(block);
            return a2;
        }
    }


}

