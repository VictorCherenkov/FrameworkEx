namespace FrameworkEx
{
    public static class BooleanExtensions
    {
        public static string AsExactString(this bool src)
        {
            return src ? "true" : "false";
        }
    }
}