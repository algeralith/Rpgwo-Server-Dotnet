using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking
{
    public class RSecurity
    {
        private Int16 _seed;
        private int _queueSize;
        private Queue<byte> _serverBytes;
        private Queue<byte> _clientBytes;

        public RSecurity(Int16 rnd, int queueSize)
        {
            this._seed = rnd;
            this._queueSize = queueSize;

            Initialze();
        }

        private void Initialze()
        {
            _serverBytes = new Queue<byte>(_queueSize);
            _clientBytes = new Queue<byte>(_queueSize);
            // Random r = new Random();
            // Seed VB Rnd
            VBMath.Rnd(-1);
            VBMath.Randomize(_seed);

            for (int i = 0; i < _queueSize; i++)
            {
                _clientBytes.Enqueue(Convert.ToByte(VBMath.Rnd() * 255));
                _serverBytes.Enqueue(Convert.ToByte(VBMath.Rnd() * 255));
            }

            // The first byte does not seem to be used, at least initially. -- Could just be related to modulo usage. 
            // TODO :: Explore whether or not it adds the firs to queue, or just skips.
            // Looks like it is added to the end. Not sure why.
            // Need to look into whether mickey keeps a counter and uses modulo. Will get different results on rollover if he does
            _clientBytes.Enqueue(_clientBytes.Dequeue());
            _serverBytes.Enqueue(_serverBytes.Dequeue());
        }

        public byte NextServerByte()
        {
            return NextByte(_serverBytes);
        }

        public byte NextClientByte()
        {
            return NextByte(_clientBytes);
        }

        private byte NextByte(Queue<Byte> bytes)
        {
            byte b = bytes.Dequeue();
            bytes.Enqueue(b);
            return b;
        }
    }
}
