using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Tavstal.TSkinManager.Models
{
    /// <summary>
    /// Defines a time-based cosmetic event that assigns random skins during a day-of-year range.
    /// </summary>
    public class Event
    {
        /// <summary>Display name of the event.</summary>
        [YamlMember(Order = 0)]
        public string EventName { get; set; }
        
        /// <summary>First day of the year (1-365) when this event becomes active.</summary>
        [YamlMember(Order = 1)]
        public int StartDayOfTheYear { get; set; }
        
        /// <summary>Day of the year (1-365) when this event ends (exclusive).</summary>
        [YamlMember(Order = 2)]
        public int EndDayOfTheYear { get; set; }
        
        /// <summary>Possible cosmetic loadouts that can be randomly assigned during the event.</summary>
        [YamlMember(Order = 3)]
        public List<EventSkin> Skins { get; set; }

        /// <summary>
        /// Initializes an empty event with default values.
        /// </summary>
        public Event()
        {
            EventName = string.Empty;
            Skins = new List<EventSkin>();
        }

        /// <summary>
        /// Initializes a new event with all parameters specified.
        /// </summary>
        /// <param name="eventName">Display name of the event.</param>
        /// <param name="startDayOfTheYear">First day of the year when the event starts.</param>
        /// <param name="endDayOfTheYear">Day of the year when the event ends (exclusive).</param>
        /// <param name="skins">Possible cosmetic loadouts for the event.</param>
        public Event(string eventName, int startDayOfTheYear, int endDayOfTheYear, List<EventSkin> skins)
        {
            EventName = eventName;
            StartDayOfTheYear = startDayOfTheYear;
            EndDayOfTheYear = endDayOfTheYear;
            Skins = skins;
        }
    }
}
