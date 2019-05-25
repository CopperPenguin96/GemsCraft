using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace GemsCraft.Network
{
    public class AesStream : Stream
    {
        private BufferedBlockCipher EncryptCipher { get; set; }
        private BufferedBlockCipher DecryptCipher { get; set; }
        internal byte[] Key { get; set; }

        public AesStream(Stream stream, byte[] key)
        {
            BaseStream = stream;
            Key = key;
            EncryptCipher = new BufferedBlockCipher(new CfbBlockCipher(new AesFastEngine(), 8));
            EncryptCipher.Init(true, new ParametersWithIV(
                new KeyParameter(key), key, 0, 16));
            DecryptCipher = new BufferedBlockCipher(new CfbBlockCipher(new AesFastEngine(), 8));
            DecryptCipher.Init(false, new ParametersWithIV(
                new KeyParameter(key), key, 0, 16));
        }

        public Stream BaseStream { get; set; }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => 0;
// hack for libnbt
            set => throw new NotSupportedException();
        }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        public override int ReadByte()
        {
            int value = BaseStream.ReadByte();
            return value == -1 ? value : DecryptCipher.ProcessByte((byte)value)[0];
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int length = BaseStream.Read(buffer, offset, count);
            var decrypted = DecryptCipher.ProcessBytes(buffer, offset, length);
            Array.Copy(decrypted, 0, buffer, offset, decrypted.Length);
            return length;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var encrypted = EncryptCipher.ProcessBytes(buffer, offset, count);
            BaseStream.Write(encrypted, 0, encrypted.Length);
        }

        public override void Close()
        {
            BaseStream.Close();
        }
    }
}