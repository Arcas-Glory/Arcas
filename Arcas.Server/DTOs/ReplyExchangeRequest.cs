namespace Arcas.Server.DTOs
{
    public class ReplyExchangeRequest
    {
        public string token { get; set; }      // 用户的身份验证 Token
        public int userID { get; set; }        // 用户 ID
        public int exchangeID { get; set; }    // 交换记录 ID
        public string detailsB { get; set; }   // 回复内容（例如支付途径或拒绝理由）
        public bool isAgree { get; set; }      // 是否同意申请
    }
}
