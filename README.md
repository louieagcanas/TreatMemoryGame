# Quick Start Guide
* Make sure you have Git LFS installed for handling larger files from the project
* On the Game view set the aspect ratio to 9:16

# Design Decisions
## Scene Setup
For this game, I went with having the Main Menu and the Game on separate scenes.
The game is very small so loading another scene from the main menu wouldn't be a problem. It's fast.

Originally I had it set up with only one scene and manually managing the active screen and other necessary setup and clean up. 
For example, when moving from main menu to the game and vice versa. 

I first did this because I was used to working with larger games before and this is how we set up some of our game.
I realized I don't necessarily need to do it here because it's a much smaller game (the type of game, not because it's an exam). 

## Game
### Game Board
I implemented a board resizing logic that would dynamically adjust based on the total number of cards. 
It aims for the most balanced look of the board, but won't always have a symmetrical look (e.g. 14 total cards).

I also have the logic where it always have a symmetrical board. But then, depending on the number of cards, it could look unbalanced 
in terms of height and width. 

The board resizing logic would technically handle any number of cards. But this would also have limitation on how many would fit the screen.
I didn't include any resizing of the card as I want it to have a uniform size on every level. Personally this would be easier on the eyes of the player.

### Timer
I used a scriptable object to store the timer values of all the levels. If we want to try a different set of values, we can just create a new instance of the scriptable object and swap it in.
This could be useful during balancing.  

### Observer Pattern
I used observer pattern for UI and most of the game events to avoid unnecessary coupling.  

# Additional Information
## Improvments
### Firebase / Leaderboard
* I thought of having every user store their **Total Moves** for each difficulty. Then in the leaderboard, you could see who made the fewest moves 
on every level. I just didn't have enough time to implement it
* I didn't have the time to put on authentication in here so I just adjusted the rules on Firebase console to not require the user to be authenticated to read and write. But of course, I understand that on an actual game the user should be authenticated.
* API key is technically in the repo, but I understand that it shouldn't be. It's just because of the arrangement of this exam.
*   

### Assets
I wasn't really able to optimize some of the assets as I wanted to put my time on other features. I'm aware that there are sprites where it could be broken down to smaller pieces so things could be more modular.



