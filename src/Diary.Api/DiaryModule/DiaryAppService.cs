using AutoMapper;
using Diary.Api.DiaryModule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Api.DiaryModule
{
    public class DiaryAppService : IDiaryAppService
    {
        private readonly IDiaryDataManager _dataManager;
        private readonly IMapper _mapper;

        public DiaryAppService(IDiaryDataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public int Create(DiaryDto entry)
        {
            var model = _mapper.Map<Diary>(entry);
            return _dataManager.InsertOrUpdate(model);
        }

        public void Delete(int id)
        {
            _dataManager.Delete(id);
        }

        public DiaryDto Get(int id)
        {
            var model = _dataManager.Get(id);
            return _mapper.Map<DiaryDto>(model);
        }

        public List<DiaryDto> GetAll()
        {
            var models = _dataManager.GetAll();
            return _mapper.Map<List<DiaryDto>>(models);
        }

        public List<DiaryDto> Search(string searchString)
        {
            var models = _dataManager.GetAll().Where(f => f.Content.Contains(searchString));
            return _mapper.Map<List<DiaryDto>>(models);
        }

        public void Share(int id, string friend)
        {
            var dto = Get(id);
            if (dto == null)
                return;
            dto.SharedWith = friend;
            Create(dto);
        }
    }
}
