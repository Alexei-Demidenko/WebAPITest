using System.Collections.Generic;

namespace BusinessLayer.Utils
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IEnumerable<T> Result { get; set; }
    }
}
