using System;

namespace BusinessLayer.DataTransferObject.AnnoncDTO
{
    public class AnnoncResponseDto
    {
        public int Number { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
