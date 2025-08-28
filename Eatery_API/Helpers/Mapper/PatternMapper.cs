namespace Eatery_API.Helpers.Mapper
{
    public class PatternMapper<T> where T : class, new()
    {
        private readonly static Lazy<T> lazy = new(() => new T());
        public static T Instance { get { return lazy.Value; } }
    }
}
