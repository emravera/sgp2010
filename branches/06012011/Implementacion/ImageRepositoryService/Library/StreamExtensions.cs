using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Library
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Copia los datos desde un stream a otro.
        /// </summary>
        /// <param name="input">El stream a copiar.</param>
        /// <param name="output">El stream copiado.</param>
        public static void CopyTo(this Stream input, Stream output)
        {
            const int bufferSize = 2048;
            byte[] buffer = new byte[bufferSize];
            int bytes = 0;

            while ((bytes = input.Read(buffer, 0, bufferSize)) > 0)
            {
                output.Write(buffer, 0, bytes);
            }
        }
    }
}
