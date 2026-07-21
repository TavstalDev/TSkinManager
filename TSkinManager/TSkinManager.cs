using Rocket.Unturned.Permissions;
using SDG.Unturned;
using Steamworks;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Tavstal.TLibrary.Extensions;
using Tavstal.TLibrary.Models.Plugin;
using Tavstal.TLibrary.Helpers.General;
using Tavstal.TLibrary.Models.Logging;
using Tavstal.TSkinManager.Models;
using Tavstal.TSkinManager.Helpers;
using UnityEngine;

namespace Tavstal.TSkinManager
{
    /// <summary>
    /// Core plugin class for TSkinManager. Handles cosmetic enforcement including
    /// skin color filtering, custom player skins, time-based event skins, and slot restrictions.
    /// </summary>
    public class TSkinManager : PluginBase<TSkinManagerConfig>
    {
        /// <summary>
        /// Singleton instance of the plugin.
        /// </summary>
        public static TSkinManager Instance { get; private set; } = null!;
        
        /// <summary>
        /// Prints the plugin banner with version and build information to the console.
        /// </summary>
        public override void OnPreLoad()
        {
            Instance = this;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("────────────────────────────────────────────────────────");
            sb.AppendLine();
            sb.AppendLine("████████╗░██████╗██╗░░██╗██╗███╗░░██╗███╗░░░███╗███╗░░██╗░██████╗░");
            sb.AppendLine("╚══██╔══╝██╔════╝██║░██╔╝██║████╗░██║████╗░████║████╗░██║██╔════╝░");
            sb.AppendLine("░░░██║░░░╚█████╗░█████═╝░██║██╔██╗██║██╔████╔██║██╔██╗██║██║░░██╗░");
            sb.AppendLine("░░░██║░░░░╚═══██╗██╔═██╗░██║██║╚████║██║╚██╔╝██║██║╚████║██║░░╚██╗");
            sb.AppendLine("░░░██║░░░██████╔╝██║░╚██╗██║██║░╚███║██║░╚═╝░██║██║░╚███║╚██████╔╝");
            sb.AppendLine("░░░╚═╝░░░╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░╚══╝╚═╝░░░░░╚═╝╚═╝░░╚══╝░╚═════╝░");
            sb.AppendLine();
            sb.AppendLine("[ About ]");
            sb.AppendLine(" ▸ Developer : Tavstal");
            sb.AppendLine(" ▸ Discord   : @Tavstal");
            sb.AppendLine(" ▸ Website   : https://redstoneplugins.com");
            sb.AppendLine(" ▸ GitHub    : https://github.com/TavstalDev");
            sb.AppendLine();
            sb.AppendLine("[ Build ]");
            sb.AppendLine($" ▸ Version   : {Version}");
            sb.AppendLine($" ▸ Build Date: {BuildDate} UTC");
            sb.AppendLine($" ▸ TLibrary  : {LibraryVersion}");
            sb.AppendLine();
            sb.AppendLine("[ Support ]");
            sb.AppendLine(" ▸ Report issues or request features:");
            sb.AppendLine(" ▸ https://github.com/TavstalDev/TSkinManager/issues");
            sb.AppendLine();
            sb.AppendLine("────────────────────────────────────────────────────────");
            Logger.Log(ELogLevel.COMMAND, sb.ToString(), includePrefixes: false, color:  ConsoleColor.Cyan);
        }

        /// <summary>
        /// Subscribes to the player join event to enable cosmetic enforcement.
        /// </summary>
        public override void OnLoad()
        {
            try
            {
                UnturnedPermissions.OnJoinRequested += PlayerConnectPending;
                Logger.Info($"# {Name} has been successfully loaded.");
            }
            catch (Exception ex)
            {
                Logger.Error($"# Failed to load {Name}...", ex);
            }
        }

        /// <summary>
        /// Unsubscribes from the player join event during plugin unload.
        /// </summary>
        public override void OnUnLoad()
        {
            UnturnedPermissions.OnJoinRequested -= PlayerConnectPending;
            Logger.Info($"# {Name} has been successfully unloaded.");
        }

