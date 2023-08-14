namespace Facets.SharedKernal.Models;

public sealed class Paginator
{
    private int _size = 100;

    /// <summary>
    /// The number of records to be returned. If less than 1 or greater than 100, PageSize will default to 100
    /// </summary>
    public int PageSize
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value < 1 || value > _size ? _size : value;
        }
    }

    private int _page = 1;
    /// <summary>
    /// The page number to be returned. If less than 1, PageNumber will default to 1
    /// </summary>
    public int PageNumber
    {
        get
        {
            return _page;
        }
        set
        {
            _page = value < 1 ? _page : value;
        }
    }
}
