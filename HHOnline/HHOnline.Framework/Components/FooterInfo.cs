using System;

namespace HHOnline.Framework
{
    public class FooterInfo
    {
        public int ID { get; set; }
        public string AbouteHuaho { get; set; }
        public string ContactInfo { get; set; }
        public string HonerUser { get; set; }
        public string RightNotice { get; set; }
        public string WFList { get; set; }
        public string Recruitment { get; set; }
    }
    public enum FooterUpdateAction
    {
        AbouteHuaho = 0,
        ContactInfo = 1,
        HonerUser = 2,
        RightNotice = 3,
        WFList = 4,
        Recruitment=5
    }
}
