using System.Collections.Generic;
using Tavstal.TLibrary.Models.Config;
using Tavstal.TLibrary.Models.Logging;
using Tavstal.TSkinManager.Models;
using YamlDotNet.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace Tavstal.TSkinManager
{
    /// <summary>
    /// YAML-serialized configuration for the TSkinManager plugin.
    /// Defines skin restrictions, allowed colors, custom player skins, and time-based events.
    /// </summary>
    public class TSkinManagerConfig : YamlConfiguration
    {
        /// <summary>Which cosmetic slots are restricted for non-bypassed players.</summary>
        [YamlMember(Order = 7, Description = "Which cosmetic slots are restricted for non-bypassed players.")]
        public RestrictionConfig  Restrictions;
        
        /// <summary>Whether to replace disallowed skin colors with a random allowed color.</summary>
        [YamlMember(Order = 8, Description = "Whether to replace disallowed skin colors with a random allowed color.")]
        public bool ReplaceNotAllowedSkins;
        
        /// <summary>Permission node that allows players to bypass all cosmetic restrictions.</summary>
        [YamlMember(Order = 9, Description = "Permission node that allows players to bypass all cosmetic restrictions.")]
        public string BypassPermission;
        
        /// <summary>Time-based events that assign random cosmetic loadouts during specific day ranges.</summary>
        [YamlMember(Order = 10, Description = "Time-based events that assign random cosmetic loadouts during specific day ranges.")]
        public List<Event> EventSkins;
        
        /// <summary>Per-player forced cosmetic loadouts, keyed by Steam ID.</summary>
        [YamlMember(Order = 11, Description = "Per-player forced cosmetic loadouts, keyed by Steam ID.")]
        public List<CustomSkin> CustomSkins;
        
        /// <summary>Hex color strings that players are allowed to use as their skin color.</summary>
        [YamlMember(Order = 12, Description = "Hex color strings that players are allowed to use as their skin color.")]
        public List<string> AllowedSkinColorsHex;

        /// <summary>
        /// Populates the configuration with default values including sample events and allowed skin colors.
        /// </summary>
        public override void LoadDefaults()
        {
            Locale = "en";
            LogLevel = ELogLevel.INFO;
            DownloadLocalePacks = true;
            Restrictions = new RestrictionConfig();
            ReplaceNotAllowedSkins = true;
            BypassPermission = "skinmanager.bypass";
            EventSkins = new List<Event>
            {
                new Event { EventName = "Christmas", StartDayOfTheYear = 358, EndDayOfTheYear = 360, Skins = new List<EventSkin>
                {
                    new EventSkin { Hat = 63501, Backpack = 867, Glasses = 0, Mask = 64601, Pants = 63701, Shirt = 63601, Vest = 64801, Weapons = new List<int> { 64101, 64001, 803, 63901, 63801, 30043, 804 } },
                    new EventSkin { Hat = 0, Backpack = 0, Glasses = 0, Mask = 64201, Pants = 64401, Shirt = 64301, Vest = 0, Weapons = new List<int> { 64101, 64001, 803, 63901, 63801, 30043, 804 } }
                }},
                new Event { EventName = "Halloween", StartDayOfTheYear = 304, EndDayOfTheYear = 306, Skins = new List<EventSkin>
                {
                    new EventSkin { Hat = 62401, Backpack = 62501, Glasses = 0, Mask = 40501, Pants = 62701, Shirt = 62601, Vest = 0, Weapons = new List<int> { 40801, 74401, 74601, 74501, 74301, 40201, 40301, 776 } }
                }},
                new Event { EventName = "Test", StartDayOfTheYear = 1, EndDayOfTheYear = 60, Skins = new List<EventSkin>
                {
                    new EventSkin { Hat = 1069, Backpack = 0, Glasses = 0, Mask = 1080, Pants = 1068, Shirt = 1071, Vest = 1070, Weapons = new List<int> { 68609, 500405, 500101, 500804, 61908, 67911, 400026, 400027 } }
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

        /// <summary>
        /// Initializes a new instance with default values.
        /// </summary>
        public TSkinManagerConfig()
        {
            Restrictions = new RestrictionConfig();
            BypassPermission = "skinmanager.bypass";
            EventSkins = new List<Event>();
            CustomSkins = new List<CustomSkin>();
            AllowedSkinColorsHex = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance with a specified filename and path.
        /// </summary>
        /// <param name="filename">Name of the configuration file.</param>
        /// <param name="path">Directory path where the configuration file is stored.</param>
        public TSkinManagerConfig(string filename, string path) : base(filename, path)
        {
            Restrictions = new RestrictionConfig();
            BypassPermission = "skinmanager.bypass";
            EventSkins = new List<Event>();
            CustomSkins = new List<CustomSkin>();
            AllowedSkinColorsHex = new List<string>();            
        }
    }
}
