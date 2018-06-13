type InventoryId = InventoryId of string
open System.Runtime.InteropServices.ComTypes

module InventoryId =
    let tryCreate (id:string) =
        if id.Length >= 5 && id.Length <= 20 then
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
}

module StockItem =
    let create inventoryId unitCost salesRate =
        {
            InventoryId = inventoryId
            UnitCost = unitCost
            SalesRate = salesRate
        }

    let tryCreate inventoryId unitCost salesRate =
        let inventoryIdResult = InventoryId.tryCreate inventoryId
        let unitCostResult = UnitCost.tryCreate unitCost
        let salesRateResult = SalesRate.tryCreate salesRate

        match inventoryIdResult, unitCostResult, salesRateResult with
        | Some id, Some cost, Some rate ->
            Some (create id cost rate)
        | _, _, _ ->
            None


type DaysOfInventory = DaysOfInventory of float

module DaysOfInventory =
    let tryCreate daysOfInventory =
        if daysOfInventory > 0. then
            Some (DaysOfInventory daysOfInventory)
        else
            None        

module Replenishment =
    let purchaseQuantity doiTarget (stockItem:StockItem) =
        let quantity (DaysOfInventory doi) (SalesRate rate) =
            doi * rate

        quantity doiTarget stockItem.SalesRate        

type ProfitCategory = 
    | Cat1
    | Cat2
    | Cat3    