using FB.Core;

namespace FB.Web
{
    public class Notification
    {
        public string Heading { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public string Icon
        {
            get
            {
                switch (this.Type)
                {
                    case MessageType.Warning:
                        return "fa-warning";
                    case MessageType.Success:
                        return "fa-check";
                    case MessageType.Danger:
                        return "fa-times";
                    case MessageType.Info:
                        return "fa-info";
                    default:
                        return "fa-info";
                }
            }
        }
    }
}
