using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VRC;

namespace Vanilla.Xrefs
{
    internal static class PlayerExtentions
    {
        private static MethodInfo _reloadAllAvatarsMethod;
        private static MethodInfo _reloadAvatarMethod;

        public static MethodInfo LoadAvatarMethod
        {
            get
            {
                if (_reloadAvatarMethod == null)
                {
                    _reloadAvatarMethod = typeof(VRCPlayer).GetMethods().First(mi => mi.Name.StartsWith("Method_Private_Void_Boolean_") && mi.Name.Length < 31 && mi.GetParameters().Any(pi => pi.IsOptional) && XRefManager.CheckUsedBy(mi, "ReloadAvatarNetworkedRPC"));
                }
                return _reloadAvatarMethod;
            }
        }

        public static MethodInfo ReloadAllAvatarsMethod
        {
            
            get
            {

                if (_reloadAllAvatarsMethod == null)
                {
                    _reloadAllAvatarsMethod = typeof(VRCPlayer).GetMethods().First(mi => mi.Name.StartsWith("Method_Public_Void_Boolean_") && mi.Name.Length < 30 && mi.GetParameters().All(pi => pi.IsOptional) && XRefManager.CheckUsedBy(mi, "Method_Public_Void_", typeof(FeaturePermissionManager)));// Both methods seem to do the same thing;
                }

                return _reloadAllAvatarsMethod;
            }
        }

        public static void ReloadAvatar(this VRCPlayer instance)
        {
            LoadAvatarMethod.Invoke(instance, [true]); // parameter is forceLoad and has to be true
        }

        public static void ReloadAllAvatars(this VRCPlayer instance, bool ignoreSelf = false)
        {
           ReloadAllAvatarsMethod.Invoke(instance, [ignoreSelf]);
        }

    }
}
