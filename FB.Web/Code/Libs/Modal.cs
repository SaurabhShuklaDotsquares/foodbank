using FB.Core;

namespace FB.Web
{
    public class Modal
    {
        public Modal()
        {
            Backdrop = "true";
        }
        public string ID { get; set; }
        public string AreaLabeledId { get; set; }
        public ModalSize Size { get; set; }
        public string Message { get; set; }
        public string Backdrop { get; set; }
        public string ModalSizeClass
        {
            get
            {
                switch (this.Size)
                {
                    case ModalSize.ExtraLarge:
                        return "modal-xl";
                    case ModalSize.Small:
                        return "modal-sm";
                    case ModalSize.Large:
                        return " modal-lg";
                    case ModalSize.Medium:
                    default:
                        return "";
                }
            }
        }

        public ModalHeader Header { get; set; }
        public ModalFooter Footer { get; set; }
        public int HouseHoldID { get; set; }
    }

    public class ModalHeader
    {
        public ModalHeader()
        {
            ShowCloseButton = true;
        }
        public string Heading { get; set; }
        public bool ShowCloseButton { get; set; }
    }

    public class ModalFooter
    {
        public ModalFooter()
        {
            SubmitButtonText = "Save";
            CancelButtonText = "Cancel";
            SubmitButtonID = "btn-submit";
            CancelButtonID = "btn-cancel";
        }
        public string SubmitButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string SubmitButtonID { get; set; }
        public string CancelButtonID { get; set; }
        public bool OnlyCancelButton { get; set; }
        public bool OnlySubmitButton { get; set; }
    }
}
