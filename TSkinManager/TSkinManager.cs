using Rocket.API.Collections;
using Rocket.Unturned.Permissions;
using SDG.Unturned;
using Steamworks;
using System;
using System.Linq;
using System.Reflection;
using Tavstal.TLibrary.Compatibility;
using Tavstal.TLibrary.Helpers.General;
using Tavstal.TSkinManager.Compability;
using Tavstal.TSkinManager.Helpers;
using UnityEngine;

namespace Tavstal.TSkinManager
{
    public class TSkinManager : PluginBase<TSkinManagerConfig>
    {
        public static new TSkinManager Instance;

        public override void OnLoad()
        {
            Instance = this;

            Logger.LogWarning("████████╗░██████╗██╗░░██╗██╗███╗░░██╗███╗░░░███╗███╗░░██╗░██████╗░");
            Logger.LogWarning("╚══██╔══╝██╔════╝██║░██╔╝██║████╗░██║████╗░████║████╗░██║██╔════╝░");
            Logger.LogWarning("░░░██║░░░╚█████╗░█████═╝░██║██╔██╗██║██╔████╔██║██╔██╗██║██║░░██╗░");
            Logger.LogWarning("░░░██║░░░░╚═══██╗██╔═██╗░██║██║╚████║██║╚██╔╝██║██║╚████║██║░░╚██╗");
            Logger.LogWarning("░░░██║░░░██████╔╝██║░╚██╗██║██║░╚███║██║░╚═╝░██║██║░╚███║╚██████╔╝");
            Logger.LogWarning("░░░╚═╝░░░╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░╚══╝╚═╝░░░░░╚═╝╚═╝░░╚══╝░╚═════╝░");
            Logger.Log("#########################################");
            Logger.Log("# Thanks for using my plugin");
            Logger.Log("# Plugin Created By Tavstal");
            Logger.Log("# Discord: Tavstal#6189");
            Logger.Log("# Website: https://redstoneplugins.com");
            Logger.Log("#########################################");
            Logger.Log(string.Format("# Build Version: {0}", Version));
            Logger.Log(string.Format("# Build Date: {0}", BuildDate));
            Logger.Log("#########################################");
            Logger.Log("# Loading TSKinManager...");

            try
            {
                UnturnedPermissions.OnJoinRequested += PlayerConnectPending;
                var ev = Config.EventSkins.FirstOrDefault(x => x.StartDayOfTheYear <= DateTime.Now.DayOfYear && x.EndDayOfTheYear > DateTime.Now.DayOfYear);
                string even = "None";
                if (ev != null)
                    even = ev.EventName;

                Logger.Log("# TSKinManager has been loaded.");
            }
            catch (Exception ex)
            {
                Logger.LogException("# Failed to load TSKinManager...");
                Logger.LogError(ex);
            }
           
        }

        public override void OnUnLoad()
        {
            UnturnedPermissions.OnJoinRequested -= PlayerConnectPending;
            Logger.Log("# TSkinManager has been unloaded");
        }

        public void PlayerConnectPending(CSteamID Player, ref ESteamRejection? REj)
        {
            foreach (SteamPending steamPending in Provider.pending)
            {
                if (steamPending.playerID.steamID == Player)
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
                        Logger.LogWarning(String.Format("{0} does not have acceptable skin color. ({1} -> {2})", steamPending.playerID.characterName, playerColorHex, hexColor));
                        steamPending.GetType().GetField("_skin", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(steamPending, color);
                    }

                    CustomSkin skin = Config.CustomSkins.FirstOrDefault(x => x.Player == steamPending.playerID.steamID.m_SteamID);
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

                    Event ev = Config.EventSkins.FirstOrDefault(x => x.StartDayOfTheYear <= DateTime.Now.DayOfYear && x.EndDayOfTheYear > DateTime.Now.DayOfYear);
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

                    if (PermissionHelper.HasPermission(Player, Config.BypassPermission))
                        return;

                    if (Config.RestrictWeaponSkins)
                    {
                        steamPending.skinItems = new int[0];
                        steamPending.packageSkins = new ulong[0];
                    }

                    if (Config.RestrictBackpacks)
                    {
                        steamPending.packageBackpack = 0UL;
                        steamPending.backpackItem = 0;
                    }

                    if (Config.RestrictHats)
                    {
                        steamPending.packageHat = 0UL;
                        steamPending.hatItem = 0;
                    }

                    if (Config.RestrictMasks)
                    {
                        steamPending.packageMask = 0UL;
                        steamPending.maskItem = 0;
                    }

                    if (Config.RestrictPants)
                    {
                        steamPending.packagePants = 0UL;
                        steamPending.pantsItem = 0;
                    }

                    if (Config.RestrictGlasses)
                    {
                        steamPending.glassesItem = 0;
                        steamPending.packageGlasses = 0UL;
                    }

                    if (Config.RestrictShirts)
                    {
                        steamPending.packageShirt = 0UL;
                        steamPending.shirtItem = 0;
                    }

                    if (Config.RestrictVests)
                    {
                        steamPending.packageVest = 0UL;
                        steamPending.vestItem = 0;
                    }
                }
            }
        }
    }
}
