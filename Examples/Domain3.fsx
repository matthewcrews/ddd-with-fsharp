open System.Text.RegularExpressions
type ProfitCategory = 
    | Cat1
    | Cat2
    | Cat3    

type ItemQuantity = ItemQuantity of float

module ItemQuantity =
    let zero =
        ItemQuantity 0.0

type OrderQuantity = OrderQuantity of ItemQuantity

module OrderQuantity =
    let ofItemQuantity itemQuantity =
        OrderQuantity itemQuantity

type InventoryId = InventoryId of string

module InventoryId =
    let tryCreate (id:string) =
        let isLettersAndNumbers = Regex.Match(id, "^[a-zA-Z0-9]+$")
        if (isLettersAndNumbers.Success) && id.Length >= 5 && id.Length <= 20 then
            Some (InventoryId id)
        else
            None

type UnitCost = UnitCost of decimal

module UnitCost =
    let tryCreate unitCost =
        if unitCost > 0M && unitCost < 2000M then
            Some (UnitCost unitCost)
        else
            None

type SalesRate = SalesRate of float

module SalesRate =
    let tryCreate salesRate =
        if salesRate >= 0.0 then
            Some (SalesRate salesRate)
        else
            None

type StockItem = {
    InventoryId : InventoryId
    UnitCost : UnitCost
    SalesRate : SalesRate
    ProfitCategory : ProfitCategory
}

module StockItem =
    let create inventoryId unitCost salesRate profitCategory =
        {
            InventoryId = inventoryId
            UnitCost = unitCost
            SalesRate = salesRate
            ProfitCategory = profitCategory
        }

    let tryCreate inventoryId unitCost salesRate profitCategory =
        let inventoryIdResult = InventoryId.tryCreate inventoryId
        let unitCostResult = UnitCost.tryCreate unitCost
        let salesRateResult = SalesRate.tryCreate salesRate

        match inventoryIdResult, unitCostResult, salesRateResult with
        | Some id, Some cost, Some rate ->
            Some (create id cost rate profitCategory)
        | _, _, _ ->
            None


type DaysOfInventory = DaysOfInventory of float with
    static member (+) (DaysOfInventory d1, DaysOfInventory d2) =
        DaysOfInventory (d1 + d2)

    static member (-) (DaysOfInventory d1, DaysOfInventory d2) =
        if d1 > d2 then
            DaysOfInventory (d1 - d2)
            |> Some
        else
            None

    static member (*) (DaysOfInventory d, SalesRate s) =
        ItemQuantity (d * s)

module DaysOfInventory =
    let tryCreate daysOfInventory =
        if daysOfInventory > 0. then
            Some (DaysOfInventory daysOfInventory)
        else
            None

module Replenishment =
    let purchaseQuantity (DaysOfInventory doiTarget) (stockItem:StockItem) =
        let (SalesRate salesRate) = stockItem.SalesRate
        match stockItem.ProfitCategory with
        | Cat1 -> ItemQuantity ((doiTarget + 10.0) * salesRate)
        | Cat2 -> ItemQuantity (doiTarget * salesRate)
        | Cat3 -> ItemQuantity ((doiTarget - 15.0) * salesRate)
        |> OrderQuantity.ofItemQuantity


