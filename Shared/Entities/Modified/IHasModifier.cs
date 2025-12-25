namespace Shared;

public interface IHasModifier<TModifierId>
    where TModifierId : struct
{
    TModifierId? ModifierId { get; set; }
}
