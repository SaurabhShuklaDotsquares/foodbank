using Microsoft.EntityFrameworkCore;
using FB.Core;
using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace FB.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal MMOOlgaDevContext Context; 
        internal DbSet<TEntity> DbSet;

        public Repository(MMOOlgaDevContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual TEntity FindById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void InsertGraph(TEntity entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public TEntity Update(TEntity dbEntity, TEntity entity)
        {
            Context.Entry(dbEntity).CurrentValues.SetValues(entity);
            Context.Entry(dbEntity).State = EntityState.Modified;
            return dbEntity;
        }
        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
        public virtual void UpdateCollection(List<TEntity> entityCollection)
        {
            entityCollection.ForEach(e =>
            {
                Context.Entry(e).State = EntityState.Modified;
            });
            Context.SaveChanges();
        }
        public virtual void InsertCollection(List<TEntity> entityCollection)
        {
            entityCollection.ForEach(e =>
            {
                DbSet.Add(e);
            });
            Context.SaveChanges();
        }

        public SPRequestOutcome ExecuteSP(KeyValuePair<DataSPNames, Dictionary<string, object>> spContent)
        {
            DataSPNames spName = spContent.Key;
            Dictionary<string, object> spParams = spContent.Value ?? null;
            Mmospresult mmospresult = new Mmospresult();
            switch (spName)
            {
                case DataSPNames.sp_MMOOrgClearData:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOOrgClearData {Convert.ToInt32(spParams["CentralOfficeID"])}").FirstOrDefault(); ;
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOCharityClearData:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOCharityClearData {Convert.ToInt32(spParams["CharityID"])},{false}").FirstOrDefault(); ;
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOBranchClearData:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOBranchClearData {Convert.ToInt32(spParams["BranchID"])},{false}").FirstOrDefault(); ;
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOMemberInActive:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOMemberInActive {Convert.ToInt32(spParams["PersonId"])},{Convert.ToDateTime(spParams["InActiveDate"])},{Convert.ToString(spParams["AuditIp"])},{Convert.ToInt32(spParams["AuditUserId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOMemberDeceased:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOMemberDeceased {Convert.ToInt32(spParams["PersonId"])},{Convert.ToDateTime(spParams["DeceasedDate"])},{Convert.ToString(spParams["AuditIp"])},{Convert.ToInt32(spParams["AuditUserId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOMemberReactivate:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOMemberReactivate {Convert.ToInt32(spParams["PersonId"])},{Convert.ToDateTime(spParams["InActiveDate"])},{Convert.ToString(spParams["AuditIp"])},{Convert.ToInt32(spParams["AuditUserId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOHouseHoldInActive:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOHouseHoldInActive {Convert.ToInt32(spParams["HouseholdId"])},{Convert.ToDateTime(spParams["InActiveDate"])},{Convert.ToString(spParams["AuditIp"])},{Convert.ToInt32(spParams["AuditUserId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOHouseHoldReactivate:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOHouseHoldReactivate {Convert.ToInt32(spParams["HouseholdId"])},{Convert.ToDateTime(spParams["InActiveDate"])},{Convert.ToString(spParams["AuditIp"])},{Convert.ToInt32(spParams["AuditUserId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMOMembershipCode:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMOMembershipCode {Convert.ToInt32(spParams["MembershipCodeId"])},{Convert.ToString(spParams["AuditIp"])},{Convert.ToInt32(spParams["AuditUserId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMORemoveMSTenant:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMORemoveMSTenant {Convert.ToString(spParams["TenantId"])},{Convert.ToInt32(spParams["CentralOfficeId"])},{Convert.ToInt32(spParams["CharityId"])},{Convert.ToString(spParams["AuditIp"])},{Convert.ToInt32(spParams["AuditUserId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMODeleteMember:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMODeleteMember {Convert.ToInt32(spParams["PersonId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MMODeleteGroup:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MMODeleteGroup {Convert.ToInt32(spParams["GroupId"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MCCreateUsers:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MCCreateUsers {spParams["UserName"]},{spParams["Password"]}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

                case DataSPNames.sp_MCRemoveUser:
                    mmospresult = Context.Mmospresult.FromSql($"sp_MCRemoveUser {Convert.ToInt32(spParams["ID"])}").FirstOrDefault();
                    return new SPRequestOutcome { Data = null, IsSuccess = mmospresult.Id == 1 };

            }
            return new SPRequestOutcome { Data = null, IsSuccess = false };
        }

        public virtual void Delete(object id)
        {
            var entity = DbSet.Find(id);
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Deleted;
                Delete(entity);
            }
        }
        public virtual void DeleteCollection(List<TEntity> entityCollection)
        {
            entityCollection.ForEach(e =>
            {
                Context.Entry(e).State = EntityState.Deleted;
            });
            Context.SaveChanges();
        }
        public virtual void SafeAttach(TEntity entity, Func<TEntity, object> keyFn)
        {
            SafeAttach<TEntity>(Context, entity, keyFn);
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual RepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        public virtual void ChangeEntityState<T>(T entity, ObjectState state) where T : class
        {
            Context.Entry(entity).State = ChangeState(state);
        }

        private EntityState ChangeState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                case ObjectState.Modified:
                    return EntityState.Modified;
                default:
                    return EntityState.Unchanged;
            }
        }

        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> customOrderBy = null,
            List<Expression<Func<TEntity, object>>>
                includeProperties = null,
            List<string>
                includeStringProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });


            if (includeStringProperties.IsNotNullAndNotEmpty())
                includeStringProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (customOrderBy != null)
                query = customOrderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }


        public void ExecuteSqlCommand(string query)
        {
            Context.Database.ExecuteSqlCommand(query);
        }

        public void SafeAttach<T>(DbContext context, T entity, Func<T, object> keyFn) where T : class
        {
            var existing = context.Set<T>().Local
                .FirstOrDefault(x => Equals(keyFn(x), keyFn(entity)));
            if (existing != null)
                context.Entry(existing).State = EntityState.Detached;
        }

    }
}
