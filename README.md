# GunBuilder
Gun Builder is my attempt at making a module based weapon building feature for a game. Originally built with cooperative PvE gameplay in mind, the overall design aims to  emphasize creativity and experimentation. Weapons are built using four swappable modules that can be infused with elements to change their behaviour.
## Features
Currently the core set of features is fluid as I feel out what works and doesn't work, so these are subject to change.

### Workbench
GunBuilder uses a workbench object as an interaction point for the building interface. If a player wishes to change their weapon, they need to go to the workbench.

Currently the workbench features a simply animated event-driven UI system that shows the player what they can do for each module. The options for each of the modules is data driven by a static object within the project. I am currently working on implementing a JSON based approach for a better data-driven structure. Using this, adding more modules is a simple pipeline with only a few steps.

### Modules
Each weapon is built using four different modules: the frame, barrel, clip, and trigger. Each module has some generic functionality such as infusion type. Each module also has module-specific properties which are indicated by the different widgets on its UI panel. For example, the barrel module has a length and scale slider to modify properties such as accuracy and damage (longer barrel is more accurate, bigger barrel is more damage, etc.).

Modules themselves can be edited to provide passive effects and stats as mentioned before, but the type of modules present within a weapon will dictate the behaviour of the overall weapon. For example, pairing a cyclotron barrel with a tank style clip will allow the weapon to fire large rolling balls charged with the clip's infusion.

### Infusions
There are three infusions present in GunBuilder at the moment: fire, electric, and magnetic. At the moment, their effects include modifying a projectile's damage type, or providing passive benefits to the weapon. For example, infusing a barrel with fire will allow the weapon to fire faster, while infusing it with magnetic will slow targets down when hit.
