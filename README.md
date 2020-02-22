# Unity Core

Unity Core is a set of highly useful, generic tools built for the Unity 3D game engine. You can pull this source directly into your project's Assets/Scripts directory.

There are 5 categories of tooling in this package:

    1. Menu Management
    2. Audio Management
    3. Scene Management
    4. Data Management
    5. Session Management
    6. Tween (Bonus Content)

### Menu Management

Managing menus and other various UI elements is hugely important. At a high level, we want to create a system of switches for menu content, or various pages in our projects. To get more control, we may also want to create control structures for animation queueing. 

Watch the tutorial: https://youtu.be/qkKuGmGRF2k

### Data Management

This implementation of data management is very basic. We simply take advantage of Unity's PlayerPrefs tool to abstract local data with simple class properties. 

Watch the tutorial: https://youtu.be/Vhuf1e0PVH0

### Audio Management

Audio management can get complex, but our implementation here is highly streamlined. This audio package is capable of managing multiple audio tracks, each with their own set of audio type playables. 

Watch the tutorial: https://youtu.be/3hsBFxrIgQI

### Scene Management

The scene tools in this package will let you easily switch scenes and subscribe to scene load events. The user can optionally choose to integrate the menu management system by passing a loading page PageType into the load method. 

Watch the tutorial: https://youtu.be/4oTluGCOgOM

### Session Management

Sessions are definitely the more abstract system in the bunch. Without an application to manage, it is difficult to foresee what goes in the session controller. However, in this package you'll see a couple of examples of what you may want to store during the session. Here, we store sessionStartTime, fps, and manage the core game loop.

Watch the tutorial: https://youtu.be/M6xy272-axM 

### Tween (Bonus)

This package is bonus content for this repo. There are some easy scripts to get started with the following tween styles:

    1. PositionTween
    2. RotationTween
    3. ScaleTween
    4. ImageFillTween
    5. ImageColorTween
