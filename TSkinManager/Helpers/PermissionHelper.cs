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

namespace Tavstal.TSkinManager.Helpers
{
    /// <summary>
    /// A static class providing helper methods for managing permissions in the RocketMod environment.
    /// </summary>
    /// <remarks>
    /// This class interacts with the RocketPermissionsManager to retrieve information about player groups, permissions, and defaults.
    /// </remarks>
    public static class PermissionHelper
    {
        /// <summary>
        /// Gets the instance of the <see cref="RocketPermissionsManager"/>.
        /// </summary>
        static RocketPermissionsManager PermissionsManager => R.Instance.GetComponent<RocketPermissionsManager>();

        /// <summary>
        /// Retrieves the default group ID from the permissions manager.
        /// </summary>
        /// <returns>
        /// The default group ID if found, or an empty string if an error occurs.
        /// </returns>
        private static string GetDefaultGroupID()
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
                TSkinManager.Logger.Exception("Failed to retrieve default group ID from permissions manager.");
                TSkinManager.Logger.Error(ex.ToString());
            }
            return string.Empty;
        }

        /// <summary>
        /// Retrieves a list of permission groups from the permissions manager.
        /// </summary>
        /// <returns>
        /// A list of <see cref="RocketPermissionsGroup"/> representing the available permission groups.
        /// </returns>
        private static List<RocketPermissionsGroup> GetPermissionsGroups()
        {
            try
            {
                FieldInfo helperFieldInfo = PermissionsManager.GetType().GetField("helper", BindingFlags.NonPublic | BindingFlags.Instance);
                object helperObject = helperFieldInfo?.GetValue(PermissionsManager);
                Type helperType = helperObject?.GetType();
                if (helperType == null)
                    return new List<RocketPermissionsGroup>();
                Asset<RocketPermissions> permissions = (Asset<RocketPermissions>)(helperType.GetField("permissions", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(helperObject));
                return permissions?.Instance.Groups;
            }
            catch (Exception ex)
            {
                TSkinManager.Logger.Exception("Failed to retrieve permission groups from permissions manager.");
                TSkinManager.Logger.Error(ex.ToString());
            }

            return new List<RocketPermissionsGroup>();
        }
        
        /// <summary>
        /// Retrieves a list of permission groups for a specific player based on their Steam ID.
        /// </summary>
        /// <param name="steamID">The Steam ID of the player.</param>
        /// <returns>
        /// A list of <see cref="RocketPermissionsGroup"/> that the player belongs to.
        /// </returns>
        private static List<RocketPermissionsGroup> GetPlayerGroups(CSteamID steamID)
        {
            string defaultId = GetDefaultGroupID();
            return GetPermissionsGroups().FindAll(x => x.Id == defaultId || x.Members.Contains(steamID.m_SteamID.ToString()));
        }

        /// <summary>
        /// Checks if a player has a specific permission.
        /// </summary>
        /// <param name="steamID">The Steam ID of the player.</param>
        /// <param name="permission">The permission to check for.</param>
        /// <returns>
        /// <c>true</c> if the player has the specified permission, otherwise <c>false</c>.
        /// </returns>
        public static bool HasPermission(CSteamID steamID, string permission)
        {
            if (SteamAdminlist.checkAdmin(steamID))
                return true;
            
            bool value = false;
            foreach (RocketPermissionsGroup group in GetPlayerGroups(steamID))
            {
                if (group.Permissions.Any(x => string.Equals(x.Name, permission, StringComparison.CurrentCultureIgnoreCase)))
                {
                    value = true;
                    break;
                }
            }
            return value;
        }
    }
}
