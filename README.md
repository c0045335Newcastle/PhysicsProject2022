# PhysicsProject2022

===============
Introduction
===============

This is my physics project for CSC3232.

This is game in which you, the green block, must outrun the enemies for as long as possible.
The longer you stay alive, the higher the score you'll achieve.

*HINT*
There's a moving obstacle to the side of the map that when colliding, will apply a force.
This could benefit the player by redirecting them away from the hoarde of enemies.
However, it could present the player nicely to the enemies for attack.

As well as this, there's an anti-gravity zone that could be used to the player's advantage.
However, this zone has a cooldown. A blue zone will give you anti-gravity properties and a 
red zone signifies a cooldown.
Entering this zone disables gravity for all characters, but they player has a decrease in mass
until leaving the zone

Avoid being knocked out of the map, as this will instantly kill the player.

The player has a health of 100 and each collision with an enemy entity will damage the player.
You can heal to prolong your life with health kits that randomly spawn every 20 seconds -
but these can be stolen by enemy agents.

*NOTE*
An injured player moves slower than a healthier player, so make use of health kits...

The player has a weak spot (darker greenside) that when hit by an enemy, will cause a critical
amount of damage that could potentially kill the player with one hit - protect it!

The trick to moving swiftly in the game is to move the mouse alongside using the keys - 
this can get you out of a tricky situation. WASD keys add a force onto the player based
on the direction the camera is facing. I recommend getting the grasp of this on beginner mode.



===============
Difficulties
===============

• BEGINNER:	>> the player moves with increased speed
		>> enemies have decreased damage
		>> only the ORANGE (stealer) enemy can steal health kits
		>> 2 health kits will (re)spawn every 20 seconds
		>> enemies will move at a slower speed until 10,000 score

• REGULAR: 	>> the player moves with regular speed
		>> enemies have increased damage
		>> only the ORANGE (stealer) enemy can steal health kits
		>> 1 health kits will (re)spawn every 20 seconds

• HARDENED: 	>> the player moves with regular speed
		>> enemies have increased damage
		>> all enemies can steal health kits
		>> 1-3 health kits will (re)spawn every 20 seconds


===============
Enemy Types
===============

• Purple Enemy: a basic enemy that uses forces to move towards and attack the player
		>>  useful for blocking/trapping the player

• Red Enemy:	a basic nav-mesh agent, will chase and attack the player
		>>  unused in the main game
		>>  removed to reduce enemy count, making less difficult
		>>  could be added as the score gets higher

• Blue Enemy: 	a nav-mesh agent that will attack the player from a distance with projectiles

• Yellow Enemy: a nav-mesh agent that uses GOAP - has a forcefield that will damage you upon contact
		>>  will either chase or attack the player, with a cooldown
		>>  tends to catch the player off guard
		>>  the forcefield is useful to prevent the player from reaching areas
		>>  *BEWARE* this enemy bounces off of walls!

• Orange Enemy: a nav-mesh agent that uses min-max to decide what action to prioritise
		>>  attack the player?
		>>  retrieve the Health Kit and prevent healing?
		chooses which action to take depending on position and player condition

• Aqua Enemy:	an enemy that uses A* pathfinding to get near to the player to fire projectiles - 
		similar to the blue enemy

• Grey Enemy: 	enemies that move in a flock, effective in slowing the player down!
		>>  can be used to bounce and avoid other enemies, with little damage taken





===============
DEV NOTES
===============

I made two attempts at GOAP: the green enemy and the yellow enemy. 
The green enemy uses some aspects of GOAP but wasn't entirely finished.
The yellow enemy may be a better GOAP agent.

Flocking can be enabled/disabled within the GameManager gameobject.
Simply tick/untick the Flock script!

Incase there are errors with the file uploaded to NESS...

https://github.com/c0045335Newcastle/PhysicsProject2022

https://drive.google.com/file/d/1pTg5Abr-BbtJc5WeBEN46aVxAdEEzlBo/view?usp=sharing

https://newcastle-my.sharepoint.com/:u:/g/personal/c0045335_newcastle_ac_uk/Efc_KBtr7w1Li7V4sMCvUxgBu394AaSMZWkNt6iWOp0c0g?e=YsN6Yn



===============
References/Ideas
===============

A* Algorithm:
https://www.youtube.com/watch?v=AKKpPmxx07w

Flocking:
https://www.youtube.com/watch?v=eMpI1eCsIyM
