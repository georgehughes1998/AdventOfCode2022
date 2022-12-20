#ifndef DATAREADER_H
#define DATAREADER_H

#include "Blueprint.h"

#include <iostream>
#include <fstream>
#include <regex>
#include <list>

class DataReader
{
    public:
        DataReader(const std::string filename);
        virtual ~DataReader();

        std::list<Blueprint> read();

    protected:

    private:
        // Input file stream
        std::ifstream input_file_;

        // Regular expression
        std::regex regex_ = std::regex(R"(Blueprint (\d+): Each ore robot costs (\d+) ore\. Each clay robot costs (\d+) ore\. Each obsidian robot costs (\d+) ore and (\d+) clay\. Each geode robot costs (\d+) ore and (\d+) obsidian\.)");
};

#endif // DATAREADER_H
