// /*
//  *
//  * VanillaClient - FullScan.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Reflection;
using Vanilla.Patches.Manager;

namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class Scanner : VanillaPatches
    {
        private static void FindOBBB()
        {
            //   Test =;   //       onClick.FindMethod_Impl.Name.ToString();


            //     InitializeLocalPatchHandler(typeof(ImageDownloaderPatch));


            /// PatchMethod(typeof().GetMethod("DownloadImageInternal"), GetLocalPatch("OnImageDownloadPatch"), null);

            //Test.
        }
#if DEBUG1
                GameObject.Find("").GetComponent<Button>().onClick.FindMethod_Impl.Name.ToString();

    protected override string patchName => "Scanner";

    internal override void Patch()
    {
        try
        {




            InitializeLocalPatchHandler(typeof(Scanner));




            LogHandler.Dev("FullScanner", $"Attempting Scan");


            //   PatchManager.AnalyzeFunction(typeof(BoltInit).GetMethod("print"));
            int Count = 0;
            foreach (var method in typeof(null).GetMethods())
            {

                if (method.Name == null) { LogHandler.Dev("FullScanner", "Method Null");}
                // if (!method.Name.StartsWith("print")) continue;
                if (XrefScanner.XrefScan(method).Count() < 3) continue;
                try
                {
                    LogHandler.Dev("FullScanner", $"Scanning Method {method}");
                    PatchManager.AnalyzeFunction(method);
                    LogHandler.Dev("FullScanner", $"Finnish Scanning Method {method}");
                    //  PatchMethod(method, null, GetLocalPatch("LogError"));
                }
                catch (Exception e) { }

                //LogHandler.ExceptionHandler("Anti Cheat", e, Count); Count++; }
            }


        }
        catch (Exception e)
        {
            Utils.LogHandler.ExceptionHandler(patchName, e);
        }
    }

#endif
    }
}
