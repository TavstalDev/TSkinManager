using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Assets;
using Rocket.Core.Permissions;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Logger = Rocket.Core.Logging.Logger;

namespace Tavstal.TSkinManager.Helpers
{
    public static class PermissionHelper
    {
        static RocketPermissionsManager PermissionsManager => R.Instance.GetComponent<RocketPermissionsManager>();

        public static string GetDefaultGroupID()
        {
            try
            {
                FieldInfo helperFieldInfo = PermissionsManager.GetType().GetField("helper", BindingFlags.NonPublic | BindingFlags.Instance);
                object helperObject = helperFieldInfo?.GetValue(PermissionsManager);
                Type helperType = helperObject?.GetType();
                Asset<RocketPermissions> permissions = (Asset<RocketPermissions>)(helperType?.GetField("permissions", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(helperObject));
                return permissions?.Instance.DefaultGroup;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }
            return string.Empty;
        }

        public static List<RocketPermissionsGroup> GetPermissionsGroups()
        {
            try
            {
                FieldInfo helperFieldInfo = PermissionsManager.GetType().GetField("helper", BindingFlags.NonPublic | BindingFlags.Instance);
                object helperObject = helperFieldInfo.GetValue(PermissionsManager);
                Type helperType = helperObject.GetType();
                Asset<RocketPermissions> permissions = (Asset<RocketPermissions>)(helperType.GetField("permissions", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(helperObject));
                return permissions.Instance.Groups;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }

            return new List<RocketPermissionsGroup>();
        }

        public static List<RocketPermissionsGroup> GetPlayerGroups(CSteamID steamID)
        {
            string defaultId = GetDefaultGroupID();
            return GetPermissionsGroups().FindAll(x => x.Id == defaultId || x.Members.Contains(steamID.m_SteamID.ToString()));
        }

        public static bool HasPermission(CSteamID steamID, string permission)
        {
            if (SteamAdminlist.checkAdmin(steamID))
                return true;
            
            bool value = false;
            foreach (RocketPermissionsGroup group in GetPlayerGroups(steamID))
            {
                if (group.Permissions.Any(x => x.Name.ToLower() == permission.ToLower()))
                {
                    value = true;
                    break;
                }
            }
            return value;
        }
    }
}
