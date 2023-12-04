#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>

#define FILE_NAME "data.txt"
#define MAX_LINE_SIZE 100

int main() {
    printf("Hello, World!\n");

    FILE *file = fopen(FILE_NAME, "r");

    if (file == NULL)
    {
        printf("ERROR: Cannot open file \"%s\"\n", FILE_NAME);
        exit(EXIT_FAILURE);
    }

    char buffer[MAX_LINE_SIZE];
    int sum = 0;

    while (fgets(buffer, sizeof(buffer), file) != NULL)
    {
        int first = -1;
        int last = -1;

        for (int i = 0; i < sizeof(buffer) - 1; ++i)
        {

            char c = buffer[i];

            if (c == NULL)
                break;

            if (isdigit(c))
            {
                if (first < 0)
                {
                    first = c - '0';
                    last = c - '0';
                }
                else
                {
                    last = c - '0';
                }
            }

        }

        sum = sum + (first * 10) + last;
    }

    printf("Sum is %d", sum);

    fclose(file);
    return 0;
}
