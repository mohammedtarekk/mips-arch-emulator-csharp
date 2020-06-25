using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIPS_Emulator
{
    public partial class Form1 : Form
    {
        // --------- MIPS DATA COUNTERS ---------
        //MIPS Registers
        int[] MipsRegisters;

        //Pipeline Registers
        string IF_ID = null;
        string ID_EX = null;
        string EX_MEM = null;
        string MEM_WB = null;

        //Program counter
        string PC = null;

        //Instruction Memory
        Hashtable InstructionsTable;

        //Process Queues
        Queue FetchQ;
        Queue DecodeQ;
        Queue ExecuteQ;
        Queue MemoryQ;
        Queue WriteBackQ;

        //Process Variables
        string Instruction = null;
        string RelativeAddress = null;

        //Register File Variables
        string ReadRegister1 = null;
        string ReadRegister2 = null;
        string ReadData1 = null;
        string ReadData2 = null;
        string WriteRegister = null;
        string WriteData = null;

        //Controls
        string MemRead = "0";
        string MemWrite = "0";
        string RegDst = "0";
        string Branch = "0";
        string MemToReg = "0";
        string ALUSrc = "0";
        string RegWrite = "0";
        string ALUOp = "0";

        //Mux1
        string Mux1_Input0 = "0";
        string Mux1_Input1 = "0";
        string PCSrc = "0";

        //ALU1
        int ALU1_Input0 = 0;
        int ALU1_Input1 = 0;
        int ALU1_Output = 0;
        int ALU1_Zero = 0;
        // --------------------------------------

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize Function
        /// This function initializes the PC, MIPS Registers, Pipeline Registers, Instruction Memory.
        /// It also updates the GUI lists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitializeBT_Click(object sender, EventArgs e)
        {
            // Handling exception of not entering the machine code
            if (UserCodeTxtBox.Text == "")
            {
                MessageBox.Show("Please enter your machine code");
                return;
            }

            // Initialize PC
            PcTxtBox.Clear();
            int y = 0;
            PC = null;
            while (UserCodeTxtBox.Text[y] != ':')
            {
                PC += UserCodeTxtBox.Text[y].ToString();
                y++;
            }
            PcTxtBox.Text = PC;

            // Initialize MIPS Registers
            MipsRegisters = new int[32];
            MipsRegisters[0] = 0;
            for (int i = 1; i < 32; i++)
                MipsRegisters[i] = i + 100;

            // Initialize Pipeline Registers
            IF_ID = "0";
            ID_EX = "0";
            EX_MEM = "0";
            MEM_WB = "0";

            // Reset GUI lists
            RegistersDGV.Rows.Clear();
            PipelineDGV.Rows.Clear();

            for (int i = 0; i < 32; i++)
                RegistersDGV.Rows.Add("$" + i, MipsRegisters[i]);

            PipelineDGV.Rows.Add("IF_ID", IF_ID);
            PipelineDGV.Rows.Add("ID_EX", ID_EX);
            PipelineDGV.Rows.Add("EX_MEM", EX_MEM);
            PipelineDGV.Rows.Add("MEM_WB", MEM_WB);

            // Initialize Instruction Memory     
            string UserCode = UserCodeTxtBox.Text.Replace(" ", ""); /// Removing white spaces

            ///Count instructions
            int InsCount = 1;
            for (int i = 0; i < UserCode.Length; i++)
            {
                if (UserCode[i] == '\r')
                    InsCount++;
            }

            ///Seperate user code by lines and ':' in a list
            ///Every PC followed by its machine code in this list
            ///etc... CodeInList[0] = "1000" , CodeInList[1] = "00000000000000000000000000000000" , and so on..
            String[] seperators = { "\r", "\n", ":" };
            String[] CodeInList = UserCode.Split(seperators, InsCount * 2, StringSplitOptions.RemoveEmptyEntries);

            ///Fill instruction memory hashtable
            ///Initializing the queues of processes
            ///Filling the Fitch queue initially with all instructions
            InstructionsTable = new Hashtable();
            FetchQ = new Queue();
            DecodeQ = new Queue();
            ExecuteQ = new Queue();
            MemoryQ = new Queue();
            WriteBackQ = new Queue();
            for (int i = 0; i < CodeInList.Length; i += 2)
            {
                InstructionsTable.Add(CodeInList[i], CodeInList[i + 1]);
                FetchQ.Enqueue(CodeInList[i].ToString());
            }
        }

        /// <summary>
        /// Run 1 Cycle Function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunCycleBT_Click(object sender, EventArgs e)
        {
            if (PCSrc == "1") // There is branching at a target address stored in Mux1_Output
            {
                PC = Mux1_Input1; // update the PC with the target

                // Clear instructions from the Fitch queue till the target address is detected.
                // Keep the target address and the rest of addresses after it.
                while (FetchQ.Peek().ToString() != PC)
                    FetchQ.Dequeue();

                // Initialize new process queues
                DecodeQ = new Queue();
                ExecuteQ = new Queue();
                MemoryQ = new Queue();
                WriteBackQ = new Queue();

                // Clear controls of the discarded instructions
                Flush();
            }

            // PROCESS STAGES
            if (WriteBackQ.Count != 0)
                WriteBack();

            if (MemoryQ.Count != 0)
                Memory();

            if (ExecuteQ.Count != 0)
                Execute();

            if (DecodeQ.Count != 0)
                Decode();

            if (FetchQ.Count != 0)
                Fetch();

            UpdateGUI();
        }

        /// <summary>
        /// This function updates the GUI lists.
        /// </summary>
        private void UpdateGUI()
        {
            RegistersDGV.Rows.Clear();
            PipelineDGV.Rows.Clear();

            for (int i = 0; i < 32; i++)
                RegistersDGV.Rows.Add("$" + i, MipsRegisters[i]);

            PipelineDGV.Rows.Add("IF_ID", IF_ID);
            PipelineDGV.Rows.Add("ID_EX", ID_EX);
            PipelineDGV.Rows.Add("EX_MEM", EX_MEM);
            PipelineDGV.Rows.Add("MEM_WB", MEM_WB);

            PcTxtBox.Text = PC;
        }


        // -------- IMPLEMENTATION OF MIPS COMPONENTS --------
        /// <summary>
        /// This functions represents the adder component.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns> The sum of two integer numbers </returns>
        private int Adder(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// This functions represents the instruction Memory Component.
        /// It takes the PC address as a parameter.
        /// It sets the global variable "Instruction" with the machine code of the given address.
        /// </summary>
        /// <param name="pc"></param>
        private void InstructionMemory(string pc)
        {
            Instruction = InstructionsTable[pc].ToString();
        }

        /// <summary>
        /// This functions represents the Control Unit.
        /// It gets the OpCode of the current instruction and checks its type.
        /// According to this type, the controls will be set.
        /// </summary>
        private void ControlUnit()
        {
            string OpCode = Instruction.Substring(0, 6); // Bits [31:26] in the machine code

            if (Convert.ToInt32(OpCode, 2) == 0) // R-Type
            {
                RegDst = "1";
                ALUOp = "10";
                ALUSrc = "0";
                Branch = "0";
                MemRead = "0";
                MemWrite = "0";
                RegWrite = "1";
                MemToReg = "0";
            }
            else if (Convert.ToInt32(OpCode, 2) == 4) // beq
            {
                RegDst = "X";
                ALUOp = "01";
                ALUSrc = "0";
                Branch = "1";
                MemRead = "0";
                MemWrite = "0";
                RegWrite = "0";
                MemToReg = "X";
            }
        }

        /// <summary>
        /// This functions clears the controls of mips and the pipeline registers.
        /// It's called when a branching is occured.
        /// </summary>
        private void Flush()
        {
            // Controls
            MemRead = "0";
            MemWrite = "0";
            RegDst = "0";
            Branch = "0";
            MemToReg = "0";
            ALUSrc = "0";
            RegWrite = "0";
            ALUOp = "0";

            // Pipeline Registers
            IF_ID = "0";
            ID_EX = "0";
            EX_MEM = "0";
            MEM_WB = "0";

            // Branching flag
            PCSrc = "0";
        }

        /// <summary>
        /// This function represents the reading part of Register File.
        /// It performs the operation of reading data from specified registers that are treated globally.
        /// </summary>
        private void RegisterFileRead()
        {
            ReadRegister1 = Instruction.Substring(6, 5);  // Bits [25:21] in the machine code
            ReadRegister2 = Instruction.Substring(11, 5); // Bits [20:16] in the machine code

            ReadData1 = MipsRegisters[Convert.ToInt32(ReadRegister1, 2)].ToString();
            ReadData2 = MipsRegisters[Convert.ToInt32(ReadRegister2, 2)].ToString();
        }

        /// <summary>
        /// This function represents the writing part of Register File.
        /// It performs the operation of writing data in a specified register that is treated globally.
        /// It writes only when the given parameter, which is RegWrite Control, is set to 1.
        /// </summary>
        /// <param name="regWrite"></param>
        private void RegisterFileWrite(string regWrite)
        {
            if (regWrite == "1")
                MipsRegisters[Convert.ToInt32(WriteRegister, 2)] = Convert.ToInt32(WriteData);
        }

        /// <summary>
        /// This function takes a 16-bit instruction as a string input.
        /// It extends it to a 32-bit machine code by the sign bit.
        /// </summary>
        /// <param name="s"></param>
        /// <returns> An extended machine code "32-bit" </returns>
        private string SignExtend(string s)
        {
            string sign = s[0].ToString();
            string _32 = null;
            for (int i = 0; i < 16; i++)
                _32 += sign;
            _32 += s;
            return _32;
        }

        /// <summary>
        /// This function represents the ALU unit.
        /// It checks the ALUOp control, and determines the operation.
        /// </summary>
        private void ALU()
        {
            if (ALUOp == "01") // beq (etc... subtraction operation)
            {
                if (ALU1_Input0 - ALU1_Input1 == 0)
                    ALU1_Zero = 1;

            }
            else if (ALUOp == "10") // R-Type
            {
                // Check type of this instruction
                string funct = Instruction.Substring(26, 6); // Bits [5:0] in the machine code

                if (funct == "100000") // add
                    ALU1_Output = ALU1_Input0 + ALU1_Input1;

                if (funct == "100100") // and
                    ALU1_Output = ALU1_Input0 & ALU1_Input1;

                if (funct == "100101") // or
                    ALU1_Output = ALU1_Input0 | ALU1_Input1;

                if (funct == "100010") // sub
                    ALU1_Output = ALU1_Input0 - ALU1_Input1;
            }
        }
        // ---------------------------------------------------


        // -------- IMPLEMENTATION OF PROCESS STAGES --------
        private void Fetch()
        {
            FetchQ.Dequeue(); // Remove an instruction from the fetch queue

            InstructionMemory(PC); // The global variable "Instruction" will hold the machine code of this address.
            string NewPC = Adder(Convert.ToInt32(PC), 4).ToString();      // Increment the PC by 4
            IF_ID = NewPC + " " + Instruction;                    // Pass the new pc and current instruction to IF_ID pipeline register
            Mux1_Input0 = NewPC;
            PC = NewPC;

            DecodeQ.Enqueue(PC); // Insert it into decode queue to know that there is some instruction needs to be decoded next.
        }

        private void Decode()
        {
            DecodeQ.Dequeue();

            // Set the controls of this instruction type
            ControlUnit();

            // Passing controls to pipeline ID/EX sepereated by spaces
            ID_EX = RegWrite + " " + MemToReg + " "
                    + Branch + " " + MemRead + " "
                    + MemWrite + " " + RegDst + " "
                    + ALUOp + " " + ALUSrc + " ";      // ID/EX = WB M EX

            // Passing New PC to from IF/ID to ID/EX
            String[] IFID_Elements = IF_ID.Split(' '); // Seperate the items of IF_ID by spaces in an array of strings.
            ID_EX += IFID_Elements[0] + " "; // ID/EX = WB M EX PC

            Instruction = IFID_Elements[1];

            // Filling MIPS registers with data
            RegisterFileRead();

            // Passing data to pipeline ID/EX
            ID_EX += ReadData1 + " " + ReadData2 + " "; // ID/EX = WB M EX PC RD1 RD2

            // Sign extend for relative address [0-15]
            RelativeAddress = Instruction.Substring(16, 16);
            RelativeAddress = SignExtend(RelativeAddress);

            // Pass relative address ( Instr [15 - 0] ) to pipeline ID/EX
            ID_EX += RelativeAddress + " "; // ID/EX = WB M EX PC RD1 RD2 RAdd

            // Pass rt ( Instr [20 - 16] ) and ( Instr [15 - 11] ) to pipeline ID/EX
            ID_EX += Instruction.Substring(11, 5) + " " + Instruction.Substring(16, 5); // ID/EX = WB M EX PC RD1 RD2 RAdd RT RD    

            ExecuteQ.Enqueue(PC);
        }

        private void Execute()
        {
            ExecuteQ.Dequeue();

            String[] PreviousPipeline = ID_EX.Split(' '); // Seperate the items of ID_EX by spaces in an array of strings.

            // Pass WB and M values to pipeline EX/MEM
            EX_MEM = PreviousPipeline[0] + " "
                    + PreviousPipeline[1] + " "
                    + PreviousPipeline[2] + " "
                    + PreviousPipeline[3] + " "
                    + PreviousPipeline[4] + " "; // EX/MEM = WB M

            int shifted = Convert.ToInt32(PreviousPipeline[11], 2) * 4;
            EX_MEM += Adder(Convert.ToInt32(PreviousPipeline[8]), shifted).ToString() + " "; // EX/MEM = WB M ADDERresult  

            ALU1_Input0 = Convert.ToInt32(PreviousPipeline[9]); // ReadData1
            if (ALUSrc == "0")
                ALU1_Input1 = Convert.ToInt32(PreviousPipeline[10]); // ReadData2
            else if (ALUSrc == "1")
                ALU1_Input1 = Convert.ToInt32(RelativeAddress, 2); // Relative address after sign extention

            string CurrentAddress = (Convert.ToInt32(PreviousPipeline[8]) - 4).ToString();
            string tempIns = Instruction;
            InstructionMemory(CurrentAddress);
            ALU();
            EX_MEM += ALU1_Zero.ToString() + " " + ALU1_Output.ToString() + " " + PreviousPipeline[10] + " "; // EX/MEM = WB M ADDERresult ALUzero ALUresult RD2
            Instruction = tempIns;

            if (RegDst == "0")
                EX_MEM += PreviousPipeline[12] + " "; //rt
            else if (RegDst == "1")
                EX_MEM += PreviousPipeline[13] + " "; //rd
            else if (RegDst == "X")
                EX_MEM += "X" + " ";
            // EX/MEM = WB M ADDERresult ALUzero ALUresult RD2 rt/rd

            MemoryQ.Enqueue(PC);
        }

        private void Memory()
        {
            MemoryQ.Dequeue();

            String[] PreviousPipeline = EX_MEM.Split(' '); // Seperate the items of EX_MEM by spaces in an array of strings.

            // Pass WB to pipeline MEM_WB
            MEM_WB = PreviousPipeline[0] + " " + PreviousPipeline[1] + " "; // MEM/WB = WB

            Mux1_Input1 = PreviousPipeline[5]; // target address (ADDERresult)
            string CurrentBranch = PreviousPipeline[2];
            PCSrc = (Convert.ToInt32(CurrentBranch) & Convert.ToInt32(PreviousPipeline[6])).ToString(); //PCSrc = Branch & ALUZero

            MEM_WB += PreviousPipeline[7] + " " + PreviousPipeline[9] + " "; // MEM/WB = WB ALUresult rt/rd

            WriteBackQ.Enqueue(PC);
        }

        private void WriteBack()
        {
            WriteBackQ.Dequeue();

            String[] PreviousPipeline = MEM_WB.Split(' '); // Seperate the items of MEM_WB by spaces in an array of strings.

            WriteData = PreviousPipeline[2]; // ALUresult
            WriteRegister = PreviousPipeline[3]; // Destination
            RegisterFileWrite(PreviousPipeline[0]); // RegWrite
        }
        // --------------------------------------------------
    }
}
