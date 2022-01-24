# VRAGE (The Virtual Reality Art Gallery Experience)

![Sarah's Gallery](/Media/SarahGallery2.PNG)

_(Above) 3D Art by Sarah Belheumer on the 1st floor of VRAGE_

## Background

_VRAGE_ (The Virtual Reality Art Gallery Experience), is a collaborative project created by Pseudodox Collective, a (now dissolved) digital arts collective based out of Portland, ME. It showcases collaborations between artists of various traditional mediums, 3D artists, and a computer science student studying applications of virutal reality (that's me!). 

The project was originally displayed at the Institute of Contemporary Art in Portland, ME in 2017, and was shown again the following year at Currents New Media Festival in Santa Fe, NM. This project is a remaster of the original VRAGE project performed in 2022. When I originally coded this project, I was just getting started out with Unity development, and the code quality showed. This remaster is an attempt to revamp the project with my new and improved Unity development skills after a few years of working in the industry.

## Free code!

All of the scripts in the `Assets/Scripts` folder were authored by me, so feel free to use those if you'd like. Some interesting functionalities include a Bezier-based teleportation system, a basic input system that utilizes Unity's action-based input system to get input from VR devices without use of the XR interactions toolkit, and an easily configurable animation system for animating singular properties of gameobjects (like color, alpha, position, rotation, etc.). In the future, I plan to separate out some of those functionalities into their own GitHub repositories, and to host them as packages on [OpenUPM](http://www.openupm.com/).

Please do be aware that this project is pretty massive due to the number of assets it contains. If you'd just like to view the project, you can download a build from the `Builds` folder and check out the `Assets/Scripts` folder from GitHub. If you'd still like to clone the repo, you'll need to install and configure [Git LFS](https://git-lfs.github.com/).

## To run VRAGE:

- For Windows (OpenVR):
  - Download `Builds/VRAGE-2022-Windows.zip`
  - Unzip the file somewhere on your Windows PC
  - Connect your VR headset and run `VRAGE-2022.exe` (SteamVR must be installed)

At this time, only Windows OpenVR is supported. I'm currently working on adding support for Quest as well, but will probably not be supporting any other platforms without good reason.

## Some additional context

This project was originally displayed in an art gallery setting, where most people were trying VR for the first time. Some of the design decisions reflect that. For instance, the tutorial is very hand-holdy, and the teleportation system restricts you to only being able to teleport to predetermined points. This allows us to position the user in reasonable positions within the galleries, but to someone more experienced with VR, it may come off as a bit restrictive.

When this was displayed in art gallery settings, the app would run for 5 - 15 minutes (depending on the build), and then display a pop-up instructing the user to return their headset to the attendant at the installation. This allowed us to keep the line moving when there were lines of people waiting to try the app. In this version, that functionality has been deactivated.

## Known Issues

- Currently, the app should be usable on most VR devices with a trigger and a thumbstick, but the controllers will always render as Quest 2 controllers. At some point soon, I'll model some basic stand-in controller models to use instead, but the Quest 2 is what I've been using for development, so I went with that for the time being.

- Some walls don't have colliders, so you can teleport through them. This doesn't break anything, but I might fix it in the future.

- Shea's gallery is deactivated until I can replace the trees with something more efficient. Each tree was modeled to somehow use hundreds of materials, resulting in excruciating lag. I'll look into modeling my own trees soon to get that gallery up and running again.

- I haven't implemented grabbable objects yet, so there are no objects that can be picked up at the moment. Shea's gallery was the only one dependent on that feature, so it's not a huge deal, but hoping to have that implemented very soon.

- The cloud models in Mike's gallery don't support single pass rendering, so they only render in one eye. I deactivated them for now, and am planning to adapt the shader to support single pass rendering sometime soon.

## 3rd Party Assets

This project uses the following 3rd party assets from the Unity Asset Store:

- Quantum Theory's '3D Cloud Models'

- Swan Animations' Space Skybox - Part 1

I do not grant permission for anyone to reuse the 3rd party assets listed above. 

## Credits

### Programmer:
 - Justin O'Malley

### 3D Artists:
 - Sarah Belheumer
 - Vincent Riley
 - Carlee St. Louis

### Gallery Artists:
 - Sarah Belheumer
 - James Sylvester
 - Nichole Mastroviti
 - Nate Buck
 - Andrew Jackson
 - Tyler Ryan
 - Chelsee Capone
 - Makenna Pope
 - Kincaid Pearson
 - Michael Lonchar
 - Olivia Dwyer
 - Shea Quinn

### Musicians & Sound Designers:
 - Bryan Waring
 - Jordan Dube
 - Justin O'Malley

 ### Voice Performance

- Julianna Nelson

# Contact

If you have any more questions, feel free to contact Justin O'Malley at justin.t.omalley@gmail.com.