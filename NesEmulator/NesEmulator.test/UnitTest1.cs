namespace NesEmulator.test;

public class OpcodeTests
{
    public CPU Cpu;
    

    [Test]
    public void TestA9_ImmedateLoadData()
    {
        Cpu = new CPU();
        var program = new List<byte>()
        {
            0xa9, 0x05,0x00
        };
        Cpu.Interpret(program);

        Assert.That(0x05, Is.EqualTo(Cpu.RegisterA));
        Assert.True((byte)(Cpu.Status & 0b0000_0010) == 0b00);
        Assert.True((byte)(Cpu.Status & 0b0000000) == 0);
    }
    
    [Test]
    public void TestA9_ImmedateLoad_zero_flag()
    {
        Cpu = new CPU();
        var program = new List<byte>()
        {
            0xa9, 0x00, 0x00
        };
        Cpu.Interpret(program);
        Assert.True((byte)(Cpu.Status & 0b0000_0010) == 0b10);
    }
}