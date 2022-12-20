#ifndef PROGRAM_H
#define PROGRAM_H

#include "DataReader.h"
#include "Blueprint.h"

#include <algorithm>
#include <utility>
#include <tuple>
#include <future>

int getQualityLevel(int minutes, Blueprint blueprint, Resources resources, Resources robots);
int getQualityLevelArgs(std::tuple<int, Blueprint, Resources, Resources> args);

#endif // PROGRAM_H
