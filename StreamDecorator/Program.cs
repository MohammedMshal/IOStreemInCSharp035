using System.IO.Compression;

namespace StreamDecorator
{
    class Program
    {
        static void Main(string[] args)
        {
            using(FileStream fileStream = File.Create("data.bin"))
            {
                using(DeflateStream ds = new(stream:fileStream, mode: CompressionMode.Compress))
                {
                    ds.WriteByte(65);
                    ds.WriteByte(66);
                }
            }

            using (FileStream fileStream = File.OpenRead("data.bin"))
            {
                using (DeflateStream ds = new(stream: fileStream, mode: CompressionMode.Decompress))
                {
                    byte[] buffer = new byte[4096]; // حجم البايت المؤقت للقراءة

                    int bytesRead;
                    while ((bytesRead = ds.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        for (int i = 0; i < bytesRead; i++)
                        {
                            Console.WriteLine(buffer[i]);
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}