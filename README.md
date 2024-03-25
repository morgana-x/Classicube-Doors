# Classicube-Doors
A plugin that adds working doors, that support all rotations, automatically face you on placing, and open and close with (almost) 0 jank!
![c08dc953-fb3e-4084-8525-6833dad908dc](https://github.com/morgana-x/Classicube-Doors/assets/89588301/aad39cc1-6ad9-4b04-acc2-7a41ca264354)

# Installation
Paste the folders into your McGalaxy Server folder, replacing files where needed

Run the following commands in your server console
```
/texture (for now you need to supply your own link, that hosts the terrain.png on this repository that contains the door textures)
```
```
/punload door (Only if updating and the plugin is loaded already)
/pcompile door
/pload door
```

You can also just edit your texture and add the door textures to slots `182` (icon tex), `183` (top tex), and `184` (bottom tex)

Please read the code in absense of conviction to add more documentation

You may need the [infid version of mcgalaxy](https://github.com/ClassiCube/MCGalaxy/blob/master/Uploads/MCGalaxy_infid.dll) for this plugin to work, replace McGalaxy_.dll with McGalaxy_infid.dll (renaming it to be the same name)

# Configuration
Open Plugins/Door.cs

Find the variable `ToggleOnBreak` and change it to true or false, to either toggle the door when breaking it, or when you try placing a block when looking at it

then unload, compile and load the plugin as per the commands stated in the installation instructions

When `ToggleOnBreak` is set to true, the only way to destroy the door is to break the block underneath it

You will find something like
```cs
		public List<DoorConfig> DoorConfigs = new List<DoorConfig>()
		{
			new DoorConfig() // Just add more of these if you want more doors! (Make sure you have a unique id, that has 8 further free Ids after it)
			{
				BLOCK_ITEM_ID = 66, // Takes 9 slots,  So Block ids 66 to 74 will be reserved for the door, keep in mind when adding new doors!
				BLOCK_ITEM_NAME = "Wooden Door",
				TEXTURE_ITEM = 182, // Press F10 to see texture Ids
				TEXTURE_BLOCK_TOP = 183, // Press F10 to see texture Ids
				TEXTURE_BLOCK_BOTTOM = 184 // Press F10 to see texture Ids
			}
		};
```
feel free to add more doors if you want!

# Screenshots

![Screenshot (60)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/57862fbd-6f8b-48bb-829d-70e589319f86)
![Screenshot (59)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/bd43cdc1-5723-4112-974d-e071e496d988)
