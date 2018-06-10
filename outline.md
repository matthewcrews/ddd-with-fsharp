# Domain Driven Design with FSharp

## Introduction

---

### What we are going to cover

- What is DDD
- What is the benefit of DDD
- Why is F# uniquely suited for DDD
- Application of DDD with F# do a financial domain
- Gotchas of DDD and overcoming them

---

### Who am I?

- Matthew Crews, Developer and Optimization Engineer
- Favorite language is whatever solves the problem well (F#, C#, C++, SQL, VBA, whatever)
- F# is my primary language but I still consider myself novice to Functional Programming
- I like clear, robust, and elegant code (in that order)

---

## Domain Driven Design

### Where does it come from?

- The term "Domain Driven Design" comes from Eric Evans. It was used in his book of the the same title (citation).
> "The purpose of abstraction is not to be vague, but to create a new semantic level in which one can be absolutely precise."
>> **Edsger Dijkstra**

---

### What's the high level idea?

- Context: The conceptual umbrella that everything falls under
- Domain: Define a clear boundary for what the project or solution is about so that you can say "No" to everything not in that boundary
- Model: The set of abstractions used to solve problems in the given Domain
- Ubiquitous Language: The consistent way of describing objects and activities within the Context that is shared with the Developers and Domain Experts (What we have here is a failure to communicate)

> Note: If an object could potentially have two different meaning in the same Context, then the Context may be too broad

---

### What are the tools of DDD?

- Entities: An object which is identified by its identity. (ie: Purchase Order Line, Customer, Sales Order Line...)
- Value Object: An object which has value but no identity. Immutable by default. (Shipping Address)
- Aggregate: A collection of Entities which are bound together and must be treated as a whole. (ie: Purchase Order, Sales Order)
- Domain Event: An event which the domain expert cares about. (ie: Purchase Order Placed, Purchase Order Shipped, Sales Order Placed, ...)
- Service: An operation that does not belong to any object. (ie: Purchase Order Placement Service, Shipment Processing Service)
- Repository: A method for retrieving Domain Objects which is storage method agnostic (SQL DB Client, Document Store Client)
- Factory: An object (or function) for creating Domain Objects which can be swapped out

---

### Why should I care?

- By accurately modeling the domain, it is easier to discuss the solution with Domain Experts and Business Users
- Helps with refactoring because the domain is more fully represented

---

## F# and it's aptness for DDD

---

### What is F#?

- A Functional-First, statically typed language 
- First released in 2005
- Runs on top of the .NET Runtime (cross platform)
- An ancestor of OCaml
- It is a pragmatic functional language (ie: you can write non-Functional Code)
- Great for OO and Procedural Programming
- Same performance as C#

### What separates Functional from Imperitive Programming?

- Functional Programming is expressions instead of commands
- Everything can be thought of as a function
- All code returns something, even if that something is `unit` (The closest thing to `void` in F#)
- Functions are first class citizens and are often parameters to other functions

### Why do I care about Functional Programming?

- If you write SQL, you are a functional programmer
- If you write LINQ expressions, you are a functional programmer


### So what makes F# great for DDD?

- Zero friction for defining many types
- Type aliases
- Value based comparison
- Algebraic type system (more on that later)
- Robust type inference (not checking)
- Match statement forces handling of cases in all instances. If you add a new possible state, it will break every match statement that doesn't cover it.

### What is up with these Algebraic Types?

### Product Types: What we are used to. Tuples and Records

### Sum Types (aka Discrimiated Union): A type which enforces dealing with various sub-types. Vegetable could be a 

## Application to Financial Domain

### The Problem Statement

- We need to create a domain for performing costing calculations
- We cannot afford making a poor recommendation
- The model needs to be easy to maintain since strategies evolve
- Ideally we want to model the restrictions of our domain within the types themselves
- Relying on a Developer to remember to validate a number will fail (ie: Checking for non-negativity)

### The Domain

- Stock Items: The items that we sell on Marketplaces. These represent physical units of inventory
- Floor Price: The Lowest Price which we can afford to sell an item for
- Days of Inventory: The number of days which an item is in stock
- Sales Rate: The daily rate which we have or expect to make sales

---

### Stock Item Model

```csharp
public class StockItem {
    public string InventoryId { get; set; }
    public decimal UnitCost { get; set; }
    public float SalesRate { get; set; }
}
```

Question

- Is any string an acceptable `InventoryId`?
- Can a `UnitCost` really take on any value a `decimal` can?
- Do we ever expect to see a `SalesRate` less than `0.0`?
- What can we do to restrict the domains of these values?

---

### Stock Item Model

- For proper DDD we would like to restrict the values of `InventoryId`, `UnitCost`, and `SalesRate`. Let's look at doing this in F#

Questions for the Domain Expert:
Q: What are the valid values for an `InventoryId`?
A: Well, it's always letters and numbers.
Q: Okay, can it be just a single letter?
A: No, it is always at least 5
Q: Can it be an infinite number of letters or numbers?
A: Oh, no it is never more than 20

```fsharp
type InventoryId = InventoryId of string

module InventoryId =
    let tryCreate (id:string) =
        if id.Length > 5 && id.Length <= 50 then
            Some (InventoryId id)
        else
            None
```

---

### Updated Requirement

> "We want to adjust the DOI Target for our StockItems based on their Profit Category. Cat 1 should get a 10 DOI Bonus. Cat 2 should get no bonus. Cat 3 should get a 15 DOI Penalty." --The Boss
 
New Term for the Ubiquitous Language  
Profit Category: The profit grouping that a Stock Item belongs to

## Resources

### Books

"Domain Driven Design" by Eric Evans  
"Domain Driven Design Made Functional" by Scott Wlaschin  
"Get Programming with F#" by Isaac Abraham

### Website

fsharpforfunandprofit.com by Scott Wlaschin  
fsharp.org