using System;
using Gamba.Domain.BuildingBlocks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gamba.Infrastructure.SeedWork
{
    public class TypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>
        where TTypedIdValue : IdValueBase
    {
        public TypedIdValueConverter() 
            : base(id => id.Value, value => Create(value))
        {
        }

        private static TTypedIdValue Create(Guid id) => Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
    }
}