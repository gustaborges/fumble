namespace Fumble.Catalog.Database.Exceptions
{
    public class EntityNotFoundException : Exception
    {

        public EntityNotFoundException(string entityName, Guid id) : base($"No {entityName} with id {id} was found")
        {
        }
    }
}
