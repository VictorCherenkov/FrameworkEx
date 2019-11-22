namespace FrameworkEx
{
    public static class BytesConverter
    {
        public static double KBytes(long bytes)
        {
            return bytes/(double) 1024;
        }
        public static double MBytes(long bytes)
        {
            return bytes / (double)(1024*1024);
        }
        public static double GBytes(long bytes)
        {
            return bytes / (double)(1024 * 1024 * 1024);
        }
        public static double TBytes(long bytes)
        {
            return bytes / (double)((long)1024 * 1024 * 1024 * 1024);
        }
    }
}