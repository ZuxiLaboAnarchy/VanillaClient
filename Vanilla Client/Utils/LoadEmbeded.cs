using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla.Utils
{
    internal class LoadEmbeded
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
    }
}
