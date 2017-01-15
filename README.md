# About
Adds lockable doors and autodoors to the game after appropriate research projects Door Locks for normal doors and Locking Autodoors for autodoors have been completed. Finally, your colonists can have a night of undisturbed sleep without needing to build a hallway if you don't want to!

I was inspired to create this mod after my colonists kept entering other colonist's rooms to use their furniture, and installing other mods that added hygiene and bladder requirements which prompted me to build ensuites.

# Dependencies
Requires HugsLib.

# Compatibility
Please note this mod uses the detour technique to redirect flow when the following methods are invoked:
* RimWorld.Pawn_Ownership.UnclaimAll

# Using as a Library
If you are implementing a custom door and want to make it lockable, download the compiled DLL for this mod from Steam Workshop or Ludeon Forums and add it to your project as a reference. Then subclass `LockableDoors.Buildings.Building_LockableDoor`. It is itself a subclass of the regular door. Make sure you make the appropriate invocations back to the base methods if you override any of the methods such as `PawnCanOpen` to retain the behaviour. Lastly, make sure you instruct your users to load your mod **after** LockableDoors and if uploading to Steam Workshop set it as a mod dependency.

# License
Please feel free to use and modify, but provide attribution. If you want to create a translation, please submit a pull request and do not republish the mod separately.