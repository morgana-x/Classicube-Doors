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

# Configuration
Open Plugins/Door.cs

Find the variable `ToggleOnBreak` and change it to true or false, to either toggle the door when breaking it, or when you try placing a block when looking at it

then unload, compile and load the plugin as per the commands stated in the installation instructions

When `ToggleOnBreak` is set to true, the only way to destroy the door is to break the block underneath it

# Screenshots

![Screenshot (60)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/57862fbd-6f8b-48bb-829d-70e589319f86)
![Screenshot (59)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/bd43cdc1-5723-4112-974d-e071e496d988)
