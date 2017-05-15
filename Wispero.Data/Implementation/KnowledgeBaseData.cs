using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;

namespace Wispero.Data
{
    public class KnowledgeBaseData : Core.Services.IDataService<KnowledgeBaseItem>, Core.Services.IQueryService<KnowledgeBaseItem>
    {
        WisperoDbContext _context;

        #region Constructors
        public KnowledgeBaseData(WisperoDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public void Add(KnowledgeBaseItem entity)
        {
            //TODO: Implement Adding mechanism for KnowledgeBaseItems.
            try
            {
                this._context.KnowledgeBaseItems.Add(entity);
                CommitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            //TODO: Implement Deleting mechanism for KnowledgeBaseItems.
            try
            {
                KnowledgeBaseItem item = this.Get(id);
                if (item != null)
                {
                    this._context.KnowledgeBaseItems.Remove(item);
                    CommitChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Edit(KnowledgeBaseItem entity)
        {
            //TODO: Implement Deleting mechanism for KnowledgeBaseItems. ??
            //This need to handle concurrency. As long as rowversions are the same then persist changes.
            var item = this._context.KnowledgeBaseItems.First(x => x.Id == entity.Id);

            try
            {
                if (item.RowVersion == entity.RowVersion)
                {
                    this._context.KnowledgeBaseItems.Remove(item);
                    CommitChanges();

                    item.Query = entity.Query;
                    item.Answer = entity.Answer;
                    item.LastUpdateOn = entity.LastUpdateOn;
                    item.Tags = entity.Tags;
                    item.RowVersion = entity.RowVersion;

                    this._context.KnowledgeBaseItems.Add(item);
                    CommitChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public KnowledgeBaseItem Get(int id)
        {
            //TODO: Implement Getting by Id mechanism for KnowledgeBaseItems.
            var item = this._context.KnowledgeBaseItems.FirstOrDefault(p => p.Id == id);
            return item;
        }

        public List<KnowledgeBaseItem> GetAll()
        {
            //TODO: Implement Getting ALL mechanism for KnowledgeBaseItems.
            var items = this._context.KnowledgeBaseItems.ToList();
            return items;
        }

        public List<KnowledgeBaseItem> GetByFilter(Expression<Func<KnowledgeBaseItem, bool>> expression)
        {
            //TODO: Implement Getting by Filter mechanism for KnowledgeBaseItems.
            var items = from p in this._context.KnowledgeBaseItems select p;
            var filteredItems = items.Where(expression).ToList();

            return filteredItems;
        }
        #endregion
    }
}
