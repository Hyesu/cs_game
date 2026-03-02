namespace HEngine.Extensions
{
    public static class StringExtensions
    {
        public static string ToStringKey(this string str)
        {
            return str?.Trim().ToLower() ?? string.Empty;
        }
    }   
}