#include "Program.h"

int main (int argc, char *argv[])
{
    DataReader reader("data.txt");
    std::list<Blueprint> blueprints = reader.read();

    Resources resources = Resources{0, 0, 0, 0};
    Resources robots = Resources{1, 0, 0, 0};

    int qualityCounter = 0;
    for (const Blueprint& blueprint : blueprints)
    {
        int qualityLevel = getQualityLevel(20, blueprint, resources, robots);
        qualityCounter += qualityLevel;
        std::cout << "Quality level: " << qualityLevel << std::endl;
    }
    std::cout << "Total Quality Level: " << qualityCounter << std::endl;
    std::cin.get();
    return 0;
}

int getQualityLevel(int minutes, Blueprint blueprint, Resources resources, Resources robots)
{
    // Base case
    if (minutes == 0)
    {
        return resources.geode * blueprint.id;
    }

    // Increment resources using robots
    Resources newResources = Resources{resources.ore + robots.ore,
                                       resources.clay + robots.clay,
                                       resources.obsidian + robots.obsidian,
                                       resources.geode + robots.geode};

    // Pruning
    if ((newResources.ore >= blueprint.geode_ore && newResources.obsidian >= blueprint.geode_obsidian && robots.obsidian == 0) ||
        (resources.ore > std::max({blueprint.ore, blueprint.clay, blueprint.obsidian_ore, blueprint.geode_ore}) && robots.obsidian == 0))
        return 0;

    // Store the scores for each branch
    std::list<std::future<int>> qualityLevelsFutures;

    // Add robots based using resources
    if (resources.ore >= blueprint.ore)
        qualityLevelsFutures.push_back(std::async(getQualityLevel, minutes - 1,
                               blueprint,
                               Resources{newResources.ore - blueprint.ore, newResources.clay, newResources.obsidian, newResources.geode},
                               Resources{robots.ore + 1, robots.clay, robots.obsidian, robots.geode}));
    if (resources.ore >= blueprint.clay)
        qualityLevelsFutures.push_back(std::async(getQualityLevel, minutes - 1,
                               blueprint,
                               Resources{newResources.ore - blueprint.clay, newResources.clay, newResources.obsidian, newResources.geode},
                               Resources{robots.ore, robots.clay + 1, robots.obsidian, robots.geode}));
    if (resources.ore >= blueprint.obsidian_ore && resources.clay >= blueprint.obsidian_clay)
        qualityLevelsFutures.push_back(std::async(getQualityLevel, minutes - 1,
                               blueprint,
                               Resources{newResources.ore - blueprint.obsidian_ore, newResources.clay - blueprint.obsidian_clay, newResources.obsidian, newResources.geode},
                               Resources{robots.ore, robots.clay, robots.obsidian + 1, robots.geode}));
    if (resources.ore >= blueprint.geode_ore && resources.obsidian >= blueprint.geode_obsidian)
        qualityLevelsFutures.push_back(std::async(getQualityLevel, minutes - 1,
                               blueprint,
                               Resources{newResources.ore - blueprint.obsidian_ore, newResources.clay, newResources.obsidian - blueprint.geode_obsidian, newResources.geode},
                               Resources{robots.ore, robots.clay, robots.obsidian, robots.geode + 1}));

    // Don't/can't buy a robot
    qualityLevelsFutures.push_back(std::async(getQualityLevel, minutes - 1, blueprint, newResources, robots));
    
    std::list<int> qualityLevels;

    std::transform(qualityLevelsFutures.begin(), 
       qualityLevelsFutures.end(), 
       std::back_inserter(qualityLevels),
       [](std::future<int>& f) { return f.get(); });

    return *std::max_element(qualityLevels.begin(), qualityLevels.end());
}
