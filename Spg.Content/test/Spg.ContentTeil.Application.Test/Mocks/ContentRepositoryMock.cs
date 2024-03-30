using Moq;
using Spg.ContentTeil.DomainModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spg.ContentTeil.Application.Test.Mocks
{
    public class ContentRepositoryMock
    {
        public Mock<IContentRepository> MockRepository { get; private set; }

        public ContentRepositoryMock()
        {
            MockRepository = new Mock<IContentRepository>();

            // Setup the GetAllContent method
            MockRepository.Setup(repo => repo.GetAllContent())
                .Returns(GetDefaultContentDtos());

            // Setup the GetContentById method
            MockRepository.Setup(repo => repo.GetContentById(It.IsAny<int>()))
                .Returns((int id) => GetDefaultContentDtos().FirstOrDefault(dto => dto.Id == id));

            // Setup the GetContentByText method
            MockRepository.Setup(repo => repo.GetContentByText(It.IsAny<string>()))
                .Returns((string text) => GetDefaultContentDtos().Where(dto => dto.text == text));

            // Setup the CreateContent method
            MockRepository.Setup(repo => repo.CreateContent(It.IsAny<ContentDto>()))
                .Returns((ContentDto dto) => dto);

            // Setup the UpdateContent method
            MockRepository.Setup(repo => repo.UpdateContent(It.IsAny<ContentDto>()))
                .Returns((ContentDto dto) => dto);

            // Setup the DeleteContent method
            MockRepository.Setup(repo => repo.DeleteContent(It.IsAny<int>()))
                .Returns((int id) => GetDefaultContentDtos().FirstOrDefault(dto => dto.Id == id));
        }

        private IEnumerable<ContentDto> GetDefaultContentDtos()
        {
            return new List<ContentDto>
            {
                new ContentDto { Id = 1, text = "content1", guid = Guid.NewGuid() },
                new ContentDto { Id = 2, text = "content2", guid = Guid.NewGuid() },
            };
        }

        public void SetupGetAllContent(IEnumerable<ContentDto> testData)
        {
            MockRepository.Setup(repo => repo.GetAllContent()).Returns(testData);
        }

        public void SetupGetContentById(int id, ContentDto contentDto)
        {
            MockRepository.Setup(repo => repo.GetContentById(id))
                .Returns(contentDto);
        }

        public void SetupCreateContent(ContentDto contentDto)
        {
            MockRepository.Setup(repo => repo.CreateContent(It.IsAny<ContentDto>()))
                .Returns(contentDto);
        }

        public void SetupUpdateContent(ContentDto updatedContentDto)
        {
            MockRepository.Setup(repo => repo.UpdateContent(It.IsAny<ContentDto>()))
                .Returns(updatedContentDto);
        }

        public void SetupDeleteContent(int id, ContentDto deletedContentDto)
        {
            MockRepository.Setup(repo => repo.DeleteContent(id))
                .Returns(deletedContentDto);
        }
    }
}