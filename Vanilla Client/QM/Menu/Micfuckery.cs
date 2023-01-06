using Vanilla.Buttons.QM;

namespace Vanilla.QM.Menu
{
    internal class Micfuckery
    {
        internal static void MicFuckery(QMTabMenu tabMenu)
        {
            var Micsettings = new QMNestedButton(tabMenu, 2, 2, "Mic Settings", "Vanilla", "Vanilla Client");

            var Default = new QMSingleButton(Micsettings, 1, 0, "Defualt Bitrate", delegate
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_USpeaker_0.field_Private_BitRate_0 = BitRate.BitRate_24K;
            }, "Defaul Bitrate");

            var twozerokbitrate = new QMSingleButton(Micsettings, 2, 0, "20K Bitrate", delegate
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_USpeaker_0.field_Private_BitRate_0 = BitRate.BitRate_20K;
            }, "20K Bitrate on Mic");

            var eighteenkbitrate = new QMSingleButton(Micsettings, 3, 0, "18K Bitrate", delegate
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_USpeaker_0.field_Private_BitRate_0 = BitRate.BitRate_18K;
            }, "18K Bitrate on Mic");

            var sixteenokbitrate = new QMSingleButton(Micsettings, 4, 0, "16K Bitrate", delegate
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_USpeaker_0.field_Private_BitRate_0 = BitRate.BitRate_16K;
            }, "16K Bitrate on Mic");

            var tenkbitrate = new QMSingleButton(Micsettings, 1, 1, "10K Bitrate", delegate
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_USpeaker_0.field_Private_BitRate_0 = BitRate.BitRate_10K;
            }, "10K Bitrate on Mic");

            var eightkbitrate = new QMSingleButton(Micsettings, 2, 1, "8K Bitrate", delegate
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_USpeaker_0.field_Private_BitRate_0 = BitRate.BitRate_8K;
            }, "8K Bitrate on Mic");

            var micvoldefaul = new QMSingleButton(Micsettings, 3, 1, "Mic Vol Defaul", delegate
            {
                USpeaker.field_Internal_Static_Single_1 = 1;
            }, "Mic Defualt Volume");

            var maxvol = new QMSingleButton(Micsettings, 4, 1, "Mic Vol max", delegate
            {
                USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
            }, "Mic Volume Max");

            var MIcup = new QMSingleButton(Micsettings, 1, 2, "Mic Vol up", delegate
            {
                USpeaker.field_Internal_Static_Single_1 += 1;
            }, "Mic Volume +1");

            var Micdown = new QMSingleButton(Micsettings, 2, 2, "Mic Vol Down", delegate
            {
                USpeaker.field_Internal_Static_Single_1 -= 1;
            }, "Mic Volume -1");

        }
    }
}