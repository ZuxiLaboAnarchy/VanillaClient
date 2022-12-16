using System;
using System.Collections;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Vanilla.Config;
using VRC;
using VRC.Core;
// this is MezqueNameplates but modifyed nfor me to use bc i cannot be asked so thanks Mez  


namespace Vanilla.Modules
{
    internal class CustomTags
    {
        public static IEnumerator TagListNetworkManager()
        {
            String[] Match = { "|" };
            String[] newline = { "\n" };
            WebClient client = new WebClient();

            while (APIUser.CurrentUser == null)
            {
                yield return null;
            }
            while (NetworkManager.field_Internal_Static_NetworkManager_0 == null)
            {
                yield return null;
            }
            NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(RemotePlayer =>
            {
             //   string DevList = Settings.Config.NamePlatesLongStaff;
               
              //  String[] DevDB = DevList.Split(newline, StringSplitOptions.RemoveEmptyEntries);
                String[] DataBase = RuntimeConfig.NamePlatesLongString.Split(newline, StringSplitOptions.RemoveEmptyEntries);

                foreach (var User in DataBase)
                {
                    if (User.Contains(RemotePlayer.prop_APIUser_0.id))
                    {
                        var VanillaTag = GameObject.Instantiate(RemotePlayer.transform.Find("NameplateContainer/PlayerNameplate/Canvas/NameplateGroup/Nameplate/Contents/Trust Text"), RemotePlayer.transform.Find("NameplateContainer/PlayerNameplate/Canvas/NameplateGroup/Nameplate/Contents/Quick Stats"));
                        VanillaTag.name = "VanillaClientUserTag";
                        var CTagTM = VanillaTag.GetComponent<TMPro.TextMeshProUGUI>();
                        CTagTM.color = Color.white;
                        CTagTM.text = $"<color=#00FFFF>Vanilla User</color>";
                        String[] TagP = User.Split(Match, StringSplitOptions.RemoveEmptyEntries);
                        if (TagP.Length == 3)
                        {
                            var Tag = GameObject.Instantiate(RemotePlayer.transform.Find("NameplateContainer/PlayerNameplate/Canvas/NameplateGroup/Nameplate/Contents/Trust Text"), RemotePlayer.transform.Find("NameplateContainer/PlayerNameplate/Canvas/NameplateGroup/Nameplate/Contents/Quick Stats"));
                            Tag.name = "VanillaUserTag";
                            var TagTM = Tag.GetComponent<TMPro.TextMeshProUGUI>();
                            TagTM.color = Color.white;
                            TagTM.text = $"<color={UserProtections.DecodeBase64(TagP.GetValue(2).ToString())}>{UserProtections.DecodeBase64(TagP.GetValue(1).ToString())}</color>";
                        }
                    }
                }

               /* foreach (var User in DevDB)
                {
                    if (User.Contains(RemotePlayer.prop_APIUser_0.id))
                    {
                        var CTag = GameObject.Instantiate(RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats/Trust Text"), RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats"));
                        CTag.name = "VanillaStaffTag";
                        var CTagTM = CTag.GetComponent<TMPro.TextMeshProUGUI>();
                        CTagTM.color = Color.white;
                        CTagTM.text = $"<color=#00FFFF>Vanilla Staff</color>";
                        String[] TagP = User.Split(Match, StringSplitOptions.RemoveEmptyEntries);
                        if (TagP.Length == 3)
                        {
                            var Tag = GameObject.Instantiate(RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats/Trust Text"), RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats"));
                            Tag.name = "VanillaUserTag";
                            var TagTM = Tag.GetComponent<TMPro.TextMeshProUGUI>();
                            TagTM.color = Color.white;
                            TagTM.text = $"<color={UserProtections.DecodeBase64(TagP.GetValue(2).ToString())}>{UserProtections.DecodeBase64(TagP.GetValue(1).ToString())}</color>";
                        }
                    }
                }
                */
                if (RemotePlayer.prop_APIUser_0.hasModerationPowers)
                {
                    var FCModTag = GameObject.Instantiate(RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats/Trust Text"), RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats"));
                    FCModTag.name = "VanillaModTag";
                    var TagTM = FCModTag.GetComponent<TMPro.TextMeshProUGUI>();
                    TagTM.color = Color.white;
                    TagTM.text = "<color=red>VRChat Staff</color>";
                }
                if (RemotePlayer.prop_VRCPlayerApi_0.isMaster)
                {
                    var FCMasterTag = GameObject.Instantiate(RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats/Trust Text"), RemotePlayer.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Quick Stats"));
                    FCMasterTag.name = "VanillaMasterTag";
                    var TagTM = FCMasterTag.GetComponent<TMPro.TextMeshProUGUI>();
                    TagTM.color = Color.white;
                    TagTM.text = "<color=yellow>Master</color>";
                }
            }));
        }
    }
}
public static class UserProtections
{
    public static string EncodeBase64(this string value)
    {
        var valueBytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(valueBytes);
    }

    public static string DecodeBase64(this string value)
    {
        var valueBytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(valueBytes);
    }
    public static string SHA256(string value)
    {
        HashAlgorithm hashAlgorithm = new SHA256Managed();
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte b in hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(value)))
        {
            stringBuilder.Append(b.ToString("x2"));
        }
        return stringBuilder.ToString();
    }
}
