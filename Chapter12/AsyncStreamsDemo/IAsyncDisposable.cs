using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreamsDemo
{
    public class StreamEnumerator : IAsyncEnumerator<byte>
    {
        private readonly Stream _stream;
        private readonly byte[] _buffer;
        private int _bytesRead;
        private int _bufferIndex;
        private const int BufferSize = 8000;

        public byte Current {
            get {
                if (_bufferIndex == _bytesRead)
                {
                    return 0;
                }

                byte result = _buffer[_bufferIndex];
                _bufferIndex++;

                return result;
            } }

        public StreamEnumerator(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _buffer = new byte[BufferSize];
            _bytesRead = -1;
        }

        public async Task<bool> WaitForNextAsync()
        {
            await Task.Delay(1000);

            _bytesRead = await _stream.ReadAsync(_buffer, 0, BufferSize);
            _bufferIndex = 0;

            if (_bytesRead == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Enumeration complete!");
                return false;
            }

            Console.WriteLine($"Read {_bytesRead: 0,0} bytes");

            return true;
        }

        public byte TryGetNext(out bool success)
        {
            if (_bufferIndex == _bytesRead)
            {
                success = false;
                return 0;
            }

            byte result = _buffer[_bufferIndex];
            _bufferIndex++;

            success = true;
            return result;
        }

        public ValueTask DisposeAsync()
        {
            _stream.Dispose();
            Console.WriteLine("Stream disposed!");
            return new ValueTask( Task.CompletedTask);
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            await Task.Delay(1000);

            _bytesRead = await _stream.ReadAsync(_buffer, 0, BufferSize);
            _bufferIndex = 0;

            if (_bytesRead == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Enumeration complete!");
                return false;
            }

            Console.WriteLine($"Read {_bytesRead: 0,0} bytes");

            return true;
        }

      
    }

    public class EnumerableStream : IAsyncEnumerable<byte>
    {
        private readonly Stream _stream;

        public EnumerableStream(Stream stream) => _stream = stream ?? throw new ArgumentNullException(nameof(stream));


        public IAsyncEnumerator<byte> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
           return  new StreamEnumerator(_stream);
        }
    }

    public static class StreamExtensions
    {
        public static IAsyncEnumerable<byte> AsEnumerable(this Stream stream) => new EnumerableStream(stream);
    }
}
