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
        /// <summary>Whether weapon skins are restricted.</summary>
        [YamlMember(Order = 0, Description = "Whether weapon skins are restricted.")]
        public bool WeaponSkins { get; set; }
        
        /// <summary>Whether hats are restricted.</summary>
        [YamlMember(Order = 1, Description = "Whether hats are restricted.")]
        public bool Hats { get; set; }
        
        /// <summary>Whether glasses are restricted.</summary>
        [YamlMember(Order = 2, Description = "Whether glasses are restricted.")]
        public bool Glasses { get; set; }
        
        /// <summary>Whether shirts are restricted.</summary>
        [YamlMember(Order = 3, Description = "Whether shirts are restricted.")]
        public bool Shirts { get; set; }
        
        /// <summary>Whether pants are restricted.</summary>
        [YamlMember(Order = 4, Description = "Whether pants are restricted.")]
        public bool Pants { get; set; }
        
        /// <summary>Whether vests are restricted.</summary>
        [YamlMember(Order = 5, Description = "Whether vests are restricted.")]
        public bool Vests { get; set; }
        
        /// <summary>Whether backpacks are restricted.</summary>
        [YamlMember(Order = 6, Description = "Whether backpacks are restricted.")]
        public bool Backpacks { get; set; }
        
        /// <summary>Whether masks are restricted.</summary>
        [YamlMember(Order = 7, Description = "Whether masks are restricted.")]
        public bool Masks { get; set; }

        /// <summary>
        /// Initializes default restrictions: all clothing slots restricted, weapon skins allowed.
        /// </summary>
        public RestrictionConfig()
        {
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
        /// <param name="restrictWeaponSkins">Whether weapon skins are restricted.</param>
        /// <param name="restrictHats">Whether hats are restricted.</param>
        /// <param name="restrictGlasses">Whether glasses are restricted.</param>
        /// <param name="restrictShirts">Whether shirts are restricted.</param>
        /// <param name="restrictPants">Whether pants are restricted.</param>
        /// <param name="restrictVests">Whether vests are restricted.</param>
        /// <param name="backpacks">Whether backpacks are restricted.</param>
        /// <param name="masks">Whether masks are restricted.</param>
        public RestrictionConfig(bool restrictWeaponSkins, bool restrictHats, bool restrictGlasses, bool restrictShirts, bool restrictPants, bool restrictVests, bool backpacks, bool masks)
        {
            WeaponSkins = restrictWeaponSkins;
            Hats = restrictHats;
            Glasses = restrictGlasses;
            Shirts = restrictShirts;
            Pants = restrictPants;
            Vests = restrictVests;
            Backpacks = backpacks;
            Masks = masks;
        }
    }
}