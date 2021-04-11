using Diary.Api.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Api.DiaryModule
{
    public interface IDiaryDataManager : IRepository<Diary>
    {
    }
}