        /// <summary>
        /// Handles pending player connections by enforcing cosmetic rules.
        /// Applies skin color filtering, custom skins, event skins, and slot restrictions
        /// based on the plugin configuration.
        /// </summary>
        /// <param name="player">Steam ID of the connecting player.</param>
        /// <param name="rej">Rejection reason, can be set to reject the player.</param>
        public void PlayerConnectPending(CSteamID player, ref ESteamRejection? rej)
        {
            foreach (SteamPending steamPending in Provider.pending)
            {
                if (steamPending.playerID.steamID == player)
                {
                    float r = steamPending.skin.r;
                    float g = steamPending.skin.g;
                    float b = steamPending.skin.b;
                    float w = steamPending.skin.a;

                    string playerColorHex = ColorUtility.ToHtmlStringRGBA(new Color (r, g, b, w));

                    if (!Config.AllowedSkinColorsHex.Contains(playerColorHex) && Config.ReplaceNotAllowedSkins)
                    {
                        int index = MathHelper.Next(Config.AllowedSkinColorsHex.Count - 1);
                        string hexColor = Config.AllowedSkinColorsHex.ElementAt(index);
                        if (!hexColor.Contains('#'))
                            hexColor = "#" + hexColor;

                        if (hexColor.Length <= 7)
                            hexColor += "FF";

                        ColorUtility.TryParseHtmlString(hexColor, out Color color);
                        Logger.Warning($"{steamPending.playerID.characterName} does not have acceptable skin color. ({playerColorHex} -> {hexColor})");
                        steamPending.GetType().GetField("_skin", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(steamPending, color);
                    }

                    CustomSkin? skin = Config.CustomSkins.FirstOrDefault(x => x.Player == steamPending.playerID.steamID.m_SteamID);
                    if (skin != null)
                    {
                        steamPending.hatItem = skin.Hat;
                        steamPending.maskItem = skin.Mask;
                        steamPending.glassesItem = skin.Glasses;
                        steamPending.shirtItem = skin.Shirt;
                        steamPending.vestItem = skin.Vest;
                        steamPending.pantsItem = skin.Pants;
                        steamPending.backpackItem = skin.Backpack;
                        steamPending.skinItems = skin.Weapons.ToArray();
                        return;
                    }

                    Event? ev = Config.EventSkins.FirstOrDefault(x => x.StartDayOfTheYear <= DateTime.Now.DayOfYear && x.EndDayOfTheYear > DateTime.Now.DayOfYear);
                    if (ev != null)
                    {
                        if (ev.Skins.Count > 0)
                        {
                            int num = MathHelper.Next(ev.Skins.Count - 1);
                            EventSkin ev2 = ev.Skins.ElementAt(num);
                            steamPending.hatItem = ev2.Hat;
                            steamPending.maskItem = ev2.Mask;
                            steamPending.glassesItem = ev2.Glasses;
                            steamPending.shirtItem = ev2.Shirt;
                            steamPending.vestItem = ev2.Vest;
                            steamPending.pantsItem = ev2.Pants;
                            steamPending.backpackItem = ev2.Backpack;
                            steamPending.skinItems = ev2.Weapons.ToArray();
                            return;
                        }
                    }

                    if (Config.Restrictions.EnableBypass && PermissionHelper.HasPermission(player, Config.Restrictions.BypassPermission))
                        return;

                    if (Config.Restrictions.WeaponSkins)
                    {
                        steamPending.skinItems = Array.Empty<int>();
                        steamPending.packageSkins = Array.Empty<ulong>();
                    }

                    if (Config.Restrictions.Backpacks)
                    {
                        steamPending.packageBackpack = 0UL;
                        steamPending.backpackItem = 0;
                    }

                    if (Config.Restrictions.Hats)
                    {
                        steamPending.packageHat = 0UL;
                        steamPending.hatItem = 0;
                    }

                    if (Config.Restrictions.Masks)
                    {
                        steamPending.packageMask = 0UL;
                        steamPending.maskItem = 0;
                    }

                    if (Config.Restrictions.Pants)
                    {
                        steamPending.packagePants = 0UL;
                        steamPending.pantsItem = 0;
                    }

                    if (Config.Restrictions.Glasses)
                    {
                        steamPending.glassesItem = 0;
                        steamPending.packageGlasses = 0UL;
                    }

                    if (Config.Restrictions.Shirts)
                    {
                        steamPending.packageShirt = 0UL;
                        steamPending.shirtItem = 0;
                    }

                    if (Config.Restrictions.Vests)
                    {
                        steamPending.packageVest = 0UL;
                        steamPending.vestItem = 0;
                    }
                }
            }
        }
    }
}
