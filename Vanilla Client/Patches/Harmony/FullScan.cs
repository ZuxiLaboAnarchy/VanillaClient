namespace Vanilla.Patches.Harmony;

internal class Scanner : VanillaPatches
{
#if DEBUG1
    protected override string patchName => "Scanner";

    public override void Patch()
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
