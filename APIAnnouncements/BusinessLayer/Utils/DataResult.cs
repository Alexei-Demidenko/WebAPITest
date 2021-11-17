using System.Collections.Generic;

namespace BusinessLayer.Utils
{
    public class DataResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
    }
}
