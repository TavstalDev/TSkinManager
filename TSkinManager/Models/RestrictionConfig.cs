using YamlDotNet.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace Tavstal.TSkinManager.Models
{
    /// <summary>
    /// Configuration model defining which cosmetic slots are restricted for non-bypassed players.
    /// When a slot is restricted, the corresponding cosmetic is stripped on join.
    /// </summary>
    public class RestrictionConfig
    {
        [YamlMember(Order = 0, Description = "Enables bypassing the restrictions.")]
        public bool EnableBypass { get; set; }
        
        [YamlMember(Order = 1, Description = "Permission node that allows players to bypass all cosmetic restrictions.")]
        public string BypassPermission { get; set; }
        
        [YamlMember(Order = 2, Description = "Whether weapon skins are restricted.")]
        public bool WeaponSkins { get; set; }
        
        [YamlMember(Order = 3, Description = "Whether hats are restricted.")]
        public bool Hats { get; set; }
        
        [YamlMember(Order = 4, Description = "Whether glasses are restricted.")]
        public bool Glasses { get; set; }

        [YamlMember(Order = 5, Description = "Whether shirts are restricted.")]
        public bool Shirts { get; set; }
        
        [YamlMember(Order = 6, Description = "Whether pants are restricted.")]
        public bool Pants { get; set; }
        
        [YamlMember(Order = 7, Description = "Whether vests are restricted.")]
        public bool Vests { get; set; }
        
        [YamlMember(Order = 8, Description = "Whether backpacks are restricted.")]
        public bool Backpacks { get; set; }
        
        [YamlMember(Order = 9, Description = "Whether masks are restricted.")]
        public bool Masks { get; set; }

        /// <summary>
        /// Initializes default restrictions: all clothing slots restricted, weapon skins allowed.
        /// </summary>
        public RestrictionConfig()
        {
            EnableBypass = false;
            BypassPermission = "skinmanager.bypass";
            WeaponSkins = false;
            Glasses = true;
            Hats = true;
            Shirts = true;
            Pants = true;
            Vests = true;
            Backpacks = true;
            Masks = true;
        }

        /// <summary>
        /// Initializes a restriction config with explicit settings for each slot.
        /// </summary>
        /// <param name="enableBypass">Whether enable bypassing.</param>
        /// <param name="bypassPermission">The permission needed to bypass restrictions.</param>
        /// <param name="weaponSkins">Whether weapon skins are restricted.</param>
        /// <param name="hats">Whether hats are restricted.</param>
        /// <param name="glasses">Whether glasses are restricted.</param>
        /// <param name="shirts">Whether shirts are restricted.</param>
        /// <param name="pants">Whether pants are restricted.</param>
        /// <param name="vests">Whether vests are restricted.</param>
        /// <param name="backpacks">Whether backpacks are restricted.</param>
        /// <param name="masks">Whether masks are restricted.</param>
        public RestrictionConfig(bool enableBypass, string bypassPermission, bool weaponSkins, bool hats, bool glasses, bool shirts, bool pants, bool vests, bool backpacks, bool masks)
        {
            EnableBypass = enableBypass;
            BypassPermission = bypassPermission;
            WeaponSkins = weaponSkins;
            Hats = hats;
            Glasses = glasses;
            Shirts = shirts;
            Pants = pants;
            Vests = vests;
            Backpacks = backpacks;
            Masks = masks;
        }
    }
}