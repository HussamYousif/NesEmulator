using System.Net.Mail;

namespace NesEmulator;

public class CPU
{
    public byte RegisterA;
    public byte Status;
    public byte RegisterX;
    public UInt16 ProgramCounter;
    
    public CPU()
    {
        RegisterA = 0;
        Status = 0;
        ProgramCounter = 0;
        RegisterX = 0;
    }

    public void Interpret(List<byte> program)
    {
        while (true)
        {
            var opcode = program[ProgramCounter];
            ProgramCounter += 1;

            switch (opcode)
            {
                case 0xA9:
                    var opcodeParam = program[ProgramCounter];
                    ProgramCounter += 1;
                    RegisterA = opcodeParam;

                    if (RegisterA == 0)
                    {
                        Status = (byte) (Status | 0b00000010);
                    }
                    else
                    {
                        Status = (byte)(Status | 0b1111_1101);
                    }

                    if ((RegisterA & 0b1000_0000) != 0)
                    {
                        Status = (byte)(Status | 0b1000_0000);
                    }
                    else
                    {
                        Status = (byte)(Status & 0b0111_1111);
                    }
                    break;
                case 0xAA:
                    RegisterX = RegisterA;

                    if (RegisterX == 0)
                    {
                        Status = (byte)(Status | 0b0000_0010);
                    }
                    else
                    {
                        Status = (byte)(Status & 0b1111_1101); 
                    }

                    break;
                case 0x00:
                    return; 
                default:
                    throw new Exception("Not implemented");
            }
        }
    }
    
    
}

 