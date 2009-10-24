using CodeCampServer.Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.DataAccess.Mappings
{
	public class EntityClassMap<TEntity> : ClassMap<TEntity>
		where TEntity : PersistentObject
	{
		public EntityClassMap()
		{
			Cache.ReadWrite();
			DynamicUpdate();
			Id(x => x.Id)
				.GeneratedBy.GuidComb();
		}
	}
}