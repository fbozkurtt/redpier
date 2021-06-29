using Docker.DotNet;
using Serilog;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Extensions
{
    public static class MultiplexedStreamExtensions
    {
        public static async Task WriteAsync(this MultiplexedStream stream, char[] input, CancellationToken cancellationToken)
        {
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            await stream.CopyFromAsync(inputStream, cancellationToken);
        }
        public static async Task<string> ReadAsync(this MultiplexedStream stream, CancellationToken cancellationToken)
        {
            var outputStream = new MemoryStream();
            var buffer = ArrayPool<byte>.Shared.Rent(81920);
            string output = null;
            try
            {
                for (; ; )
                {
                    var offset = outputStream.Position;

                    var result = await stream.ReadOutputAsync(buffer, (int)offset, buffer.Length, cancellationToken).ConfigureAwait(false);

                    if (result.EOF)
                        break;

                    if (result.Count == 0)
                        break;

                    await outputStream.WriteAsync(buffer, (int)offset, result.Count, cancellationToken).ConfigureAwait(false);

                    offset = outputStream.Position;
                    outputStream.Position = 0;
                    var reader = new StreamReader(outputStream);
                    output += reader.ReadToEnd();
                    outputStream.Position = offset;
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
            return output;
        }
    }
}
