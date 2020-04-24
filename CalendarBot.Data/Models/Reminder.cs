using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarBot.Data.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string ServerId { get; set; }
        public string ChannelId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string JobId { get; set; }
    }
}
