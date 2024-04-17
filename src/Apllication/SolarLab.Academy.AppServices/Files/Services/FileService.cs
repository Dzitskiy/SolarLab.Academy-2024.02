using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SolarLab.Academy.AppServices.Files.Repositories;
using SolarLab.Academy.Contracts.Files;
using File = SolarLab.Academy.Domain.Files.Entity.File;

namespace SolarLab.Academy.AppServices.Files.Services
{
    /// <inheritdoc cref="IFileService"/>
    public class FileService : IFileService
    {
        // кеш в памяти приложения
        private readonly IMemoryCache _memoryCache;

        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
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
        public async Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(id, out var res))
            {
                return (FileInfoDto)res;
            }

            var info = await _fileRepository.GetInfoByIdAsync(id, cancellationToken);

            _memoryCache.Set(id, info, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });

            return info;
        }

        /// <inheritdoc/>
        public Task<Guid> UploadAsync(FileDto model, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<File>(model);
            return _fileRepository.UploadAsync(file, cancellationToken);
        }
    }
}
