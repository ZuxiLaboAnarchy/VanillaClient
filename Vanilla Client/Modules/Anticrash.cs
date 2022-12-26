using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Vanilla.QM.Menu.Safetymenu;

namespace Vanilla.Modules
{
    internal class Anticrash
    {

        internal static string whitelist = "";
        private static SkinnedMeshRenderer[] _SkinnedMeshRenderers { get; set; }
        private static MeshRenderer[] _MeshRenderes { get; set; }
        private static MeshFilter[] _MeshFilters { get; set; }
        private static Renderer[] _Renderers { get; set; }
        private static ParticleSystem[] _ParticleSystems { get; set; }
        private static LineRenderer[] _LineRenderers { get; set; }
        private static Light[] _Lights { get; set; }
        private static AudioSource[] AudioSources { get; set; }
        private static AvatarAudioSourceFilter[] AvatarAudioSourceFilters { get; set; }
        //Thx to Autumn for telling me about constructors.
        public Anticrash(GameObject obj, VRCPlayer vrcplayer)
        {
            if (meshp)
            {
                _SkinnedMeshRenderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>(true);
                _MeshRenderes = obj.GetComponentsInChildren<MeshRenderer>(true);
                int maxmaterials = 0;
                for (int i = 0; i < _MeshRenderes.Length; i++)
                    maxmaterials += _MeshRenderes[i].materials.Length;

                if (_MeshRenderes.Length > maxmeshes)
                {
                    MelonLogger.Msg($"User {vrcplayer._player.field_Private_APIUser_0.displayName} hiddien by anticrash (Meshes) Meshrenders:[{_MeshRenderes.Length}] ");
                    return;
                }
                if (maxmaterials > 800)
                {
                    MelonLogger.Msg($"User {vrcplayer._player.field_Private_APIUser_0.displayName} hiddien by anticrash (Meshes) Giagantic number of materials:[{maxmaterials}] ");
                    return;
                }
                if (_SkinnedMeshRenderers.Length > maxmeshes / 1.5f)
                {
                    MelonLogger.Msg($"User {vrcplayer._player.field_Private_APIUser_0.displayName} hiddien by anticrash (Meshes) SkindeMeshRenders:[{_SkinnedMeshRenderers.Length}] ");
                    return;
                }
                for (int i3 = 0; i3 < _MeshRenderes.Length; i3++)
                {
                    if (_MeshRenderes[i3].materials.Count < maxmaterials)
                    {
                        var a = new List<Material>();
                        a.Add(new Material(Shader.Find("Standard")));
                        _MeshRenderes[i3].materials = a.ToArray();
                    }
                }
                for (int i = 0; i < _SkinnedMeshRenderers.Length; i++)
                {
                    if (i > 5) continue;
                    if (_SkinnedMeshRenderers[i].materials.Length > maxmaterials * 1.5f)
                    {
                        var a = new List<Material>();
                        a.Add(new Material(Shader.Find("Standard")));
                        _SkinnedMeshRenderers[i].materials = a.ToArray();
                    }
                }
            }


            if (verticiesp)
            {
                _MeshFilters = obj.GetComponentsInChildren<MeshFilter>(true);
                int toinc = 0;
                try
                {
                    for (int i = 0; i < _SkinnedMeshRenderers.Length; i++)
                    {
                        if (_SkinnedMeshRenderers[i].sharedMesh.vertexCount > maxverticies)
                            return;
                    }
                }
                catch { }
                try
                {
                    for (int i = 0; i < _MeshFilters.Length; i++)
                    {
                        toinc += _MeshFilters[i].sharedMesh.vertexCount;
                    }
                    if (toinc > maxverticies)
                        return;
                }
                catch { }

            }


            /*
            if (ShaderP)
            {
                _Renderers = obj.GetComponentsInChildren<Renderer>(true);
                for (int v = 0; v < _Renderers.Length; v++)
                {
                    for (int i = 0; i < _Renderers[v].materials.Length; i++)
                    {
                        bool isbadshader = false;

                        if (_Renderers[v].materials[i].shader.name.Contains("crash") || _Renderers[v].materials[i].shader.name.Contains("lag") || _Renderers[v].materials[i].shader.name.Contains("Crash") || _Renderers[v].materials[i].shader.name.Contains("Lag"))
                        {
                            if (_Renderers[v].materials[i].shader.name != "Standard")
                                Defiancec.Log($"Shader Replaced:[{_Renderers[v].materials[i].shader.name}] on: {_Renderers[v].name}", "Anti Crash", ConsoleColor.Red, ConfigVars.logshaderstoconsole);
                            _Renderers[v].materials[i].shader = Shader.Find("Standard");
                        }
                        var splited = Download_Images.shaderlist.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i3 = 0; i3 < splited.Length; i3++)
                        {
                            var tobc = splited[i3].Trim();
                            if (tobc.Length > 3)
                            {
                                if (_Renderers[v].materials[i].shader.name.Contains(tobc))
                                    isbadshader = true;

                            }
                        }
                        if (!isbadshader)
                        {
                            if (_Renderers[v].materials[i].shader.name != "Standard")
                                Defiancec.Log($"Shader Replaced:[{_Renderers[v].materials[i].shader.name}] on: {_Renderers[v].name}", "Anti Crash", ConsoleColor.Red, ConfigVars.logshaderstoconsole);
                            _Renderers[v].materials[i].shader = Shader.Find("Standard");
                        }
                        if (_Renderers[v].materials[i].shader.passCount > 6)
                        {
                            if (_Renderers[v].materials[i].shader.name != "Standard")
                                Defiancec.Log($"Shader Replaced:[{_Renderers[v].materials[i].shader.name}] on: {_Renderers[v].name}", "Anti Crash", ConsoleColor.Red, ConfigVars.logshaderstoconsole);
                            _Renderers[v].materials[i].shader = Shader.Find("Standard");

                        }
                    }
                }
            }
            */
            if (particlep)
            {
                int particlescount = 0;
                _ParticleSystems = obj.GetComponentsInChildren<ParticleSystem>(true);
                for (int i = 0; i < _ParticleSystems.Length; i++)
                {
                    if (_ParticleSystems[i].maxParticles > maxparticles)
                    {
                        _ParticleSystems[i].maxParticles = 0;
                    }
                }
                for (int i = 0; i < _ParticleSystems.Length; i++)
                {
                    if (_ParticleSystems[i].emissionRate > 99)
                    {
                        _ParticleSystems[i].emissionRate = 0;
                    }
                }
                if (_ParticleSystems.Length > particlesystem)
                {
                    for (int i = 0; i < _ParticleSystems.Length; i++)
                        Component.DestroyImmediate(_ParticleSystems[i]);
                }
                else
                if (_ParticleSystems.Length != 0)
                {
                    for (int i = 0; i < _ParticleSystems.Length; i++)
                        particlescount += _ParticleSystems[i].particleCount;
                    if (particlescount > 50000)
                    {
                        for (int i = 0; i < _ParticleSystems.Length; i++)
                            Component.DestroyImmediate(_ParticleSystems[i]);
                    }
                }
            }

            if (linerenderp)
            {
                _LineRenderers = obj.GetComponentsInChildren<LineRenderer>(true);
                if (_LineRenderers.Length > 49)
                {
                    for (int i = 0; i < _LineRenderers.Length; i++)
                        Component.DestroyImmediate(_LineRenderers[i]);
                    return;
                }
                for (int i = 0; i < _LineRenderers.Length; i++)
                {
                    if (_LineRenderers[i].positionCount > 20)
                    {
                        Component.DestroyImmediate(_LineRenderers[i]);
                    }
                }
            }

            if (lightsp)
            {
                _Lights = obj.GetComponentsInChildren<Light>(true);
                if (_Lights.Length > 15)
                {
                    for (int i = 0; i < _Lights.Length; i++)
                        Component.DestroyImmediate(_Lights[i]);
                }
            }



            if (audiosourcep)
            {
                AudioSources = obj.GetComponentsInChildren<AudioSource>(true);
                AvatarAudioSourceFilters = obj.GetComponentsInChildren<AvatarAudioSourceFilter>(true);
                if (AvatarAudioSourceFilters.Length > maxaudiosources)
                {
                    for (int i = 0; i < AvatarAudioSourceFilters.Length; i++)
                        Component.DestroyImmediate(AvatarAudioSourceFilters[i]);
                }
                if (AudioSources.Length > maxaudiosources)
                {
                    for (int i = 0; i < AudioSources.Length; i++)
                        Component.DestroyImmediate(AudioSources[i]);
                    return;
                }
                for (int i = 0; i < AudioSources.Length; i++)
                    if (AudioSources[i].clip == null)
                        Component.DestroyImmediate(AudioSources[i]);
            }

            obj.SetActive(true);



        }
        
    }
}
