using Diary.Api.DiaryModule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Api.DiaryModule
{
    public interface IDiaryAppService
    {
        int Create(DiaryDto entry);
        List<DiaryDto> GetAll();
        DiaryDto Get(int id);
        void Delete(int id);
        List<DiaryDto> Search(string searchString);
        void Share(int id, string friend);
    }
}
