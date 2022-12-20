#include "DataReader.h"

DataReader::DataReader(const std::string filename) : input_file_(filename)
{
    // Open the file for reading
    if (!input_file_.is_open()) {
        throw std::runtime_error("Error: failed to open file.");
    }
}

// Destructor
DataReader::~DataReader()
{
    // Close the file
    input_file_.close();
}

// Read data from the file and apply the regular expression
std::list<Blueprint> DataReader::read()
{
    // Create a list of Blueprints
    std::list<Blueprint> blueprints;

    // Read the file line by line
    std::string line;
    while (input_file_) {
        std::getline(input_file_, line);

        // Apply the regular expression to the line and extract the capture groups
        std::smatch match;
        if (std::regex_search(line, match, regex_)) {
            // Print the capture groups
            std::cout << "ID: " << match[1] << " Ore cost: [ore: "
            << match[2] << "] Clay cost: [ore: "
            << match[3] << "] Obsidian cost [ore: "
            << match[4] << " clay:  "
            << match[5] << "] Geode cost [ore: "
            << match[6] << " obsidian: "
            << match[7] << "]" << std::endl;

            // Add Blueprint to the list
            blueprints.emplace_back(Blueprint{
                            std::stoi(match[1]),
                            std::stoi(match[2]),
                            std::stoi(match[3]),
                            std::stoi(match[4]),
                            std::stoi(match[5]),
                            std::stoi(match[6]),
                            std::stoi(match[7])});
        }


    }

    return blueprints;
}
