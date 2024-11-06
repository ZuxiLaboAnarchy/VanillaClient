// /*
//  *
//  * VanillaClient - ResourceUtils.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Reflection;

namespace Vanilla.Utils
{
    internal class ResourceUtils
    {
        internal static byte[] ExtractResource(string filename)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            using var stream = executingAssembly.GetManifestResourceStream(filename);
            if (stream == null)
            {
                return null;
            }

            var array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            return array;
        }

        internal static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            Dev("Resolver", "Game Attempted to Resolve: " + args.Name);
            var a1 = Assembly.GetExecutingAssembly();
            var s = a1.GetManifestResourceStream(args.Name);
            var block = new byte[s.Length];
            s.Read(block, 0, block.Length);
            var a2 = Assembly.Load(block);
            return a2;
        }
    }
}
