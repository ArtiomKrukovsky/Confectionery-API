namespace Confectionery.Domain.Enums
{
    public enum OrderStatus: byte
    {
        Submitted,
        Paid,
        Cooking,
        Shipping,
        Completed,
        Cancelled
    }
}
