﻿namespace Vanilla.Config
{
    internal class RuntimeConfig
    {
        internal readonly static string GameVER = "2022.4.2p1-1275--Release";
        internal static bool isBot = false;
        internal static bool ShouldFly = false;
        internal static bool ShouldEarRape = false;
        internal static string NamePlatesLongString = "usr_94d9bc4e-6e16-438e-aa97-7382cb5187e4|VGhlIFJhaWQ/|IzkxNzRkYg==";

#if DEBUG
        internal static string ReleaseID = "Debug";
#else
        internal static string ReleaseID = "Release";
#endif
    }
}
