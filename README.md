# GrappleProto
WCGW contains v0.0-2, "fatal error" made me do this

((what could go wrong))

Grapple prototype

I would like to create a rollable player that can select and grapple to floating anchor points in 2D space.
A spinning indicator should spawn on the active target.

To gamify the prototype, I imagine one achor is lit / active and rewards a score for being connected to it while touching the ground resets the score.
Certain anchors could be active as debuffs (disconnect grapple after time, reset score)


//gathering thoughts

PlayerController:
- A and D for addForce L-R

TargetController:
-Rotate over time, maybe change scale

AnchorController:
-Use sphere collider to detect mouse
-Instantiate target on hovered anchor
-Bool "isTargeted"
-Bool "isModified" (dual use for buffs / debuffs)
