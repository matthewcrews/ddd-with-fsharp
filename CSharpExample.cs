public class StockItem {
    public string InventoryId { get; set; }
    public decimal UnitCost { get; set; }
    public float SalesRate { get; set; }
}

public static class Replenishment {
    public static float PurchaseQuantity(float targetDaysOfInventory, StockItem stockItem){
        var purchaseQuantity = targetDaysOfInventory * stockItem.SalesRate;

        return purchaseQuantity;
    }

    public static float PurchaseValue(float purchaseQuantity, StockItem stockItem){
        var purchaseValue = purchaseQuantity * stockItem.UnitCost;

        return purchaseValue;
    }
}