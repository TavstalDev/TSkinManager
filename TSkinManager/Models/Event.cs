using System.Collections.Generic;

namespace Tavstal.TSkinManager.Compability
{
    public class Event
    {
        public string EventName { get; set; }
        public int EventStartDayOfTheYear { get; set; }
        public int EventEndDayOfTheYear { get; set; }
        public List<EventSkin> EventSkins { get; set; }
    }
}
