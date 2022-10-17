# Block-Defense
My Unity project, a tower defense game called "Block Defense" with the twist that the player can directly influence the creeps path through strategic placing of the towers.
## Easily Customizable
The base of the game was constructed to allow adding new elements, such as levels, enemies and towers with just a couple of button clicks & no code changes required.
### Creating a new level
A new level is created by making a new scene and adding the prerequisite prefabs that control the flow of the game. The user can freely modify the game area as well as the size and the shape of the walls. The name should follow the format "Level x" where "x" is any number.
![Create Scene](https://user-images.githubusercontent.com/42253300/196178118-55c49624-df01-4643-a6b5-be956c7a18d3.gif)
The new level should also be added to the build, after which the basic setup is complete and the level itself is functional, however, it requires at least an enemy wave and a tower type in order to be fully functional. 
![Add scene to build](https://user-images.githubusercontent.com/42253300/196178955-f610300a-a911-4e27-a74d-412709f706e6.gif)
### Adding waves and towers to the new level
The user can add waves made up entirely at the user's discression by creating Wave ScriptableObjects in Resources/ScriptableObjects/Waves/"Level x"/ , where "Level x" is the name of the level. The "Level x" folder should be created.
![Add waves to level](https://user-images.githubusercontent.com/42253300/196179944-e1606423-4507-46e8-83e3-44b1f1f6d7e2.gif)
The user can add tower types to the level by creating a Towers ScriptableObject in Resources/ScriptableObjects/Towers/"Level x"/ , where "Level x" is the name of the level. The "Level x" folder should be created.
![Add towers to level](https://user-images.githubusercontent.com/42253300/196180268-cb675181-9fc5-4b1f-a179-5d90af311491.gif)

