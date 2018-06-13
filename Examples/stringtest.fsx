open System.Text.RegularExpressions

type InventoryId = InventoryId of string

module InventoryId =
    let tryCreate (id:string) =
        let isLettersAndNumbers = Regex.Match(id, "^[a-zA-Z0-9]+$")
        if (isLettersAndNumbers.Success) && id.Length >= 5 && id.Length <= 20 then
            Some (InventoryId id)
        else
            None


let t1 = InventoryId.tryCreate "abc1234"
let t2 = InventoryId.tryCreate "-123456"
let t3 = InventoryId.tryCreate "ABC1234"