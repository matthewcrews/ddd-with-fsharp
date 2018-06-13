type Maybe () =
    member this.Bind(opt, func) = opt |> Option.bind func
    member this.Return v = Some v

let maybe = Maybe ()

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
        maybe {
            let! inventoryId = InventoryId.tryCreate inventoryId
            let! unitCost = UnitCost.tryCreate unitCost
            let! salesRate = SalesRate.tryCreate salesRate
            return (create inventoryId unitCost salesRate profitCategory)
        }
