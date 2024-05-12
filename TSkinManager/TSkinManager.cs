using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Permissions;
using SDG.Unturned;
using Steamworks;
using System;
using System.Linq;
using System.Reflection;
using Tavstal.TSkinManager.Compability;
using Tavstal.TSkinManager.Helpers;
using UnityEngine;
using Logger = Tavstal.TSkinManager.Helpers.LoggerHelper;

namespace Tavstal.TSkinManager
{
    public class TSkinManager : RocketPlugin<TSkinManagerConfig>
    {
        public static TSkinManager Instance;
        internal System.Version _Version => Assembly.GetExecutingAssembly().GetName().Version;
        internal DateTime _BuildDate => new DateTime(2000, 1, 1).AddDays(_Version.Build).AddSeconds(_Version.Revision * 2);

        private System.Random random;
        private object syncObj = new object();
        private void InitRandomNumber(int seed)
        {
            random = new System.Random(seed);
        }
        private int GenerateRandomNumber(int max)
        {
            lock (syncObj)
            {
                if (random == null)
                    random = new System.Random(); // Or exception...
                return random.Next(max);
            }
        }

        protected override void Load()
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
            Logger.Log(string.Format("# Build Version: {0}", _Version));
            Logger.Log(string.Format("# Build Date: {0}", _BuildDate));
            Logger.Log("#########################################");
            Logger.Log("# Loading TSKinManager...");

            try
            {
                UnturnedPermissions.OnJoinRequested += PlayerConnectPending;
                var ev = Configuration.Instance.eventSkins.FirstOrDefault(x => x.EventStartDayOfTheYear <= DateTime.Now.DayOfYear && x.EventEndDayOfTheYear > DateTime.Now.DayOfYear);
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

        protected override void Unload()
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
                    var config = Configuration.Instance;
                    float r = steamPending.skin.r;
                    float g = steamPending.skin.g;
                    float b = steamPending.skin.b;
                    float w = steamPending.skin.a;

                    string playerColorHex = ColorUtility.ToHtmlStringRGBA(new Color (r, g, b, w));

                    if (!config.AllowedSkinColorsHex.Contains(playerColorHex) && config.ReplaceNotAllowedSkins)
                    {
                        int index = GenerateRandomNumber(config.AllowedSkinColorsHex.Count - 1);
                        string hexColor = config.AllowedSkinColorsHex.ElementAt(index);
                        if (!hexColor.Contains('#'))
                            hexColor = "#" + hexColor;

                        if (hexColor.Length <= 7)
                            hexColor += "FF";

                        ColorUtility.TryParseHtmlString(hexColor, out Color color);
                        Logger.LogWarning(String.Format("{0} does not have acceptable skin color. ({1} -> {2})", steamPending.playerID.characterName, playerColorHex, hexColor));
                        steamPending.GetType().GetField("_skin", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(steamPending, color);
                    }

                    var skin = config.CustomSkins.FirstOrDefault(x => x.Player == steamPending.playerID.steamID.m_SteamID);
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

                    var ev = config.eventSkins.FirstOrDefault(x => x.EventStartDayOfTheYear <= DateTime.Now.DayOfYear && x.EventEndDayOfTheYear > DateTime.Now.DayOfYear);
                    if (ev != null)
                    {
                        if (ev.EventSkins.Count > 0)
                        {
                            int num = GenerateRandomNumber(ev.EventSkins.Count - 1);
                            EventSkin ev2 = ev.EventSkins.ElementAt(num);
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

                    if (PermissionHelper.HasPermission(Player, config.BypassPermission))
                        return;

                    if (config.RestrictWeaponSkins)
                    {
                        steamPending.skinItems = new int[0];
                        steamPending.packageSkins = new ulong[0];
                    }

                    if (config.RestrictBackpacks)
                    {
                        steamPending.packageBackpack = 0UL;
                        steamPending.backpackItem = 0;
                    }

                    if (config.RestrictHats)
                    {
                        steamPending.packageHat = 0UL;
                        steamPending.hatItem = 0;
                    }

                    if (config.RestrictMasks)
                    {
                        steamPending.packageMask = 0UL;
                        steamPending.maskItem = 0;
                    }

                    if (config.RestrictPants)
                    {
                        steamPending.packagePants = 0UL;
                        steamPending.pantsItem = 0;
                    }

                    if (config.RestrictGlasses)
                    {
                        steamPending.glassesItem = 0;
                        steamPending.packageGlasses = 0UL;
                    }

                    if (config.RestrictShirts)
                    {
                        steamPending.packageShirt = 0UL;
                        steamPending.shirtItem = 0;
                    }

                    if (config.RestrictVests)
                    {
                        steamPending.packageVest = 0UL;
                        steamPending.vestItem = 0;
                    }
                }
            }
        }

        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                { "", "" },
            };
    }
}
