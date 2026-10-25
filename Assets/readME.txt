Notes:

General

We had to use Awake instead of Start in all scripts except the Brain. Awake has higher priority, but the real reason is, it breaks stuff if we use Start in all scripts.

EnemyBrain
Decides what state it should change based on the following priority:

Priority 3
Pick random point in the play area, go to point, repeat.

Priority 2
If it senses collectable it collects it.

Priority 1
If it senses the player it attempts to attack him.

EnemyVision
It has a radius with a  vision angle and uses tags to identify objects in its view.
If it detects either a collectible or the Player it locks on to them until it reaches them.
It will continue until either the Player is dead, the coin is collected(Destroyed) or it is out of the field of view. 

EnemyHearing
It has a secondary radius. Basically anything that enters the radius gets detected.
The enemy is a ghost and in this game ghosts have very bad hearing far away, but exceptional hearing when you are close to them. (Radius is small, and it detects you even if you don't move).

EnemyRoam
An old repurposed movement script to work with NavMesh. It follows the same logic as the old. Populate the scene with waypoints and add the waypoints into the inspector as a list.
Then the Enemy chooses at random one point, goes to it and repeats.
For the new one we found that we needed a stoppingDistance variable. Or the Enemy might get stuck on the selected Waypoints.

EnemyCollect
If it spots a coin it will go collect it. There is no actual collection, it just heads to the coins and the coin destroys itself after it touches the enemy.

EnemyAttack
Similar to the EnemyCollect, when it spots the Player he goes towards him.
The main difference is that it has an attack range and when in range it will trigger attack animation. The “health” of the player is also on this script, to keep it simple if the attack is triggered 5 times the player is deleted. When this happens some errors may occur in the console because we deleted the Player but it will not break the game(it did not for us), the Enemy will continue his Roam and Collect. The error also seems to some time appear some time not.

