
# DSR Gadget - By Pav & TKGP
A multi-purpose testing tool for Dark Souls: Remastered.  
Compatible with DSR App ver. 1.01, 1.01.1, 1.01.2, 1.03, and possibly future versions.  
Requires [.NET 4.7.2](https://www.microsoft.com/net/download/thank-you/net472) - Windows 10 users should already have this.

# Special Thanks
*Villhellm*, for generalizing the hotkey UI and kicking off controls not being frozen when they shouldn't be.

# Credits
[Costura.Fody](https://github.com/Fody/Costura) by Simon Cropp, Cameron MacFarland

[LowLevelHooking](https://github.com/jnm2/LowLevelHooking) by Joseph N. Musser II

[Octokit](https://github.com/octokit/octokit.net) by GitHub

[Semver](https://github.com/maxhauser/semver) by Max Hauser

# Changelog
### 1.5
* Supports 1.03
* Controls that don't need the game to be loaded can now be used without the game being loaded
* Added hotkey to create whatever's selected in the Items tab
	
### 1.4
* Add basic event flag support (Misc tab)
* Fix stable position not working on 1.01.2
* Fix stable angle never working in the first place
* Fix no gravity and no collision not reapplying after load screens
* Fix camera storing/restoring not working sometimes
* Hooking the game is much faster now

### 1.3
* Supports 1.01.2
* Camera is restored with positions
* The download link actually works now

### 1.2
* Supports 1.01.1 and hopefully all future versions automatically
* Option to restore state with position (but not camera, yet)
* Add cut covenant items to spawner

### 1.1
* Fixed crash with out of range player angle
