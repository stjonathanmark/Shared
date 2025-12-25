namespace Shared.Dto;

public class PagingInfo : IPagingInfo
{
    private int? itemCount;
    private int? pageNumber;
    private int? pageSize;
    private int? medianPage;
    private int pageCount;
    private int startPage;
    private int endPage;
    private int sidePages;
    private int maxPages;
    private bool showFirst;
    private bool showPrev;
    private bool showNext;
    private bool showLast;

    public PagingInfo()
    {

    }

    public PagingInfo(int itemCount, int pageNumber, int pageSize, int medianPage)
    {
        PageEntities(itemCount, pageNumber, pageSize, medianPage);
    }

    private void GetPageCount()
    {
        if (itemCount.HasValue && pageSize.HasValue)
        {
            pageCount = itemCount.Value / pageSize.Value;
        }
        else
        {
            pageCount = 0;
        }
    }

    public void Reset()
    {
        itemCount = null;
        pageNumber = null;
        pageSize = null;
        medianPage = null;
        pageCount = 0;
        startPage = 0;
        endPage = 0;
        maxPages = 0;
        showFirst = false;
        showPrev = false;
        showNext = false;
        showLast = false;
    }

    public int? ItemCount
    {
        get { return itemCount; }
        set
        {
            itemCount = value;
            GetPageCount();
        }
    }

    public int? PageNumber
    {
        get { return pageNumber; }
        set { pageNumber = value; }
    }

    public int? PageSize
    {
        get { return pageSize; }
        set
        {
            pageSize = value;
            GetPageCount();
        }
    }

    public int? MedianPage
    {
        get { return medianPage; }
        set { medianPage = value; }
    }

    public int PageCount
    {
        get
        {
            GetPageCount();
            return pageCount;
        }
    }

    public int StartPage
    {
        get { return startPage; }
    }

    public int EndPage
    {
        get { return endPage; }
    }

    public int MaxPages
    {
        get { return maxPages; }
    }

    public int SidePages
    {
        get { return sidePages; }
    }

    public int? PrevPage
    {
        get
        {
            int? prevPageNum = null;

            if (showPrev && pageNumber.HasValue)
            {
                prevPageNum = pageNumber.Value - 1;
            }

            return prevPageNum;
        }
    }

    public int? NextPage
    {
        get
        {
            int? nextPageNum = null;

            if (showNext && pageNumber.HasValue)
            {
                nextPageNum = pageNumber.Value + 1;
            }

            return nextPageNum;
        }
    }

    public bool ShowFirst
    {
        get { return showFirst; }
    }

    public bool ShowPrev
    {
        get { return showPrev; }
    }

    public bool ShowNext
    {
        get { return showNext; }
    }

    public bool ShowLast
    {
        get { return showLast; }
    }

    public void PageEntities()
    {
        startPage = 1;

        if (itemCount > pageSize)
        {
            pageCount = itemCount.Value / pageSize.Value;

            if (itemCount.Value % PageSize!.Value > 0)
            {
                pageCount++;
            }
        }
        else
        {
            pageCount = 1;
            endPage = 1;
        }

        maxPages = medianPage!.Value * 2 - 1;
        sidePages = medianPage.Value - 1;

        if (pageCount > 1)
        {
            if (pageCount <= maxPages)
            {
                endPage = pageCount;
            }
            else
            {
                if (pageNumber <= medianPage)
                {
                    endPage = maxPages;
                }
                else if (pageNumber > medianPage && pageNumber + sidePages <= pageCount)
                {
                    startPage = pageNumber.Value - sidePages;
                    endPage = pageNumber.Value + sidePages;
                }
                else if (pageNumber > medianPage && pageNumber + sidePages > pageCount)
                {
                    endPage = pageCount;
                    startPage = pageCount - 2 * sidePages;
                }
            }
        }

        showFirst = (startPage > 1);
        showPrev = pageNumber!.Value > 1;
        showNext = (pageNumber.Value < pageCount);
        showLast = (endPage < pageCount);
    }

    public void PageEntities(int itemCount, int pageNumber, int pageSize, int medianPage)
    {
        this.itemCount = itemCount;
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
        this.medianPage = medianPage;
        PageEntities();
    }
}
