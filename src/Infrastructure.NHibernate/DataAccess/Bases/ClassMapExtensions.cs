﻿using System;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Mappings
{
	public static class ClassMapExtensions
	{
		public static void ChangeAuditInfo<T>(this ClasslikeMapBase<T> map) where T : AuditedPersistentObject
		{
			map.Component(x => x.ChangeAuditInfo, cp =>
			                                      	{
			                                      		cp.Map(x => x.Created);
			                                      		cp.Map(x => x.Updated);
			                                      		cp.Map(x => x.CreatedBy).Column("CreatedBy");
			                                      		cp.Map(x => x.UpdatedBy).Column("UpdatedBy");
			                                      	});
		}

		public static void StandardId<T>(this ClassMap<T> map) where T : AuditedPersistentObjectOfGuid
		{
			try
			{
				map.Id(x=>x.Id)
					.GeneratedBy.GuidComb();
			}
			catch(Exception e)
			{
				;
			}
		}
	}
}