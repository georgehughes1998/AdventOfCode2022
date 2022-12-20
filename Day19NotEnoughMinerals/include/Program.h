#ifndef PROGRAM_H
#define PROGRAM_H

#include "DataReader.h"
#include "Blueprint.h"

#include <algorithm>
#include <future>

int getQualityLevel(int minutes, Blueprint blueprint, Resources resources, Resources robots);

#endif // PROGRAM_H
