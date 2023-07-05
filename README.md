# GameOfLife-CS
A simplified version of John Conway's Game of Life

[INSTRUCTIONS]
The user can move the green cursor icon during the input process to turn on some of the matrix blocks. After that, just by pressing any key, he can watch the evolution of the matrix. Any process requires a click.

[HOW IT WORKS]
Each block is taken individually, and the state of the surrounding blocks is checked. Each block is surrounded by eight other blocks. Based on that, one of the possible states can occur. The checks are performed in one matrix, while the results are saved in a support matrix. Once this is done, at the end of the checks, the results are copied back to the original matrix. This is done to prevent alteration of the block's evolution since they all need to change simultaneously.

[MOVE] by using: [W][A][S][D]

[ACTIVATE BLOCKS] by using: [WW][AA][SS][DD]

[END THE INPUT PHASE] by using: [p]

The output of the matrix may cause some errors in the visualization of the ASCII blocks characters; if that happens, you can edit the function "StampaMatrice" with different characters.



