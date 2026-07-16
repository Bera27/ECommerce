namespace Ecommerce.Domain.Enums
{
    public enum OrderStatus
    {
        AwaitingPayment,
        Paid,
        InPreparation,
        Shipped,
        Delivered,
        Cancelled,
        Returned
    }
}