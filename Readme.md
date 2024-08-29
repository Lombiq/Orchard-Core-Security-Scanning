# Security scanning an Orchard Core application - demo solution

This is the Orchard Core solution for the code demoed at the [Orchard Harvest](https://orchardcore.net/harvest) conference in 2024. Check out the talk's recording here (to be added, not yet published).

## Cloning the repository

Since other projects are included as git submodules when cloning this repo be sure to initialize submodules: When using a GUI this will most possibly happen by default, and when using the command line use the `--recurse-submodules` switch. If you cloned without initializing submodules, then you can run `git submodule update --init --recursive` to initialize them later.

## Links

Links to the projects that were used in the demo:

- [Zed Attack Proxy (ZAP)](https://www.zaproxy.org/)
- [Lombiq UI Testing Toolbox for Orchard Core](https://github.com/Lombiq/UI-Testing-Toolbox)
