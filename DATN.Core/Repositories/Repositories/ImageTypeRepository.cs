using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;

namespace DATN.Core.Repositories.Repositories
{
    public class ImageTypeRepository : BaseRepository<ImageType>, IImageTypeRepository
    {
        private readonly IMapper _mapper;
        public ImageTypeRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
