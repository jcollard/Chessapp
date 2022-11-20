#!/bin/bash
rm output_2_test.txt
dotnet run < input_2.txt > output_2_test.txt
diff output_2.txt output_2_test.txt