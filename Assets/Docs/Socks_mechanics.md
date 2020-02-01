## Socks mechanics

- a player benefits from a **bonus** until it releases/throws the sock

- a player is affected from a **malus** when it is hit for: 

  - a fixed amount of time
  - ~~until it takes another sock~~

- a player hit (by a sock thrown by the opponent of from the washing machine) loses the socks he was carrying socks. The fallen socks are then randomly distributed around him (and not "directly" pickable)

  

| Sock ID | Bonus (if kept)                                              | Malus (if hit by)                                            |
| ------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| 1       | speed increase (set the new speed)                           | speed decrease (set the speed to a low value)                |
| 2       | firing range extended (`setForce` 's value increased)        | cannot throw socks (socks "thrown" with zero force aka "fall to the ground") |
| 3       | cannot be hit (temporarily disable collider)                 | cannot pick socks (I guess we need a "can pick" bool)        |
| 4       | floats aka "able to walk through walls" (change player's layer) | cannot move (zeroing movements' forces)                      |
| 5       | ??                                                           | changes player socks' color (does it break the game?)        |



Bonus 1: okay
Malus 1: malus 4

Bonus 2: okay
Malus 2: okay

Bonus 3: if hit lose only one sock
Malus 3: okay

Bonus 4: double jump
Malus 4: can walk only crouch

Bonus 5: can carry three sock
Malus 5: can carry only one sock at a time