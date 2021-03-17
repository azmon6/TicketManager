using System.Collections.Generic;

namespace TicketManager.WebUI.Models
{
    public class ShowCartInfo
    {
        public IEnumerable<CartViewInfo> userCartItems { get; set; }
        public IEnumerable<string> returnedUnavailableItems { get; set; }
    }
}