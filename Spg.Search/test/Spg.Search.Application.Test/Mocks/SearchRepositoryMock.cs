using Moq;
using Spg.Search.DomainModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spg.Search.Application.Test.Mocks
{
    public class SearchRepositoryMock
    {
        public Mock<ISearchRepository> MockRepository { get; private set; }

        public SearchRepositoryMock()
        {
            MockRepository = new Mock<ISearchRepository>();

            // Setup the GetAllSearchHistory method
            MockRepository.Setup(repo => repo.GetAllSearchHistory())
                .Returns(GetDefaultSearchHistoryDtos());

            // Setup the GetSearchHistoryById method
            MockRepository.Setup(repo => repo.GetSearchHistoryById(It.IsAny<int>()))
                .Returns((int id) => GetDefaultSearchHistoryDtos().FirstOrDefault(dto => dto.Id == id));

            // Setup the GetSearchHistoryByUsername method
            MockRepository.Setup(repo => repo.GetSearchHistoryByUsername(It.IsAny<string>()))
                .Returns((string username) => GetDefaultSearchHistoryDtos().Where(dto => dto.searchUsername == username));

            // Setup the GetSearchHistoryByContent method
            MockRepository.Setup(repo => repo.GetSearchHistoryByContent(It.IsAny<string>()))
                .Returns((string content) => GetDefaultSearchHistoryDtos().Where(dto => dto.searchContent == content));

            // Setup the CreateSearchHistory method
            MockRepository.Setup(repo => repo.CreateSearchHistory(It.IsAny<SearchHistoryDto>()))
                .Returns((SearchHistoryDto dto) => dto);

            // Setup the UpdateSearchHistory method
            MockRepository.Setup(repo => repo.UpdateSearchHistory(It.IsAny<SearchHistoryDto>()))
                .Returns((SearchHistoryDto dto) => dto);

            // Setup the DeleteSearchHistory method
            MockRepository.Setup(repo => repo.DeleteSearchHistory(It.IsAny<int>()))
                .Returns((int id) => GetDefaultSearchHistoryDtos().FirstOrDefault(dto => dto.Id == id));
        }

        private IEnumerable<SearchHistoryDto> GetDefaultSearchHistoryDtos()
        {
            return new List<SearchHistoryDto>
            {
                new SearchHistoryDto { Id = 1, searchUsername = "user1", searchContent = "content1", guid = Guid.NewGuid() },
                new SearchHistoryDto { Id = 2, searchUsername = "user2", searchContent = "content2", guid = Guid.NewGuid() },
            };
        }

        public void SetupGetAllSearchHistory(IEnumerable<SearchHistoryDto> testData)
        {
            MockRepository.Setup(repo => repo.GetAllSearchHistory()).Returns(testData);
        }

        public void SetupGetSearchHistoryById(int id, SearchHistoryDto searchHistoryDto)
        {
            MockRepository.Setup(repo => repo.GetSearchHistoryById(id))
                .Returns(searchHistoryDto);
        }

        public void SetupCreateSearchHistory(SearchHistoryDto searchHistoryDto)
        {
            MockRepository.Setup(repo => repo.CreateSearchHistory(It.IsAny<SearchHistoryDto>()))
                .Returns(searchHistoryDto);
        }

        public void SetupUpdateSearchHistory(SearchHistoryDto updatedSearchHistoryDto)
        {
            MockRepository.Setup(repo => repo.UpdateSearchHistory(It.IsAny<SearchHistoryDto>()))
                .Returns(updatedSearchHistoryDto);
        }

        public void SetupDeleteSearchHistory(int id, SearchHistoryDto deletedSearchHistoryDto)
        {
            MockRepository.Setup(repo => repo.DeleteSearchHistory(id))
                .Returns(deletedSearchHistoryDto);
        }
    }
}