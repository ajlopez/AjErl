namespace AjErl.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class OutputChannel
    {
        private BinaryWriter writer;

        public OutputChannel(BinaryWriter writer)
        {
            this.writer = writer;
        }

        public void Write(object obj)
        {
            if (obj == null)
            {
                this.writer.Write((byte)Types.Null);
                return;
            }

            if (obj is int)
            {
                this.writer.Write((byte)Types.Integer);
                this.writer.Write((int)obj);
                return;
            }
        }
    }
}
