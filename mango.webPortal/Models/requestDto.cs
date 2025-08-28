using Microsoft.AspNetCore.Mvc;
using static mango.webPortal.Utilities.DT;

namespace mango.webPortal.Models
{
    public class requestDto
    {
        public apiType apiType { get; set; } = apiType.GET;
        public string url { get; set; }
        public object data { get; set; }
        public string accessToken { get; set; }
    }
}
