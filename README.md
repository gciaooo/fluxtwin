# FluxTwin

A Unity Project that simulates and monitors electricity in an apartment housing. Built for the future development of an Planning-based device automation system.

## Installation

Clone this repository and run the project via Unity.

## How to use

1. Load a copy of EmptyScene in the Editor in order to have all required GameObjects ready.
1. Add prefab electronic devices (Appliances, ElecLinks) in the scene by using the already made prefabs.
2. Hook Electronic Devices to their respective parents and configure their power limit.
3. Run the scene and interact with added Appliances to simulate grid behaviour.

### Electronic devices

Each electronic device type is built as a prefab and can be hooked to each other by following a specific hierarchy. The hierarchy (read as TopLevel --> SecondLevel --> ...) is as follows:

**`ApartmentSource ---> CircuitLine ---> WallPlug ---> Appliance`**

##### Hooking electronic devices

To hook electronic devices to each other, go to the Inspector window on their respective Component scripts and add their parent to the field. The project will automatically create a graphical hook between the devices.

#### Appliances
The class of commodity devices used in an household.

To add an Appliance, choose one of its two available prefabs (Fridge and Oven) and add it to the scene.

> [!WARNING] 
> All Appliance game objects require that there is exactly one ModeSwitcherHandler object on the root of the scene. Be careful:
> 1.  Check if ModeSwitcherHandler is on the scene tree (if you have started the project from EmptyScene you already have it) 
> 2.  Make all Appliances have a reference to ModeSwitcherHandler, in the `ApplianceView` component found on the Inspector window.

To configure an Appliance, go to the `ApplianceController` script component on the Inspector window. There you can change what modes the Appliance can have, how much do they take to complete and their respective power draw.

#### ElecLinks
ApartmentSources, CircuitLines and WallPlugs are defined as `ElecLink`s.

To configure ElecLinks, go to the Inspector window on their respective Component script and modify their maximum power limit and their parent.

### Interacting on the simulation
While the scene is running, you can interact by right-clicking on an Appliance icon and open the ModeSwitcher window. From there you can:
1. Change current mode of the Appliance
2. Look at the time remaining for the current mode `(WIP)` and its power draw.
3. Turn power of the Appliance on/off.

You can also pan the camera that shows the grid by holding right click and zoom via the scrollwheel.

#### Dynamic grid design
You can add and connect grid elements to the scene at application runtime. You can do so by raising the dropdown element on the bottom, which brings the SpawnMenu. Use the SpawnMenu by dragging with left click the electric element you
want to add from the menu to the grid. The element that dragged will be automatically connected with the last parent that has been added to the scene (Manually defining links at runtime is `WIP`).

### Power Surge monitoring
If a power draw threshold on an ElecLink is reached, the simulations stops all relevant electronic devices and outputs a log on their states at the moment of the surge.
This log is outputted and can be analyzed on the Unity Debug Console.

## Development Notes
### Known bugs
- Unexpected behaviour on timers: when changing appliance mode while the appliance is shut down, the timer does not reset to zero (unknown if the bus is only on the visualization or logical aspect)