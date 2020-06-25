- The supported types of instructions to be performed are: beq,add,sub,and,or ONLY.

- Assume the following initial state:
  • Each register contains a value equals to its number plus 100 (decimal). For
    example, register $8 contains 108, register $29 contains 129, and so on.
  • Register $Zero always contains 0.

- How to use the emulator?
  1. The user should enter a MIPS machine code with its address in a textbox. Code
     should consist of the supported instructions only.
  2. When the user presses “Initialize”, the program resets to its initial state as specified
     before: Give initial values to PC, MIPS registers, reset pipeline registers, 
     update the GUI lists accordingly and fill the emulated instruction memory with the given code in the textbox.
  3. When the user presses “Run 1 Cycle”, the program runs the emulated MIPS
     components to move pipeline stages forward and updates the GUI accordingly. 