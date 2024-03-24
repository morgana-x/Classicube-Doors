# Classicube-Doors
A plugin that adds working doors

# Installation
Paste the folders into your McGalaxy Server folder, replacing files where needed

Run the following commands in your server console
```
/texture (for now you need to supply your own link, that hosts the terrain.png on this repository that contains the door textures)
```
You can also just edit your texture and add the door textures to slots `182` (icon), `183` (top), and `184` (bottom)

If those texture slots do not work, feel free to change them and edit the block textures (block ids `66`, `67`, `68`, `69`, `70`)

And if you don't like the block ids, you can create new blocks with different ids, and edit the code (it's semi readable I swear!)
```
/punload door (Only if updating and the plugin is loaded already)
/pcompile door
/pload door
```
# Configuration
Open Plugins/Door.cs

Find the variable `ToggleOnBreak` and change it to true or false, to either toggle the door when breaking it, or when you try placing a block when looking at it

then unload, compile and load the plugin as per the commands stated in the installation instructions

When `ToggleOnBreak` is set to true, the only way to destroy the door is to break the block underneath it

# Screenshots

![Screenshot (60)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/57862fbd-6f8b-48bb-829d-70e589319f86)
![Screenshot (59)](https://github.com/morgana-x/Classicube-Doors/assets/89588301/bd43cdc1-5723-4112-974d-e071e496d988)
