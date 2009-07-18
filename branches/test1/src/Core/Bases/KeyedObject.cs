namespace July09v31.Core.Domain.Model
{
    public abstract class KeyedObject : PersistentObject
    {
        public virtual string Key { get; set; }
    }
}