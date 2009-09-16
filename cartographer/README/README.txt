#Information

data folder needs to be relative to the compiled executeable. 
ie. If the exe is in the bin/Debug folder than there must be a bin/Debug/data folder within it.

KML file is created at runtime and does not need to be in the data folder.

The polygons have been shaded to indicate the winner of the Preffered Party system. 

DMS has been created for future functionality to convert between Degree Minutes and Seconds to normal Longitude and 
Latitude Coordinates in KML files.


#Tests
NUnit test file has been included but has been excluded from the solution. This file is located with the other source files named "test.cs".
Nunit test screenshots have been included in this folder.


#Instructions
1) Load the executable once the data folder is in it's relative path.
2) Either select and load each individual XLS/MID/MIF file or use the last save file to load them automatically.
3) Convert the data into KML


#Optional
1A) Once 3) is complete, check box list area will be filled with the points from the MIF file, this allows for rendering of the area to be turned off
	1A.1) If a change has been made in the check box area (ie turning off Brisbane), Select Generate for the converter to remake the KML file and load it
1B) Select Load KML File to load any given KML file into the application and it will be drawn on the Google Earth display
1C) Load a previous session of Cartographer by selecting Load Last

#Problems
Marona is not filling properly possibly due to lack of information given in the MID and MIF data files.
