using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.Domain.Entities.Supply;

public class Purchase
{
    public int OrderId { get; private set; }
    public virtual Order? Order { get; private set; }
    public int InputId { get; private set; }
    public virtual Input? Input { get; private set; }
    public int SupplierId { get; private set; }
    public virtual Supplier.Supplier? Supplier { get; private set; }
    public double Amount { get; private set; }
    public decimal UnitaryValue { get; private set; }
    public DateTime? DeliveryDate { get; private set; }
    public int? DeliveryPlaceId { get; private set; }
    public virtual Address DeliveryPlace { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Purchase()
    { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Purchase(int orderId, int inputId, int supplierId, double amount,
        decimal unitaryValue, DateTime deliveryDate, Address deliveryPlace)
    {
        OrderId = orderId;
        InputId = inputId;
        SupplierId = supplierId;
        Amount = amount;
        UnitaryValue = unitaryValue;
        DeliveryDate = deliveryDate;
        DeliveryPlace = deliveryPlace;
    }
}