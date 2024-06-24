
namespace EM.Domain.Common.Paging
{
    public class Paginate<T> : IPaginate<T>
    {
        /// <summary>
        /// Page index (number)
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Element size in the page
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Total data count
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int Pages { get; }

        /// <summary>
        /// Items
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPrevious => Index > 0;

        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNext => Index + 1 < Pages;

        public Paginate()
        {
            Items = new List<T>();
        }

        public Paginate(int index, int size, int count, int pages, IReadOnlyList<T> items)
        {
            Index = index;
            Size = size;
            Count = count;
            Pages = pages;
            Items = items;
        }
    }
}