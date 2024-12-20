using Arcas.Server.Models;
namespace Arcas.Server.DTOs
    
{
    public class EmailGetResponse
    {
        public string vertify { get; set; }  
        public List<ExchangeDetail> emails { get; set; }    
    }
}
