using AutoMapper;
using Diary.Api.DiaryModule;
using Diary.Api.DiaryModule.Dto;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Diary.Tests
{
    public class DiaryServiceTests
    {
        private readonly Mock<IDiaryDataManager> _mockDataManager = new Mock<IDiaryDataManager>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly DiaryAppService _target;

        public DiaryServiceTests()
        {
            _target = new DiaryAppService(_mockDataManager.Object, _mockMapper.Object);
        }

        [Fact]
        public void WhenCreatingNewEntry_ShouldCallCreate()
        {
            // arange
            var diaryDto = new DiaryDto
            {
                Title = "title",
                Content = "content",
                SharedWith = "friend"
            };

            var diaryModel = new Diary.Api.Diary()
            {
                Id = 1,
                Content = diaryDto.Content,
                Title = diaryDto.Title,
                Created = DateTime.Now,
                SharedWith = diaryDto.SharedWith
            };

            _mockMapper.Setup(x => x.Map<Diary.Api.Diary>(It.IsAny<DiaryDto>()))
            .Returns((DiaryDto source) =>
            {
                return diaryModel;
            });

            _mockDataManager.Setup(x => x.InsertOrUpdate(diaryModel))
                .Returns(diaryModel.Id);

            // act
            var result = _target.Create(diaryDto);

            // assert
            _mockDataManager.Verify(x => x.InsertOrUpdate(diaryModel), Times.Once);
            Assert.Equal(diaryModel.Id, result);
        }

        [Fact]
        public void WhenDeletingEntry_ShouldCallDelete()
        {
            // arange
            var id = 1;

            _mockDataManager.Setup(x => x.Delete(id))
                .Verifiable();

            _target.Delete(id);

            _mockDataManager.Verify(x => x.Delete(id), Times.Once);
        }

        [Fact]
        public void WhenGetAll_ShouldReturnList()
        {
            // arange
            var modelList = new List<Diary.Api.Diary>();
            var dtoList = new List<DiaryDto>();

            modelList.Add(new Diary.Api.Diary()
            {
                Id = 1,
                Title = "title",
                Content = "content",
                SharedWith = "friend",
                Created = DateTime.Now,
            });
            modelList.Add(new Diary.Api.Diary()
            {
                Id = 2,
                Title = "title2",
                Content = "content2",
                SharedWith = "friend2",
                Created = DateTime.Now,
            });

            dtoList.Add(new DiaryDto
            {
                Title = "title",
                Content = "content",
                SharedWith = "friend"
            });

            dtoList.Add(new DiaryDto
            {
                Title = "title2",
                Content = "content2",
                SharedWith = "friend2"
            });


            _mockMapper.Setup(x => x.Map<List<DiaryDto>>(It.IsAny<IQueryable<Diary.Api.Diary>>()))
            .Returns(dtoList);

            _mockDataManager.Setup(x => x.GetAll())
                .Returns(modelList.AsQueryable());

            // act
            var result = _target.GetAll();

            // assert
            _mockDataManager.Verify(x => x.GetAll(), Times.Once);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void WhenSearch_ShouldReturnList()
        {
            // arange
            var modelList = new List<Diary.Api.Diary>();
            var dtoList = new List<DiaryDto>();

            modelList.Add(new Diary.Api.Diary()
            {
                Id = 1,
                Title = "title",
                Content = "content",
                SharedWith = "friend",
                Created = DateTime.Now,
            });
            modelList.Add(new Diary.Api.Diary()
            {
                Id = 2,
                Title = "title2",
                Content = "content2",
                SharedWith = "friend2",
                Created = DateTime.Now,
            });

            dtoList.Add(new DiaryDto
            {
                Id = 2,
                Title = "title2",
                Content = "content2",
                SharedWith = "friend2"
            });


            _mockMapper.Setup(x => x.Map<List<DiaryDto>>(It.IsAny<IQueryable<Diary.Api.Diary>>()))
            .Returns(dtoList);

            _mockDataManager.Setup(x => x.GetAll())
                .Returns(modelList.AsQueryable());

            // act
            var result = _target.Search("2");

            // assert
            _mockDataManager.Verify(x => x.GetAll(), Times.Once);
            Assert.Equal(2, result.First().Id);
            Assert.Single(result);
        }

        [Fact]
        public void WhenGet_ShouldReturnDiaryEntry()
        {
            // arange\
            var id = 1;
            var diaryDto = new DiaryDto
            {
                Id = 1,
                Title = "title",
                Content = "content",
                SharedWith = "friend"
            };

            var diaryModel = new Diary.Api.Diary()
            {
                Id = 1,
                Content = diaryDto.Content,
                Title = diaryDto.Title,
                Created = DateTime.Now,
                SharedWith = diaryDto.SharedWith
            };


            _mockMapper.Setup(x => x.Map<DiaryDto>(It.IsAny<Diary.Api.Diary>()))
            .Returns(diaryDto);

            _mockDataManager.Setup(x => x.Get(id))
                .Returns(diaryModel);

            // act
            var result = _target.Get(id);

            // assert
            _mockDataManager.Verify(x => x.Get(id), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void WhenSharingToAFriend_ShouldCallGetThenCreate()
        {
            // arange
            var id = 1;
            var friend = "friend";

            var diaryDto = new DiaryDto
            {
                Id = 1,
                Title = "title",
                Content = "content"
            };

            var diaryModel = new Diary.Api.Diary()
            {
                Id = 1,
                Content = diaryDto.Content,
                Title = diaryDto.Title,
                Created = DateTime.Now,
                SharedWith = diaryDto.SharedWith
            };

            _mockMapper.Setup(x => x.Map<DiaryDto>(It.IsAny<Diary.Api.Diary>()))
           .Returns(diaryDto);

            _mockMapper.Setup(x => x.Map<Diary.Api.Diary>(It.IsAny<DiaryDto>()))
           .Returns((DiaryDto source) =>
           {
               return diaryModel;
           });

            _mockDataManager.Setup(x => x.Get(id))
               .Returns(diaryModel);

            _mockDataManager.Setup(x => x.InsertOrUpdate(diaryModel))
                .Returns(diaryModel.Id);

            // act
            _target.Share(id, friend);

            // assert
            _mockDataManager.Verify(x => x.Get(id), Times.Once);
            _mockDataManager.Verify(x => x.InsertOrUpdate(diaryModel), Times.Once);
        }
    }
}
