using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Files.Repositories;
using SolarLab.Academy.Contracts.Files;
using SolarLab.Academy.DataAccess.Base;

namespace SolarLab.Academy.DataAccess.Files.Repository
{
    /// <inheritdoc cref="IFileRepository"/>
    public class FileRepository : BaseRepository<Domain.Files.Entity.File>, IFileRepository
    {
        private readonly IMapper _mapper;

        public FileRepository(IMapper mapper, DbContext dbContext)
            : base(dbContext)
        {
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return DeleteAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetAll().Where(s => s.Id == id)
                              .ProjectTo<FileDto>(_mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetAll().Where(s => s.Id == id)
                              .ProjectTo<FileInfoDto>(_mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(Domain.Files.Entity.File file, CancellationToken cancellationToken)
        {
            await AddAsync(file, cancellationToken);
            return file.Id;
        }
    }
}
