public class StockItem {
    public string InventoryId { get; }
    public decimal UnitCost { get; }
    public float SalesRate { get; }

    public StockItem(string inventoryId, decimal unitCost, float salesRate){
        InventoryId = inventoryId;
        UnitCost = unitCost;
        SalesRate = salesRate;
    }
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