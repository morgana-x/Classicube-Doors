# Classicube-Doors
A plugin that adds working doors, that support all rotations, automatically face you on placing, and open and close with (almost) 0 jank!

![door](https://github.com/morgana-x/Classicube-Doors/assets/89588301/080e328f-ffc5-4293-90ee-848dedefe8ec)

![c08dc953-fb3e-4084-8525-6833dad908dc](https://github.com/morgana-x/Classicube-Doors/assets/89588301/aad39cc1-6ad9-4b04-acc2-7a41ca264354)

# Installation
You will (most likely) need the [infid version of mcgalaxy](https://github.com/ClassiCube/MCGalaxy/blob/master/Uploads/MCGalaxy_infid.dll) for this plugin to work, replace McGalaxy_.dll with McGalaxy_infid.dll (renaming it to be the same name)

If you don't want to use the infid version, try changing the DoorBlockIdStorageIndex to somewhere between 70 and 220 (See configuration) 

Paste the Plugins folder into your server

Run the following commands in your server console
```
/punload door (Only if updating and the plugin is loaded already)
/pcompile door
/pload door
```
```
/texture (either host the terrain.png provided or
          edit your texture at whatever texture slots you choose, see configuration steps below for custom texture id slots
	  feel free to paste door_textures_raw.png into your terrain.png at a free row of texture ids)
```


# Configuration
Open Plugins/Door.cs

edit whatever you want

then unload, compile and load the plugin as per the commands stated in the installation instructions
## Change reserved slots (Where all the hidden actual blocks get banished to away from player sight)
```cs
public ushort DoorBlockIdStorageIndex = 300; // Beggining of reserved Door slots will take up 8*Number of doors
```
## CUSTOM DOOR CONFIGS, (Textures, Names, Block Id etc)
```cs
	public List<DoorConfig> DoorConfigs = new List<DoorConfig>()
		{
			new DoorConfig() // Just add more of these if you want more doors! (Make sure you have a unique id, that has 8 further free Ids after it)
			{
				BLOCK_ITEM_ID = 66,
				BLOCK_ITEM_NAME = "Wooden Door",
				TEXTURE_ITEM = 182, // Press F10 to see texture Ids
				TEXTURE_BLOCK_TOP = 183, // Press F10 to see texture Ids
				TEXTURE_BLOCK_BOTTOM = 184, // Press F10 to see texture Ids
			},
			new DoorConfig() // Iron Door
			{
				BLOCK_ITEM_ID = 67,
				BLOCK_ITEM_NAME = "Iron Door",
				TEXTURE_ITEM = 185, // Press F10 to see texture Ids
				TEXTURE_BLOCK_TOP = 186, // Press F10 to see texture Ids
				TEXTURE_BLOCK_BOTTOM = 187, // Press F10 to see texture Ids
			},
			new DoorConfig() // Dark Oak Door
			{
				BLOCK_ITEM_ID = 68,
				BLOCK_ITEM_NAME = "Dark Oak Door",
				TEXTURE_ITEM = 188, // Press F10 to see texture Ids
				TEXTURE_BLOCK_TOP = 189, // Press F10 to see texture Ids
				TEXTURE_BLOCK_BOTTOM = 190, // Press F10 to see texture Ids
			},
		};
```

feel free to add more doors if you want!

# Screenshots
![Screenshot (67)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/54200295-c9a4-45f8-a7e2-e3887374d0ee)

![Screenshot (63)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/c48b9f16-388c-4b46-9673-aa0f0a32d26c)
