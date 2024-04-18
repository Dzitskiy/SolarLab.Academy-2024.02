using AutoMapper;
using SolarLab.Academy.AppServices.Files.Repositories;
using SolarLab.Academy.Contracts.Files;
using File = SolarLab.Academy.Domain.Files.Entity.File;

namespace SolarLab.Academy.AppServices.Files.Services
{
    /// <inheritdoc cref="IFileService"/>
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.DeleteByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.DownloadAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.GetInfoByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> UploadAsync(FileDto model, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<File>(model);
            return _fileRepository.UploadAsync(file, cancellationToken);
        }
    }
}
