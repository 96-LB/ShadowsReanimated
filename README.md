# Shadows Reanimated
This project is a mod for Rift of the NecroDancer which adds new enemy shadow types to make it easier to read off-beat enemies. The mod includes support for changing both the shapes and the colors of shadows, and everything is fully customizable. Players can choose between a variety of default shadow shapes, or even create their own custom shadow sprites. In order to help distinguish between different types of off-beat enemies, unique shadows can be selected for enemies on thirds, quarters, and sixths of a beat.

> ⚠️ BepInEx mods are <ins>**not officially supported**</ins> by Rift of the NecroDancer. If you encounter any issues with this mod, please open an issue on this GitHub repository, and do not submit reports to Brace Yourself Games! In order to prevent serious bugs, this mod will automatically disable itself when you update your game, and you will have to return here to download a new, compatible version.

The current version is <ins>**v0.2.5**</ins> and is compatible with Rift of the NecroDancer Patch 1.7.0 released on 13 August 2025. Downloads for the latest version can be found [here](https://github.com/96-LB/ShadowsReanimated/releases/latest). The changelog can be found [here](Changelog.md).

To preview the mod, check out the [mod showcase](https://steamcommunity.com/sharedfiles/filedetails/?id=3480138263) on the Steam workshop or view the video below.

[![showcase video](https://github.com/user-attachments/assets/92732cab-7b8b-4d56-9232-445d8e030562)](https://www.youtube.com/watch?v=xkEGyyYabao)



## Installation

Shadows Reanimated runs on BepInEx 5. In order to use this mod, you must first install BepInEx into your Rift of the NecroDancer game folder. A more detailed guide can be found [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html), but a summary is provided below. If BepInEx is already installed, you can skip the next subsection.

### Installing BepInEx
1. Navigate to the latest release of BepInEx 5 [here](https://github.com/BepInEx/BepInEx/releases).

    > ⚠️ This mod is only tested for compatibility with BepInEx 5. If the above link takes you to a version of BepInEx 6, check out [the full list of releases](https://github.com/BepInEx/BepInEx/releases).

2. Expand the "Assets" tab at the bottom and download the correct `.zip` file for your operating system.

    > ℹ️ For example, if you use 64-bit Windows, download `BepInEx_win_x64_5.X.Y.Z.zip`.

4. Extract the contents of the `.zip` file into your Rift of the NecroDancer game folder.

    > ℹ️ You can find this folder by right clicking on the game in your Steam library and clicking 'Properties'. Then navigate to 'Installed Files' and click 'Browse'.

6. If you're on Mac or Linux, configure Steam to run BepInEx when you launch your game. Follow the guide [here](https://docs.bepinex.dev/articles/advanced/steam_interop.html).

7. Run Rift of the NecroDancer to set up BepInEx.

    > ℹ️ If done correctly, your `BepInEx` folder should now contain several subfolders, such as `BepInEx/plugins`.

### Installing Shadows Reanimated
1. Navigate to the latest release of Shadows Reanimated [here](https://github.com/96-LB/ShadowsReanimated/releases/latest).

   > ⚠️ Do NOT download the source code using the button at the top of this page. If you're downloading a `.zip`, you are at the wrong place.

2. Expand the "Assets" tab at the bottom and download `ShadowsReanimated.dll`.

3. Place `ShadowsReanimated.dll` in the `BepInEx/plugins` directory inside the Rift of the NecroDancer game folder.

   > ℹ️ You can find this folder by right clicking on the game in your Steam library and clicking 'Properties'. Then navigate to 'Installed Files' and click 'Browse'.

4. Check that your mod is working by playing the [mod showcase](https://steamcommunity.com/sharedfiles/filedetails/?id=3480138263) level.

### Installing Rift of the NecroManager (highly recommended)

In order to configure the mod to your liking, you are strongly encouraged to additionally install [Rift of the NecroManager](https://github.com/96-LB/RiftOfTheNecroManager), which adds an in-game settings menu for mods. If you already have a mod manager installed, or you prefer manually editing your configuration files, you can skip this step. Detailed installation instructions can be found [here](https://github.com/96-LB/RiftOfTheNecroManager), but the process is the same as in the previous subsection.

## Usage

After installation, your shadows will be configured with the default presets. If you like the standard settings, great - you're all set! If you want to tweak the appearance of your shadows, keep reading for more information.
   > ⚠️ You may notice some extra star shadows on enemies that should be on-beat, especially in vanilla charts. This is not an issue with the mod - it's an issue with the game's charting! Some enemies are misaligned, and the vanilla game incorrectly assigns shadows which hide the fact that they're slightly off-beat.

### Changing shadow shapes and colors

Shadows Reanimated has a variety of options to allow you to personalize the look of your shadows. The mod comes with several presets for shadow shapes:
- Default: Triplet enemies have trapezoids, and quadruplets have triangles. The shapes face left for 1/3 and 1/4, and right for 2/3 and 3/4.
- Delta: Triplet enemies have triangles, and sextuplets have trapezoids. The shapes face left for 1/3 and 1/6, and right for 2/3 and 5/6. Quadruplet enemies have rings: square-shaped for 1/4 and diamond-shaped for 3/4.
- Katie: Triplet enemies have isoceles triangles. The triangle is solid for 1/3 and a ring for 2/3. Quadruplet enemies have rings: circle-shaped for 1/4 and diamond-shaped for 3/4.
- Vanilla: Shapes are unchanged from the base game.

All presets use the vanilla shapes of circles for on-beats, diamonds for half-beats, and stars for anything that doesn't have a designated shadow. If you're unsatisfied with these options, you can mix and match shapes from the various presets, or even add your own. To do so, set the 'Preset' configuration option to 'Custom' and use the settings under the 'Custom Sprites' category to choose a shape for each beat type. To use custom shadows for a beat type, set the sprite to 'Custom' and refer to the next section.

You can use the options in the 'Custom Colors' category to change the shadow colors for each beat type. Or, if you prefer, you can turn them off entirely with the 'Custom Colors' option in the 'General' category. You can also choose to disable the yellow shadows from vibe chains and vibe power if you would like to have your color choices always be visible.

To modify any of these settings, it's recommended to have [Rift of the NecroManager](https://github.com/96-LB/RiftOfTheNecroManager) installed. In this case, you can simply navigate to the in-game mod settings menu and easily set your preferences. Changes will take effect immediately. If you would rather change your settings manually, navigate to `BepInEx/config/com.lalabuff.necrodancer.shadowsreanimated.cfg` in your game directory, modify the text file directly, and restart your game.


### Creating your own shadows

To use your own sprites, you must both set your preset to 'Custom' the sprite for the beat type you want to customize to 'Custom'. Then, create a directory called `ShadowsReanimated` next to your game executable (the same location where you created your BepInEx folder). Inside the folder, you can add PNG files with the following names: `OnBeat.png`, `SixthBeat.png`, `QuarterBeat.png`, `ThirdBeat.png`, `HalfBeat.png`, `TwoThirdBeat.png`, `ThreeQuarterBeat.png`, `FiveSixthBeat.png`, `OtherBeat.png`. A sample directory might look like the following:

```
RiftOfTheNecroDancer.exe
ShadowsReanimated/
  OnBeat.png
  HalfBeat.png
  OtherBeat.png
```


The recommended size is 512x512, and the recommended color is white. Note that shadows appear very horizontally squashed in-game. For examples, check out [this folder](ShadowsReanimated/img) for the default shadows used by the mod and the original Photoshop file used to create them. After adding or editing any custom shadows, the game must be relaunched for changes to take effect.
