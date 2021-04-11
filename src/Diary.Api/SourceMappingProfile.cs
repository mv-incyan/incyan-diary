using AutoMapper;
using Diary.Api.DiaryModule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Api
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<DiaryDto, Diary>().ReverseMap();
        }
    }
}
