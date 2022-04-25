# Analytics Overview : Top 3 Insights

During the development of our game 'Aether', we collected game data which was based on various metrics of the gameplay,
such as the retry count, the number of times a path was chosen at each level, the time taken by the player to complete a
level, distance travelled by the player in each level, the mode of player death as well as the number of powerups
collected by the player at each level. Post data collection, we analyzed the data and our findings were as follows -

[Team Aether Analytics Data](https://aether-analytics.netlify.app/)

Through these graphical representations, along with the feedback that we received, we categorized our findings into the
following 3 categories -

- Interactivity
- Intuitivity
- Level of Difficulty

## Interactivity

Our basic player mechanics involved the player jumping around from one path to another and dodging obstacles as the
player proceeded further in the path. With the player actions being limited to just changing the direction to land on a
safe path, the player was not left with much to do, and this led to a sense of boredom in the gameplay, which was
adequately reflected in the feedback we received, with some of the actual testimonials being as follows -

> "Some kind of invincible weapons like a rocket" - _Anonymous Duck_

> "Something that can help player run through obstacles (break them)" - _Anonymous Hen_

> "Ability to break obstacle" - _Anonymous Mouse_

> "Teleport Functionality" - _Anonymous Cat_

So, we sought out to address the elephant in the room - **ADD MORE INTERACTIVITY**

| Feedback                                                                                                               | Solution                               |
|------------------------------------------------------------------------------------------------------------------------|----------------------------------------|
| "Some kind of invincible weapons like a rocket" - _Anonymous Calc_                                                     | [Shooting Functionality](#gun-powerup) |
| "Something that can help player run through obstacles (break them)", "Ability to break obstacles" - _Anonymous Beaker_ | [Size Powerup](#size-powerup)          |
| "Teleport Functionality" - _Anonymous Helix_                                                                           | [Wormhole Powerup](#wormhole-effect)   |

<div id="gun-powerup"></div>

#### Gun Powerup

In order to enhance player interactivity, we implemented, what we call the `Space-Gun Powerup` using which the player
can now destroy an obstacle in its path as long as it is within 100 units and is straight ahead of the player. The
player can also keep a score of the number of obstacles that he has shot and destroyed.

| ![gun-powerup](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/gun-powerup.png) |
|:---------------------------------------------------------------------------------------------------------:|
|                                          In Action: Gun Powerup                                           |

<div id="size-powerup"></div>

#### Size Powerup

The `Size Powerup` enables the player to increase in size. When this powerup is enabled, the player can breeze through
the small obstacles, breaking them along its path.

| ![size-powerup-1](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/obstacle-destroy-before.png) |
|:------------------------------------------------------------------------------------------------------------------------:|
|                            In Action: Size Powerup (Player is about to take the size powerup)                            |

| ![size-powerup-2](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/obstacle-destroy-after.png) |
|:-----------------------------------------------------------------------------------------------------------------------:|
|            In Action: Size Powerup (Player is able to destroy the obstacle when the size powerup is enabled)            |

<div id="wormhole-effect"></div>

#### Wormhole Effect

The player (spaceship) can also cut through the fabric of time and space when it travels through a wormhole, which is
when it gets transported to a further point in the game in an instant.

| ![wormhole-powerup-1](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/wormhole-before.png) |
|:--------------------------------------------------------------------------------------------------------------------:|
|                            In Action: Wormhole (Player is about to go into the wormhole)                             |

| ![wormhole-powerup-2](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/wormhole-after.png) |
|:-------------------------------------------------------------------------------------------------------------------:|
|                       In Action: Wormhole (Player has been teleported to a further location)                        |

## Intuitivity

Humans are creatures of habit, and we, at Team Aether, believe that this hypothesis can safely be extended to gamers as
well ;). There are conventions in place that we follow and any deviations to those conventions, can render the gamers
flabbergasted and flummoxed! Some of these ambiguities that we found in the initial iteration of our game, which were
evident by the feedback that we received, can accurately be represented by these testimonials -

> "It's probably better if you don't die instantly and need to restart from the beginning of the level"   -_Anonymous Scale_

> "Yellow and red could switch colors. It's easier to understand that red is DANGER and the game would end upon landing on it. Yellow could be used to slow the player"

So, it was time to address the next elephant in the room - **MAKE GAME INTUITIVE**

| Feedback                                                                                                                                                                                  | Solution                                          |
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------|
| "It's probably better if you don't die instantly and need to restart from the beginning of the level" - _Anonymous Platypus_                                                              | [Health Points (HP)](#health-points)              |
| "Yellow and Red could switch colors. It's easier to understand that red is DANGER and the game would end upon landing on it. Yellow could be used to slow the player" - _Anonymous Perry_ | [Remove Color Ambiguity](#remove-color-ambiguity) |

<div id="health-points"></div>

#### Health Points (HP)

Initially, the player encountered imminent death upon even a single mistake. This, we believed was particularly
discouraging for gamers sometimes. The following analytics chart solidified our inferences -

| ![chart](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/retries-vs-level.png) |
|:--------------------------------------------------------------------------------------------------------:|
|                           Analytics Chart comparing # of retries in each level                           |

Hence, we implemented `Health Points`, which allows the players to take damage upto 5 times, before they perish, and the
game restarts.

| ![chart](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/obstacle-destroy-before.png) |
|:---------------------------------------------------------------------------------------------------------------:|
|                                            In Action: Health Points                                             |

<div id="remove-color-ambiguity"></div>

#### Remove Color Ambiguity

| Initial                                                                                                                                                               | Modified                                          |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------|
| <span style="color:#dee600">**Yellow Path**</span> : Player Death                                                                 | <span style="color:#dee600">**Yellow Path**</span> : Player speed decreases from the base speed              |
| <span style="color:#ff0000">**Red Path**</span>: Player speed decreases from the base speed | <span style="color:#ff0000">**Red Path**</span>: Player health decreases each second the player remains on the red path               |


## Level of Difficulty

Every player that starts playing our game, is a space cadet and goes on to become a space lieutenant, and this was only
possible when the cadet undertook difficult missions. But, we realized that the mission difficulty during the previous
implementation of our game was proving to be too easy, which was evident from the following cadet reviews (and was
backed by analytics as well) -

> "I think the game is too easy overall for power ups. The game needs to be a bit harder for the power ups to be useful"   -_Anonymous Sherlock_

> "Speed is too slow" -_Anonymous Dodo_

| ![chart](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/time-taken-vs-level.png) |
|:-----------------------------------------------------------------------------------------------------------:|
|                         Analytics Chart comparing time taken to complete each level                         |

| ![chart](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/mode-of-death-vs-level.png) |
|:--------------------------------------------------------------------------------------------------------------:|
|                           Analytics Chart comparing the mode of death at each level                            |

So, it was time to address the last elephant in the room - **MAKE GAME CHALLENGING**

| Feedback                                                                                                                                   | Solution                                             |
|--------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------|
| "I think the game is too easy overall for power ups. The game needs to be a bit harder for the power ups to be useful" - _Anonymous Goose_ | Designed difficult Level1 and Level2 & Added Level 3 |
| "Speed is too slow" - _Anonymous Dodo_               | [Speed Powerup](#speed-powerup)                      |

<div id="speed-powerup"></div>

#### Speed Powerup

To add to more challenges in the gameplay, we added the `Speed Powerup`, collecting which, will send your spaceship into
a warp-drive, increasing your speed.

| ![chart](https://raw.githubusercontent.com/ruch0401/resources/main/csci-526-images/speed-powerup.png) |
|:-----------------------------------------------------------------------------------------------------:|
|                                       In Action: Speed Powerup                                        |

