using System;

namespace ItemsArchiveService.Utility
{
    public class HashingOptions
    {
        public int Iterations { get { return _iterations; } }

        private int _iterations = new Random().Next(50,9999);
    }
}