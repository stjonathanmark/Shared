namespace Shared.Dto
{
    public interface IPagingInfo
    {
        int EndPage { get; }

        int? ItemCount { get; set; }

        int MaxPages { get; }

        int? MedianPage { get; set; }

        int? NextPage { get; }

        int PageCount { get; }

        int? PageNumber { get; set; }

        int? PageSize { get; set; }

        int? PrevPage { get; }

        bool ShowFirst { get; }

        bool ShowLast { get; }

        bool ShowNext { get; }

        bool ShowPrev { get; }

        int SidePages { get; }

        int StartPage { get; }
    }
}