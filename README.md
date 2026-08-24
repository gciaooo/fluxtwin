# EMS - Energy Management System

A Unity Project that simulates and monitors electricity in an apartment housing. Built for the future development of an Planning-based device automation system.

## Installation

Clone this repository and run the project via Unity.

## How to use

1. Add prefab electronic devices (Appliances, ElecLinks) in the scene by using the already made prefabs.
2. Hook Electronic Devices to their respective parents and configure their power limit.
3. Run the scene and interact with added Appliances to simulate grid behaviour.

### Electronic devices

Each electronic device type is built as a prefab and can be hooked to each other by following a specific hierarchy. The hierarchy (read as TopLevel --> SecondLevel --> ...) is as follows:

**`ApartmentSource ---> CircuitLine ---> WallPlug ---> Appliance`**

##### Hooking electronic devices

To hook electronic devices to each other, go to the Inspector window on their respective Component scripts and add their parent to the field. The project will automatically create a graphical hook between the devices.

> [!NOTE]
> Graphical hook between CircuitLine and WallPlug is still not implemented.

#### Appliances
The class of commodity devices used in an household. Graphically represented as a squared Sprite.

To add an Appliance, choose one of its two available prefabs (Fridge and Oven) and add it to the scene.

> [!WARNING] 
> All Appliance game objects require that there is exactly one ModeSwitcherHandler object on the root of the scene. Be careful on having it on the scene tree and that all Appliances have a reference to it in the `ApplianceView` component found on the Inspector window.

To configure an Appliance, go to the `ApplianceController` script component on the Inspector window. There you can change what modes the Appliance can have, how much do they take to complete and their respective power draw.

#### ElecLinks
ApartmentSources, CircuitLines and WallPlugs are defined as `ElecLink`s. ApartmentSource and WallPlug are represented as a squared Sprite, while CircuitLine is represented as a border box.

To configure ElecLinks, go to the Inspector window on their respective Component script and modify their maximum power limit and their parent.

> [!WARNING]
> Do not modify the ElecLink's children list property, it will be automatically filled once their parents are assigned upon the scene is run.

### Interacting on the simulation
While the scene is running, you can interact by right-clicking on an Appliance icon and open the ModeSwitcher window. From there you can:
1. Change current mode of the Appliance
2. Look at the time remaining for the current mode `(WIP)` and its power draw.
3. Turn power of the Appliance on/off.

### Power Surge monitoring
If a power draw threshold on an ElecLink is reached, the simulations stops all relevant electronic devices and outputs a log on their states at the moment of the surge.
This log is outputted and can be analyzed on the Unity Debug Console.

