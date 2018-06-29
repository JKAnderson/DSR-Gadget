
--| DSR Gadget 1.4
--| By Pav & TKGP
--| https://github.com/JKAnderson/DSR-Gadget

A multi-purpose testing tool for Dark Souls: Remastered.
Compatible with DSR App ver. 1.01, 1.01.1, 1.01.2, and hopefully future versions.
Requires .NET 4.7.2: https://www.microsoft.com/net/download/thank-you/net472
Windows 10 users should already have this.


--| Credits

Costura.Fody by Simon Cropp, Cameron MacFarland
https://github.com/Fody/Costura

LowLevelHooking by Joseph N. Musser II
https://github.com/jnm2/LowLevelHooking

Octokit by GitHub
https://github.com/octokit/octokit.net

Semver by Max Hauser
https://github.com/maxhauser/semver


--| Changelog

1.4
	Add basic event flag support (Misc tab)
	Fix stable position not working on 1.01.2
	Fix stable angle never working in the first place
	Fix no gravity and no collision not reapplying after load screens
	Fix camera storing/restoring not working sometimes
	Hooking the game is much faster now

1.3
	Supports 1.01.2
	Camera is restored with positions
	The download link actually works now

1.2
	Supports 1.01.1 and hopefully all future versions automatically
	Option to restore state with position (but not camera, yet)
	Add cut covenant items to spawner

1.1
	Fixed crash with out of range player angle
