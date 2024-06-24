namespace EM.Domain.Common.Paging
{
    public interface IPaginate<T>
    {
        /// <summary>
        /// Page index (number)
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Element size in the page
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Total data count
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        int Pages { get; }

        /// <summary>
        /// Items
        /// </summary>
        IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        /// Has next page
        /// </summary>
        bool HasNext { get; }
    }
}