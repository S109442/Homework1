using X.PagedList.Mvc.Core;

namespace Homework1.Helper
{
    public static class PagingOptions
    {
        public static int ToPageIndex(this int? p)
        {
            return p.HasValue ? p.Value < 1 ? 1 : p.Value : 1;
        }
    }
}
