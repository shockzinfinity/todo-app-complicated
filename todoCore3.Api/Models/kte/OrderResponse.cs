using System;
namespace todoCore3.Api.Models.kte
{
    public class OrderResponse
    {
        public int ItemId { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public string OrderTotal { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentDetail { get; set; }
        public string StatusOrig { get; set; }
        public string CustomerName { get; set; }
        public string National { get; set; }
        public string ProductName { get; set; }
        public string OrderOptions { get; set; } // attribute 로 시작하는 속성 모두 해서 -> <b> 태
        public string NoOfPPL { get; set; }
        public string BookingDate { get; set; }
        public string ReturnDate { get; set; }
        public string PartnerId { get; set; }
        public string PartnerBookingDate { get; set; }
        public string PartnerConfirm { get; set; }
        public string VoucherSendDate { get; set; }
        // tax_invoice -> 세금계산서
        // cc -> CC
        // with_holding -> 원천징수
        // en_invoice -> 인보이스
        public string PartnerEvidence { get; set; }
        public string PartnerAmount { get; set; }
        public string PartnerApproveDate { get; set; }
        public string PartnerPaymentMethod { get; set; }
        public string PartnerPaidDate { get; set; }
        public string IsItemUsed { get; set; }
        public string ItemPrice { get; set; }
        public string ItemPriceDiscounted { get; set; }
        public string PGDate { get; set; }
        public string PGAmount { get; set; }
        public string Margin { get; set; }
        public string MarginPer { get; set; }
        public string MarginReal { get; set; }
        public string MarginRealPer { get; set; }
    }
}
