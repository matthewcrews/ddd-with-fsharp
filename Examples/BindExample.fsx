type DaysOfInventory = DaysOfInventory of float with
    static member (+) (DaysOfInventory d1, DaysOfInventory d2) =
        DaysOfInventory (d1 + d2)

    static member (-) (DaysOfInventory d1, DaysOfInventory d2) =
        if d1 > d2 then
            DaysOfInventory (d1 - d2)
            |> Some
        else
            None

    static member (-) (d1:DaysOfInventory option, d2:DaysOfInventory) =
        match d1 with
        | Some d -> d - d2
        | None -> None        

module DaysOfInventory =
    let tryCreate daysOfInventory =
        if daysOfInventory > 0. then
            Some (DaysOfInventory daysOfInventory)
        else
            None

let d1 = DaysOfInventory 10.
let d2 = DaysOfInventory 15.
let d3 = DaysOfInventory 20.
let d4 = DaysOfInventory 100.
let dSum = d1 + d2 + d3

let optionSubtract = Option.map (-)
let dSub = 
    d1 - d2
    |> Option.bind ((-) d3)

let dSub2 =
    d4 - d3 - d2 - d1