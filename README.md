# Huffman

Naive implementation of Huffman tree for learning purposes.

Reference: https://en.wikipedia.org/wiki/Huffman_coding

## Usage

The program is designed in UNIX-like way, so it accepts data from standard input and writes result to standard output in TSV format.

Examples:

- `man -P cat random | dotnet run --project Huffman`
- `echo "A_DEAD_DAD_CEDED_A_BAD_BABE_A_BEADED_ABACA_BED" | dotnet run --project Huffman`
- `echo "should be depth of four" | dotnet run --project Huffman depth`
