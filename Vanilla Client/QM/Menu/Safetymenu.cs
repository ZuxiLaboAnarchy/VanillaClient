using System.Collections;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Wrappers;

namespace Vanilla.QM.Menu
{
    internal class SafetyMenu
    {
        internal static void InitMenu(QMNestedButton Safemenu)
        {

          

            var Getoffhead = new QMSingleButton(Safemenu, 1, 0, "Anti Attach", delegate
            {
                TeleportToSpace(3f).Start();
            }, "Ports You to space then reloads crashing user atttached");

            var Anticrash = new QMNestedButton(Safemenu, 2, 0, "AntiCrash", "Vanilla", "Vanilla Client");

            var Particles = new QMToggleButton(Anticrash, 1, 0, "Particles Anti", delegate
            {
                particlep = true;
            }, delegate
            {
                particlep = false;
            }, "Particles anti crash");

            var AudioSources = new QMToggleButton(Anticrash, 2, 0, "Audio Sources Anti", delegate
            {
                audiosourcep = true;
            }, delegate
            {
                audiosourcep = false;
            }, "AudioSources anti");

            var Lightsource = new QMToggleButton(Anticrash, 3, 0, "Light Sources Anti", delegate
            {
                lightsp = true;
            }, delegate
            {
                lightsp = false;
            }, "Light Sources anti");

            var LineRenderers = new QMToggleButton(Anticrash, 4, 0, "Line Renderers Anti", delegate
            {
                linerenderp = true;
            }, delegate
            {
                linerenderp = false;
            }, "Line Renderers anti");

            var Meshes = new QMToggleButton(Anticrash, 1, 1, "Meshes Anti", delegate
            {
                meshp = true;
            }, delegate
            {
                meshp = false;
            }, "Meshes anti");

            var vertecies = new QMToggleButton(Anticrash, 2, 1, "Vertecies Anti", delegate
            {
                verticiesp = true;
            }, delegate
            {
                verticiesp = false;
            }, "Vertecies anti");

            var AntiE1 = new QMToggleButton(Safemenu, 3, 0, "Anti Earrape", delegate
            {
                MainConfig.AntiE1 = true;
            }, delegate
            {
                MainConfig.AntiE1 = false;
            }, "Anti Event 1 Bad Data");


            if (MainConfig.AntiE1 == true)
            { AntiE1.ClickMe(); }
           
        }


    // Safety Handleing

    private static Vector3 _currentVector;

        private static Quaternion _currentQuaternion;

        internal static IEnumerator TeleportToSpace(float delay)
        {
            _currentVector = PlayerWrapper.GetLocalPlayer().transform.position;
            _currentQuaternion = PlayerWrapper.GetLocalPlayer().transform.rotation;
            yield return new WaitForEndOfFrame();
            PlayerWrapper.GetLocalPlayer().transform.position = new Vector3(0f, 999999f, 0f);
            yield return new WaitForSeconds(delay);
            GeneralWrappers.CopyInstanceToClipboard();
            GeneralWrappers.JoinInstanceFromClipboard();
        }



        // Just going to set the anti Values here
        internal static int maxaudiosources = 15;
        internal static int maxmaterials = 60;
        internal static int maxmeshes = 100;
        internal static int maxverticies = 200000;
        internal static int maxparticles = 30000;
        internal static int particlesystem = 30;
        internal static bool verticiesp = true;
        internal static bool meshp = true;
        internal static bool ShaderP = true;
        internal static bool audiosourcep = true;
        internal static bool particlep = true;
        internal static bool linerenderp = true;
        internal static bool lightsp = true;

      //  public static bool Antirape { get; private set; }
    }
}
