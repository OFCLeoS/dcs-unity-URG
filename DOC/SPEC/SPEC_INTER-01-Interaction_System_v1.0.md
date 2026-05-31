# Interactable
---------
Every item that can be interacted by the player will be implement the "Interactable" interface.\
The "Interactable" interface will contain a single method signature, "OnInteract". This method will be called when the player interacts with the object by pressing the interaction key.\
To know when a player is looking at an interactible, and for performance sake, Interactables will have their own layer. A raycast for Interactable's check will only be done once every few frames if an Interactable is detected near the player.\
Interactions will be handled on the player's "InteractionHandler"