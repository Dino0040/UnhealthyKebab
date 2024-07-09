# Experimental Kebab Chefs! - Restaurant Simulator Mods

* Multiplayer - Increases the maximum allowed player count of 4 up to 6 (Confirmed working with 5 Players)

## Install Instructions
- Install BepInEx 5 using the [official instructions](https://docs.bepinex.dev/articles/user_guide/installation/index.html)
- [Download](https://github.com/Dino0040/UnhealthyKebab/releases/latest) the plugin(s) you want to install
- Place the downloaded dlls in \*Game Root\*\BepInEx\plugins

## Build Instructions
- Install Visual Studio 2022 with the "Game development with Unity" workload
- Set the "KEBABCHEF_INSTALL" environment variable to your game's root folder (for example "C:\Program Files (x86)\Steam\steamapps\common\Kebab Chefs!")
- Open the "Kebab Chefs Mods.sln" solution using Visual Studio
- Clicking "Build" -> "Build Solution" will compile all plugins and automatically copy them into the correct folder for BepInEx
