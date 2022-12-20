#ifndef BLUEPRINT_H_INCLUDED
#define BLUEPRINT_H_INCLUDED

struct Blueprint
{
    int id;
    int ore;
    int clay;
    int obsidian_ore;
    int obsidian_clay;
    int geode_ore;
    int geode_obsidian;
};

struct Resources
{
    int ore;
    int clay;
    int obsidian;
    int geode;
};

#endif // BLUEPRINT_H_INCLUDED
