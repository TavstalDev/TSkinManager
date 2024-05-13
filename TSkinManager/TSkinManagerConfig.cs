using Newtonsoft.Json;
using System.Collections.Generic;
using Tavstal.TLibrary.Compatibility;
using Tavstal.TSkinManager.Compability;

namespace Tavstal.TSkinManager
{
    public class TSkinManagerConfig : ConfigurationBase
    {
        [JsonProperty(Order = 3)]
        public bool RestrictWeaponSkins;
        [JsonProperty(Order = 4)]
        public bool RestrictHats;
        [JsonProperty(Order = 5)]
        public bool RestrictGlasses;
        [JsonProperty(Order = 6)]
        public bool RestrictShirts;
        [JsonProperty(Order = 7)]
        public bool RestrictPants;
        [JsonProperty(Order = 8)]
        public bool RestrictVests;
        [JsonProperty(Order = 9)]
        public bool RestrictBackpacks;
        [JsonProperty(Order = 10)]
        public bool RestrictMasks;
        [JsonProperty(Order = 11)]
        public bool ReplaceNotAllowedSkins;
        [JsonProperty(Order = 12)]
        public string BypassPermission;
        [JsonProperty(Order = 13)]
        public List<Event> EventSkins;
        [JsonProperty(Order = 14)]
        public List<CustomSkin> CustomSkins;
        [JsonProperty(Order = 15)]
        public List<string> AllowedSkinColorsHex;

        public override void LoadDefaults()
        {
            RestrictWeaponSkins = false;
            RestrictGlasses = true;
            RestrictHats = true;
            RestrictShirts = true;
            RestrictPants = true;
            RestrictVests = true;
            RestrictBackpacks = true;
            RestrictMasks = true;
            ReplaceNotAllowedSkins = true;
            BypassPermission = "skinmanager.bypass";
            EventSkins = new List<Event>
            {
                new Event { EventName = "Christmas", StartDayOfTheYear = 358, EndDayOfTheYear = 360, Skins = new List<EventSkin>
                {
                    new EventSkin() { Hat = 63501, Backpack = 867, Glasses = 0, Mask = 64601, Pants = 63701, Shirt = 63601, Vest = 64801, Weapons = new List<int> { 64101, 64001, 803, 63901, 63801, 30043, 804 } },
                    new EventSkin() { Hat = 0, Backpack = 0, Glasses = 0, Mask = 64201, Pants = 64401, Shirt = 64301, Vest = 0, Weapons = new List<int> { 64101, 64001, 803, 63901, 63801, 30043, 804 } }
                }},
                new Event { EventName = "Halloween", StartDayOfTheYear = 304, EndDayOfTheYear = 306, Skins = new List<EventSkin>
                {
                    new EventSkin() { Hat = 62401, Backpack = 62501, Glasses = 0, Mask = 40501, Pants = 62701, Shirt = 62601, Vest = 0, Weapons = new List<int> { 40801, 74401, 74601, 74501, 74301, 40201, 40301, 776 } }
                }},
                new Event { EventName = "Test", StartDayOfTheYear = 1, EndDayOfTheYear = 60, Skins = new List<EventSkin>
                {
                    new EventSkin() { Hat = 1069, Backpack = 0, Glasses = 0, Mask = 1080, Pants = 1068, Shirt = 1071, Vest = 1070, Weapons = new List<int> { 68609, 500405, 500101, 500804, 61908, 67911, 400026, 400027 } }
                }}
            };
            CustomSkins = new List<CustomSkin>
            {
                new CustomSkin { Player = 00000000000000000, Hat = 63501, Backpack = 867, Glasses = 0, Mask = 64601, Pants = 63701, Shirt = 63601, Vest = 64801, Weapons = new List<int> { 64101, 64001, 803, 63901, 63801, 30043, 804 } }
            };
            AllowedSkinColorsHex = new List<string>
            {
                "DEB887", "FFE4E1", "F3CCA5", "FFD5C3", "764A34", "361C0F", "250F04", "E1B37E", "F9DEC0", "150901", "AE6E44",
                "BEA582FF", "F4E6D2FF", "D9CAB4FF", "9D886BFF", "94764BFF", "706049FF", "534736FF", "4B3D31FF", "332C25FF", "231F1CFF",
            };
        }
    }
}
