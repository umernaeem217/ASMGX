using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;
using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;

namespace ASMGX.DeepMed.Shared.Helpers.EntityFramework
{
    public static class SoftDeletedEntityQueryHelper
    {
        public static void AddSoftDeleteQueryFilter(
            this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeletedEntityQueryHelper)
                ?.GetMethod(nameof(GetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall?.Invoke(null, new object[] { });
            entityData.SetQueryFilter((LambdaExpression)(filter ?? throw new Exception("An error occured in soft delete query extention.")));
            //entityData.AddIndex(entityData.
            //     FindProperty(nameof(ISoftDelete.SoftDeleted)));
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : class, ISoftDeletedEntity
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}
