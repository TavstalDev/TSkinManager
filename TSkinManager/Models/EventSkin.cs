using System.Collections.Generic;

namespace Tavstal.TSkinManager.Models
{
    public class EventSkin
    {
        public int Hat { get; set; }
        public int Glasses { get; set; }
        public int Mask { get; set; }
        public int Backpack { get; set; }
        public int Shirt { get; set; }
        public int Vest { get; set; }
        public int Pants { get; set; }
        public List<int> Weapons { get; set; }

        public EventSkin(int hat, int glasses, int mask, int backpack, int shirt, int vest, int pants, List<int> weapons)
        {
            Hat = hat;
            Glasses = glasses;
            Mask = mask;
            Backpack = backpack;
            Shirt = shirt;
            Vest = vest;
            Pants = pants;
            Weapons = weapons;
        }

        public EventSkin() { }
    }
}
