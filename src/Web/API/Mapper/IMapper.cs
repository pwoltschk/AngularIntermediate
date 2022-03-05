namespace API.Mapper
{
    public interface IMapper<out TViewModel, in TDomainModel>
    {
        TViewModel Map(TDomainModel model);
    }
}
