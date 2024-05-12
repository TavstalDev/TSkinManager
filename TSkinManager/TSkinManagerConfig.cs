using Rocket.API;
using System.Collections.Generic;
using Tavstal.TSkinManager.Compability;

namespace Tavstal.TSkinManager
{
    public class TSkinManagerConfig : IRocketPluginConfiguration
    {
        public bool RestrictWeaponSkins;
        public bool RestrictHats;
        public bool RestrictGlasses;
        public bool RestrictShirts;
        public bool RestrictPants;
        public bool RestrictVests;
        public bool RestrictBackpacks;
        public bool RestrictMasks;
        public bool ReplaceNotAllowedSkins;
        public string BypassPermission;
        public List<Event> eventSkins;
        public List<CustomSkin> CustomSkins;
        public List<string> AllowedSkinColorsHex;

        public void LoadDefaults()
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
            eventSkins = new List<Event>
            {
                new Event { EventName = "Christmas", EventStartDayOfTheYear = 358, EventEndDayOfTheYear = 360, EventSkins = new List<EventSkin>
                {
                    new EventSkin() { Hat = 63501, Backpack = 867, Glasses = 0, Mask = 64601, Pants = 63701, Shirt = 63601, Vest = 64801, Weapons = new List<int> { 64101, 64001, 803, 63901, 63801, 30043, 804 } },
                    new EventSkin() { Hat = 0, Backpack = 0, Glasses = 0, Mask = 64201, Pants = 64401, Shirt = 64301, Vest = 0, Weapons = new List<int> { 64101, 64001, 803, 63901, 63801, 30043, 804 } }
                }},
                new Event { EventName = "Halloween", EventStartDayOfTheYear = 304, EventEndDayOfTheYear = 306, EventSkins = new List<EventSkin>
                {
                    new EventSkin() { Hat = 62401, Backpack = 62501, Glasses = 0, Mask = 40501, Pants = 62701, Shirt = 62601, Vest = 0, Weapons = new List<int> { 40801, 74401, 74601, 74501, 74301, 40201, 40301, 776 } }
                }},
                new Event { EventName = "Test", EventStartDayOfTheYear = 1, EventEndDayOfTheYear = 60, EventSkins = new List<EventSkin>
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
