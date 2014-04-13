namespace AjErl.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class InputChannel
    {
        private BinaryReader reader;

        public InputChannel(BinaryReader reader)
        {
            this.reader = reader;
        }

        public object Read()
        {
            byte type = this.reader.ReadByte();

            switch (type)
            {
                case (byte)Types.Null:
                    return null;
                case (byte)Types.Integer:
                    return this.reader.ReadInt32();
            }

            throw new InvalidDataException();
        }
    }
}
