using Harmony;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;
using Vanilla.Config;
using Vanilla.Wrappers;
using Vanilla.Xrefs;
using static Vanilla.QM.Menu.SafetyMenu;
using Object = UnityEngine.Object;

namespace Vanilla.Modules
{
    internal class Anticrash : VanillaModule
    {
        protected override string ModuleName => "AntiCrash";

        internal override void Start()
        {
            Reload();
        }


        internal static void Reload()
        {
            GetInstance().Load();
            Log("AntiCrash", "Reloaded WhiteList");
            if (PlayerWrapper.GetLocalPlayer() != null)
            {
                Log("AntiCrash", "Reloading All Avatar...");
                PlayerWrapper.GetLocalPlayer().ReloadAllAvatars(false);
            }
        }

        internal static void Save()
        {
            GetInstance().Save();
            //File.WriteAllLines(Path.Combine(FileHelper.GetCheatFolder(), "AntiCrash", "WhiteListedAvtIDs.txt"), WhiteListedAvtID);
        }

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
        public static bool ProcessAvatar(GameObject obj, VRCPlayer vrcplayer)
        {
            obj.SetActive(true);
            var safe = false;
            safe = _ProcessAvatar(obj, vrcplayer);
            obj.SetActive(safe);
            return safe;
        }

        private static bool _ProcessAvatar(GameObject obj, VRCPlayer vrcplayer)
        {
            try
            {
                return true;
                if (obj == null)
                {
                    return false;
                }

                if (vrcplayer == null)
                {
                    return false;
                }

                if (PlayerWrapper.GetLocalPlayer() == vrcplayer)
                {
                    return true;
                }

                //FIX FOR LOUD ASS AVATARS thanks akuma for me having to do this lmao
                AudioSources = obj.GetComponentsInChildren<AudioSource>(true);
                for (var i = 0; i < AudioSources.Length; i++)
                {
                    if (AudioSources[i].clip == null)
                    {
                        Object.DestroyImmediate(AudioSources[i]);
                    }
                }

                for (var i = 0; i < AudioSources.Length; i++)
                {
                    AudioSources[i].volume = 50;
                }

                if (GetInstance().WhiteListedAvatarIDs.Contains(vrcplayer.prop_ApiAvatar_0.id))
                {
                    return true;
                }

                if (audiosourcep)
                {
                    AvatarAudioSourceFilters = obj.GetComponentsInChildren<AvatarAudioSourceFilter>(true);
                    if (AvatarAudioSourceFilters.Length > maxaudiosources)
                    {
                        for (var i = 0; i < AvatarAudioSourceFilters.Length; i++)
                        {
                            Object.DestroyImmediate(AvatarAudioSourceFilters[i]);
                        }
                    }

                    if (AudioSources.Length > maxaudiosources)
                    {
                        for (var i = 0; i < AudioSources.Length; i++)
                        {
                            Object.DestroyImmediate(AudioSources[i]);
                        }

                        return false;
                    }

                    for (var i = 0; i < AudioSources.Length; i++)
                    {
                        if (AudioSources[i].clip == null)
                        {
                            Object.DestroyImmediate(AudioSources[i]);
                        }
                    }

                    for (var i = 0; i < AudioSources.Length; i++)
                    {
                        AudioSources[i].volume = i;
                    }
                }

                if (meshp)
                {
                    List<Material> maMaterial = new();
                    _SkinnedMeshRenderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>(true);
                    _MeshRenderes = obj.GetComponentsInChildren<MeshRenderer>(true);
                    var maxmaterials = 0;
                    for (var i = 0; i < _MeshRenderes.Length; i++)
                    {
                        maxmaterials += _MeshRenderes[i].materials.Length;
                        foreach (var a in _MeshRenderes[i].materials)
                        {
                            maMaterial.Add(a);
                        }
                    }

                    if (_MeshRenderes.Length > maxmeshes)
                    {
                        for (var i = 0; i < _MeshRenderes.Length; i++)
                        {
                            if (100 < i)
                            {
                                continue;
                            }

                            Object.DestroyImmediate(_MeshRenderes[i]);
                        }

                        Log("AntiCrash",
                            $"User {vrcplayer._player.field_Private_APIUser_0.displayName} Hidden by anticrash (Meshes) Meshrenders:[{_MeshRenderes.Length}] ");
                        obj.SetActive(false);
                        return false;
                    }

                    if (maxmaterials > 800)
                    {
                        for (var i = 0; i < maMaterial.Count; i++)
                        {
                            if (800 < i)
                            {
                                continue;
                            }

                            Object.DestroyImmediate(maMaterial[i]);
                        }

                        Log("AntiCrash",
                            $"User {vrcplayer._player.field_Private_APIUser_0.displayName} Hidden by anticrash (Meshes) Large number of materials:[{maxmaterials}] ");
                        obj.SetActive(false);
                        return false;
                    }

                    if (_SkinnedMeshRenderers.Length > maxmeshes / 1.5f)
                    {
                        for (var i = 0; i < _SkinnedMeshRenderers.Length; i++)
                        {
                            if (maxmeshes < i)
                            {
                                continue;
                            }

                            Object.DestroyImmediate(_SkinnedMeshRenderers[i]);
                        }

                        Log("AntiCrash",
                            $"User {vrcplayer._player.field_Private_APIUser_0.displayName} Hidden by anticrash (Meshes) SkinMeshRenders:[{_SkinnedMeshRenderers.Length}] ");

                        obj.SetActive(false);
                        return false;
                    }

                    for (var i3 = 0; i3 < _MeshRenderes.Length; i3++)
                    {
                        if (_MeshRenderes[i3].materials.Count < maxmaterials)
                        {
                            var a = new List<Material>();
                            a.Add(new Material(Shader.Find("Standard")));
                            _MeshRenderes[i3].materials = a.ToArray();
                        }
                    }

                    for (var i = 0; i < _SkinnedMeshRenderers.Length; i++)
                    {
                        if (i > 5)
                        {
                            continue;
                        }

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
                    var toinc = 0;
                    try
                    {
                        for (var i = 0; i < _SkinnedMeshRenderers.Length; i++)
                        {
                            if (_SkinnedMeshRenderers[i].sharedMesh.vertexCount > maxverticies)
                            {
                                return false;
                            }
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        for (var i = 0; i < _MeshFilters.Length; i++)
                        {
                            toinc += _MeshFilters[i].sharedMesh.vertexCount;
                        }

                        if (toinc > maxverticies)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                    }
                }


                if (ShaderP)
                {
                    _Renderers = obj.GetComponentsInChildren<Renderer>(true);
                    for (var v = 0; v < _Renderers.Length; v++)
                    {
                        for (var i = 0; i < _Renderers[v].materials.Length; i++)
                        {
                            if (GetInstance().WhiteListedShaderList.Any(substring =>
                                    _Renderers[v].materials[i].shader.name.Contains(substring)))
                            {
                                continue;
                            }

                            var isbadshader = false;

                            if (_Renderers[v].materials[i].shader.name.Contains("crash") ||
                                _Renderers[v].materials[i].shader.name.Contains("lag") ||
                                _Renderers[v].materials[i].shader.name.Contains("Crash") ||
                                _Renderers[v].materials[i].shader.name.Contains("Lag"))
                            {
                                if (_Renderers[v].materials[i].shader.name != "Standard")
                                {
                                    Log("AntiCrash",
                                        "1 " + vrcplayer._player.field_Private_APIUser_0.displayName +
                                        $": Shader Replaced:[{_Renderers[v].materials[i].shader.name}] on: {_Renderers[v].name}",
                                        ConsoleColor.Red);
                                }

                                _Renderers[v].materials[i].shader = Shader.Find("Standard");
                            }

                            if (GetInstance().WhiteListedShaderList.Any(substring =>
                                    !_Renderers[v].materials[i].shader.name.Contains(substring)))
                            {
                                Log("Anti Crash",
                                    "2 " + vrcplayer._player.field_Private_APIUser_0.displayName +
                                    $": Shader Replaced:[{_Renderers[v].materials[i].shader.name}] on: {_Renderers[v].name}",
                                    ConsoleColor.Red);
                                _Renderers[v].materials[i].shader = Shader.Find("Standard");
                            }

                            /*
                            if (!isbadshader)
                            {
                                if (_Renderers[v].materials[i].shader.name != "Standard")
                                    Log("Anti Crash", $"Shader Replaced:[{_Renderers[v].materials[i].shader.name}] on: {_Renderers[v].name}", ConsoleColor.Red);
                                _Renderers[v].materials[i].shader = Shader.Find("Standard");
                            }*/
                            if (_Renderers[v].materials[i].shader.passCount > 6)
                            {
                                if (_Renderers[v].materials[i].shader.name != "Standard")
                                {
                                    Log("AntiCrash",
                                        "3 " + vrcplayer._player.field_Private_APIUser_0.displayName +
                                        $": Shader Replaced:[{_Renderers[v].materials[i].shader.name}] on: {_Renderers[v].name}",
                                        ConsoleColor.Red);
                                }

                                _Renderers[v].materials[i].shader = Shader.Find("Standard");
                            }
                        }
                    }
                }

                if (particlep)
                {
                    var particlescount = 0;
                    _ParticleSystems = obj.GetComponentsInChildren<ParticleSystem>(true);
                    for (var i = 0; i < _ParticleSystems.Length; i++)
                    {
                        if (_ParticleSystems[i].maxParticles > maxparticles)
                        {
                            _ParticleSystems[i].maxParticles = 0;
                        }
                    }

                    for (var i = 0; i < _ParticleSystems.Length; i++)
                    {
                        if (_ParticleSystems[i].emissionRate > 99)
                        {
                            _ParticleSystems[i].emissionRate = 0;
                        }
                    }

                    if (_ParticleSystems.Length > particlesystem)
                    {
                        for (var i = 0; i < _ParticleSystems.Length; i++)
                        {
                            Object.DestroyImmediate(_ParticleSystems[i]);
                        }
                    }
                    else if (_ParticleSystems.Length != 0)
                    {
                        for (var i = 0; i < _ParticleSystems.Length; i++)
                        {
                            particlescount += _ParticleSystems[i].particleCount;
                        }

                        if (particlescount > 50000)
                        {
                            for (var i = 0; i < _ParticleSystems.Length; i++)
                            {
                                Object.DestroyImmediate(_ParticleSystems[i]);
                            }
                        }
                    }

                    if (_ParticleSystems.Count() > 10)
                    {
                        Log("AntiCrash", "Removing Extra Particle Systems");
                        foreach (var i in _ParticleSystems)
                        {
                            Object.DestroyImmediate(i);
                        }
                    }
                }

                if (linerenderp)
                {
                    _LineRenderers = obj.GetComponentsInChildren<LineRenderer>(true);
                    if (_LineRenderers.Length > 49)
                    {
                        for (var i = 0; i < _LineRenderers.Length; i++)
                        {
                            Object.DestroyImmediate(_LineRenderers[i]);
                        }

                        return false;
                    }

                    for (var i = 0; i < _LineRenderers.Length; i++)
                    {
                        if (_LineRenderers[i].positionCount > 20)
                        {
                            Object.DestroyImmediate(_LineRenderers[i]);
                        }
                    }
                }

                if (lightsp)
                {
                    _Lights = obj.GetComponentsInChildren<Light>(true);
                    if (_Lights.Length > 15)
                    {
                        for (var i = 0; i < _Lights.Length; i++)
                        {
                            Object.DestroyImmediate(_Lights[i]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                obj.SetActive(false);
                return false;
            }

            obj.SetActive(true);
            return true;
        }
    }
}