namespace VanillaClient.Patches.Harmony
{
    internal class PhotonPatch : VanillaPatches
    {
        protected override string patchName => "PhotonPatch";




        //    ExitGames.Client.Photon.

        public override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(PhotonPatch));

                //PatchMethod(typeof().GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("PhotonRaiseEventPatch"), null);



            }
            catch (Exception e)
            {
                Utils.LogHandler.ExceptionHandler(patchName, e);
            }
        }
    }
}
