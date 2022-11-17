#!/bin/bash
rm output_1_test.txt
dotnet run < input_1.txt > output_1_test.txt
diff output_1.txt output_1_test.txt
rm output_2_test.txt
dotnet run < input_2.txt > output_2_test.txt
diff output_2.txt output_2_test.txt