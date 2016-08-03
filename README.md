# Immersive Deep Dreaming

## Project structure

This project is made based on modifications from Google VR's SDK.

Many unused files haven't been removed yet.

The only important scene for the project is: `Assets/skybox.unity`

Two folders have been ignored, for they contained more than 100Mb each:

1. `Assets/Resources`
2. `Assets/Plugins/iOS`

The `Resources` folder should be created and filled with two panoramic scenes:

First scene:

* Panorama sequence for left eye: l.0000.jpg to l.0155.jpg
* Panorama sequence for right eye: r.0000.jpg to r.0155.jpg

Second scene:

* Panorama sequence for left eye: inc.l.0000.jpg to inc.l.0155.jpg
* Panorama sequence for right eye: inc.r.0000.jpg to inc.r.0155.jpg

It should be like this beacause, for now, this is hardcoded in `Assets/scripts/JpegViewer.cs`.

Our scripts are in `Assets\scripts` folder.

Our `pano.shader` shader is in `Assets\shaders` folder, along with our custom cylinder mesh `cylinder.fbx`.

Our material are in `Assets\Materials`