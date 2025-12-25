namespace Shared;

public interface IHasCreator<TCreatorId>
    where TCreatorId : struct
{
    TCreatorId CreatorId { get; set; }
}
