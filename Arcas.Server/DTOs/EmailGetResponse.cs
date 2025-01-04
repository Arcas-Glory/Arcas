using Arcas.Server.Models;
namespace Arcas.Server.DTOs
    
{
    public class ExchangeDetailDTO
    {
        public int exchangeID { get; set; }
        public string nickname { get; set; }
        public string IDA { get; set; }
        public string IDB { get; set; }
        public int bookID { get; set; }
        public string trackingID { get; set; }
        public bool isApproved { get; set; }
        public string content { get; set; }
        public string detailsA { get; set; }
        public string detailsB { get; set; }
    }

    public class EmailGetResponse
    {
        public string vertify { get; set; }  
        public List<ExchangeDetailDTO> emails { get; set; }    
    }
}
