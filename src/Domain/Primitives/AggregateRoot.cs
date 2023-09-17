namespace Domain.Primitives
{
    public abstract class AggregateRoot : AuditableEntity
    {
        protected AggregateRoot(int id) : base(id)
        {
        }
        protected AggregateRoot()
        {

        }
    }
}
