# ARCH
This folder contains information about software architecture.

# Entity Diagram
An entity diagram contains all the actions an entity can perform. It is an high-level diagram that is meant to give an overview of an entities capabilities.
## Nodes
Nodes can be distinguished by a "node letter" and their background colour. Example: "r_Player" with a white background would indicate the main entity of the diagram, "Player".
### Root Node
The Root Node is the entity of the diagram. It is represented by a white background (#FFFFFF) and with the letter 'r'.
### Action Group Node
An Action Group Node is a parent for multiple Leaf Nodes. It is used to group multiple actions of the same type under a single node, as to make the diagram more readable. It is represented by a red background (#FF5964) and with the letter 'g'.
### Leaf Node
The Leaf Node is an actual action the entity can perform. It is represented by a yellow background (#FFE74C) and the fact that it has no children.

# Behaviour Tree (BT)
A Behaviour Tree represents the behaviour of an AI Agent. Priorities go from right (highest) to left (lowest).
## Nodes
Nodes can be distinguished by a "node letter" and their background colour. Example: "? (Find Scientist)" with a red background would indicate a selector node whose sub-nodes are related to finding a scientist.
### Flow
Flow Nodes are marked with their respective letters (e.g. "?" for selector, "!" for sequence, etc..), a red background, and a possible description in parentheses.
#### Selector
The Selector Node is marked with a "?".
#### Sequence
The Sequence Node is marked with a "->".
#### Enum Switch
The Enum Switch Node is marked with a "E?".\
##### Enum Value
Enum Values are marked with a yellow background and start with "E_".
#### Boolean Check
Boolean checks are to end with a question mark.
#### Parallel Execution
Parallel executions are to be marked with a "| |" (Use "\<U+007C> \<U+007C>" for it to actually work).
#### Repeat
Repeats are to be marked with "⟳" (\&#10227;). If it's a conditional repeat, then the "⟳" is to be followed by a boolean check.
### Leaf
Flow Nodes are nodes with specific actions that will either: Fail, Run, or Succeed. Leaf nodes are to have a green background and no children.
### TBD
Actions that are to be performed, but don't yet have a valid BT structure are to have a purple background and no children.
### Sub-Behaviour-Tree
Sub-Behaviour-Trees are to be marked with a blue background. The entire Sub-Tree may or may not be represented.