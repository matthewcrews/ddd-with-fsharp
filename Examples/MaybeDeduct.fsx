type DaysOfInventory = DaysOfInventory of float with
    static member (+) (DaysOfInventory d1, DaysOfInventory d2) =
        DaysOfInventory (d1 + d2)

module DaysOfInventory =
    let maybeDeduct (DaysOfInventory d2) (d1:DaysOfInventory option) =
        match d1 with
        | Some d -> d1 - d2 |> Some
        | None -> none

let d1 = DaysOfInventory 10.
let d2 = DaysOfInventory 15.

let d2 = DaysOfInventory 20.

let inline maybeSubtract =
    Option.map (-)


let dSub = d1 - d2 - d3