using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Tavstal.TSkinManager.Models
{
    /// <summary>
    /// Represents a forced cosmetic loadout assigned to a specific player by Steam ID.
    /// </summary>
    public class CustomSkin
    {
        /// <summary>Steam ID of the player this skin is assigned to.</summary>
        [YamlMember(Order = 0)]
        public ulong Player { get; set; }
        
        /// <summary>Item asset ID for the hat slot.</summary>
        [YamlMember(Order = 1)]
        public int Hat { get; set; }
        
        /// <summary>Item asset ID for the glasses slot.</summary>
        [YamlMember(Order = 2)]
        public int Glasses { get; set; }
        
        /// <summary>Item asset ID for the mask slot.</summary>
        [YamlMember(Order = 3)]
        public int Mask { get; set; }
        
        /// <summary>Item asset ID for the backpack slot.</summary>
        [YamlMember(Order = 4)]
        public int Backpack { get; set; }
        
        /// <summary>Item asset ID for the shirt slot.</summary>
        [YamlMember(Order = 5)]
        public int Shirt { get; set; }
        
        /// <summary>Item asset ID for the vest slot.</summary>
        [YamlMember(Order = 6)]
        public int Vest { get; set; }
        
        /// <summary>Item asset ID for the pants slot.</summary>
        [YamlMember(Order = 7)]
        public int Pants { get; set; }
        
        /// <summary>Asset IDs of weapon skins to apply.</summary>
        [YamlMember(Order = 8)]
        public List<int> Weapons { get; set; }

        /// <summary>
        /// Initializes a new custom skin with all slots specified.
        /// </summary>
        /// <param name="p">Steam ID of the player.</param>
        /// <param name="hat">Hat asset ID.</param>
        /// <param name="glasses">Glasses asset ID.</param>
        /// <param name="mask">Mask asset ID.</param>
        /// <param name="backpack">Backpack asset ID.</param>
        /// <param name="shirt">Shirt asset ID.</param>
        /// <param name="vest">Vest asset ID.</param>
        /// <param name="pants">Pants asset ID.</param>
        /// <param name="weapons">Weapon skin asset IDs.</param>
        public CustomSkin(ulong p, int hat, int glasses, int mask, int backpack, int shirt, int vest, int pants, List<int> weapons)
        {
            Player = p;
            Hat = hat;
            Glasses = glasses;
            Mask = mask;
            Backpack = backpack;
            Shirt = shirt;
            Vest = vest;
            Pants = pants;
            Weapons = weapons;
        }

        /// <summary>
        /// Initializes an empty custom skin with default values.
        /// </summary>
        public CustomSkin()
        {
            Weapons = new List<int>();
        }
    }
}
