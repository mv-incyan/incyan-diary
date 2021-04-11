using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Api.DiaryModule
{
    public class DiaryDataManager : IDiaryDataManager
    {
        private readonly DiaryContext _context;
        private readonly IMapper _mapper;
        
        public DiaryDataManager(DiaryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
                _context.Remove(entity);
            _context.SaveChanges();
        }

        public Diary Get(int id)
        {
            return _context.Diaries.Where(f => f.Id == id).FirstOrDefault();
        }

        public IQueryable<Diary> GetAll()
        {
            return _context.Diaries;
        }

        public int InsertOrUpdate(Diary entity)
        {
            if (entity.Id == 0)
            {
                _context.Diaries.Add(entity);
                _context.SaveChanges();
            }
            else
            {
                var model = Get(entity.Id);
                model.Title = entity.Title;
                model.Content = entity.Content;
                model.SharedWith = entity.SharedWith;
                _context.SaveChanges();
            }
            
            return entity.Id;
        }
    }
}
