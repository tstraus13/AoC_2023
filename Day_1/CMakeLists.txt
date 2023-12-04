cmake_minimum_required(VERSION 3.26)
project(Day_1 C)

set(CMAKE_C_STANDARD 11)

file(COPY ${CMAKE_CURRENT_SOURCE_DIR}/data.txt DESTINATION ${CMAKE_CURRENT_BINARY_DIR})
add_executable(Day_1 main.c)
