# TSkinManager

![Release (latest by date)](https://img.shields.io/github/v/release/TavstalDev/TSkinManager?style=plastic-square)
![Workflow Status](https://img.shields.io/github/actions/workflow/status/TavstalDev/TSkinManager/release.yml?branch=stable&label=build&style=plastic-square)
![License](https://img.shields.io/github/license/TavstalDev/TSkinManager?style=plastic-square)
![Downloads](https://img.shields.io/github/downloads/TavstalDev/TSkinManager/total?style=plastic-square)
![Issues](https://img.shields.io/github/issues/TavstalDev/TSkinManager?style=plastic-square)

### What is this?
This is the source code of a .NETFramework library written in C#. This library is a plugin made for Unturned 3.24.x+ servers. 

### Description
A basic cosmetic and skin restrictor plugin. Ideal for roleplay servers.

### Features
* Restrict cloth cosmetics like shirt, pants, hats etc...
* Restrict weapon cosmetics
* Player skin color filter - Replaces unallowed skins with a random allowed one
* Event cosmetics - Gives custom cosmetics to players during configurable events based on date

### Requirements
- Unturned 3.24.x or later
- [RocketMod](https://rocketmod.net/) installed on the server

### Installation

1. Download the latest release and its libraries from the [Releases](https://github.com/TavstalDev/TSkinManager/releases) page.
2. Place `TSkinManager.dll` into your server's `Rocket/Plugins/` directory.
3. Extract the libraries archive into `Rocket/Libraries` directory.
4. Start or restart the server. The plugin will generate a default YAML configuration file on first load.
5. Edit the configuration file to your liking, then reload the plugin or restart the server.

## Building from Source

### Prerequisites

- .NET Framework 4.8 SDK / targeting pack

### Steps

1. Clone the repository:
   ```
   git clone https://github.com/TavstalDev/TSkinManager.git
   ```
2. Open `TSkinManager.sln` in your IDE.
3. Build the project:
   ```
   dotnet build -c Release
   ```
4. The output DLL will be at `TSkinManager/bin/Release/net48/TSkinManager.dll`.

## License

This project is licensed under the GNU General Public License v3.0. See the `LICENSE` file for more details.

## Contact

For issues or feature requests, please use the [GitHub issue tracker](https://github.com/TavstalDev/TSkinManager/issues).