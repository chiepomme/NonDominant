namespace JoyConSharp
{
    class PacketCounter
    {
        byte count;

        public byte Get()
        {
            var v = count;
            if (count == 0x0F)
            {
                count = 0;
            }
            else
            {
                count++;
            }

            return v;
        }
    }
}
