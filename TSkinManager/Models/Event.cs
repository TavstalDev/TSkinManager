using System.Collections.Generic;

namespace Tavstal.TSkinManager.Models
{
    public class Event
    {
        public string EventName { get; set; }
        public int StartDayOfTheYear { get; set; }
        public int EndDayOfTheYear { get; set; }
        public List<EventSkin> Skins { get; set; }
    }
}
