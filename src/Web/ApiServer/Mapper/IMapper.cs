namespace ApiServer.Mapper
{
    public interface IMapper<TApiValue, TInternalValue
        >
    {
        TApiValue Map(TInternalValue model);

        TInternalValue Map(TApiValue model);
    }
}
