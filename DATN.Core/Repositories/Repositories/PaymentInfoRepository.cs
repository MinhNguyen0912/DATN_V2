using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;

namespace DATN.Core.Repositories.Repositories
{
    public class PaymentInfoRepository : BaseRepository<PaymentInfo>, IPaymentInfoRepository
    {
        private readonly IMapper _mapper;
        public PaymentInfoRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
