# Domain Driven Design with FSharp

## Introduction

### What we are going to cover

- What is DDD
- What is the benefit of DDD
- Why is F# uniquely suited for DDD
- Application of DDD with F# do a financial domain
- Gotchas of DDD and overcoming them

### Who am I?

- Matthew Crews, Developer and Optimization Engineer
- Favorite language is whatever solves the problem well (F#, C#, C++, SQL, VBA, whatever)
- F# is my primary language but I still consider myself novice to Functional Programming
- I like clear, robust, and elegant code (in that order)

## Domain Driven Design

### Where does it come from?

- The term "Domain Driven Design" comes from Eric Evans. It was used in his book of the the same title (citation).
- "The purpose of abstraction is not to be vague, but to create a new semantic level in which one can be absolutely precise." Edsger Dijkstra

### What's the high level idea?

- Context: The conceptual umbrella that everything falls under
- Domain: Define a clear boundary for what the project or solution is about so that you can say "No" to everything not in that boundary
- Model: The set of abstractions used to solve problems in the given Domain
- Ubiquitous Language: The consistent way of describing objects and activities within the Context that is shared with the Developers and Domain Experts (What we have here is a failure to communicate)

> Note: If an object could potentially have two different meaning in the same Context, then the Context may be too broad

### What are the tools of DDD?

- Entities: An object which is identified by its identity. (ie: Purchase Order Line, Customer, Sales Order Line...)
- Value Object: An object which has value but no identity. Immutable by default. (Shipping Address)
- Aggregate: A collection of Entities which are bound together and must be treated as a whole. (ie: Purchase Order, Sales Order)
- Domain Event: An event which the domain expert cares about. (ie: Purchase Order Placed, Purchase Order Shipped, Sales Order Placed, ...)
- Service: An operation that does not belong to any object. (ie: Purchase Order Placement Service, Shipment Processing Service)
- Repository: A method for retrieving Domain Objects which is storage method agnostic (SQL DB Client, Document Store Client)
- Factory: An object (or function) for creating Domain Objects which can be swapped out

### Why should I care?

- By accurately modeling the domain, it is easier to discuss the solution with Domain Experts and Business Users
- Helps with refactoring because the domain is more fully represented

## F# and it's aptness for DDD

### What is F#?

### What separates Functional from Imperitive Programming?

### Why do I care about Functional Programming?

- If you write SQL, you are a functional programmer 

### So what makes F# great for DDD?

- Easy to define many types
- Type aliases
- Sane type comparison
- Algebraic type system (more on that later)
- Robust type checking
- Match statement forces handling of cases in all instances. If you add a new possible state, it will break every match statement that doesn't cover it.

### What is up with these Algebraic Types?

### Product Types: What we are used to. Tuples and Records

### Sum Types (aka Discrimiated Union): A type which enforces dealing with various sub-types. Vegetable could be a 