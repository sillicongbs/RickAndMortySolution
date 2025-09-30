using RickAndMortyCore;
using SHA3.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyGame
{
    // Provably fair RNG using commit–reveal with SHA3
    public class FairRng : IFairRng, IDisposable
    {
        private readonly List<(int n, int morty, byte[] key, byte[] hmac, int rick, int result, string label)> _history = new();

        // OLD API — keep no-ops so nothing explodes if referenced
        public void CommitRickValue(string value) { /* not used anymore */ }
        public int Generate(int n, int mortyValue) => throw new NotSupportedException("Use GenerateFair.");

        public int GenerateFair(int n, string label, string rickPrompt)
        {
            // 1) key (256-bit)
            var key = RandomNumberGenerator.GetBytes(32);

            // 2) morty number in [0..n-1] with rejection sampling
            int morty = SecureInt(n);

            // 3) HMAC-SHA3-256(morty, key)
            var hmac = HmacSha3_256(key, BitConverter.GetBytes(morty));
            Console.WriteLine($"Morty: {label}={ToHex(hmac)}");

            // 4) Rick’s number
            int rick = AskInt($"Rick: {rickPrompt}", 0, n - 1);

            // 5) final
            int result = (morty + rick) % n;

            _history.Add((n, morty, key, hmac, rick, result, label));
            return result;
        }

        public void RevealAll()
        {
            foreach (var x in _history)
            {
                Console.WriteLine($"Morty: my random value for {x.label} is {x.morty}.");
                Console.WriteLine($"Morty: KEY for {x.label}={ToHex(x.key)}");
                Console.WriteLine($"Morty: fair number = ({x.rick} + {x.morty}) % {x.n} = {x.result}");
            }
            _history.Clear();
        }

        public void Dispose() { }

        // helpers
        private static int AskInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out var v) && v >= min && v <= max) return v;
                Console.WriteLine($"Morty: enter an integer in [{min},{max}].");
            }
        }

        private static int SecureInt(int n)
        {
            using var rng = RandomNumberGenerator.Create();
            Span<byte> b = stackalloc byte[4];
            uint v, max = (uint.MaxValue / (uint)n) * (uint)n;
            do { rng.GetBytes(b); v = BitConverter.ToUInt32(b); } while (v >= max);
            return (int)(v % (uint)n);
        }

        private static byte[] HmacSha3_256(byte[] key, byte[] msg)
        {
            var mac = new HMac(new Sha3Digest(256));
            mac.Init(new Org.BouncyCastle.Crypto.Parameters.KeyParameter(key));
            mac.BlockUpdate(msg, 0, msg.Length);
            var outp = new byte[mac.GetMacSize()];
            mac.DoFinal(outp, 0);
            return outp;
        }

        private static string ToHex(byte[] data)
        {
            var sb = new StringBuilder(data.Length * 2);
            foreach (var b in data) sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
