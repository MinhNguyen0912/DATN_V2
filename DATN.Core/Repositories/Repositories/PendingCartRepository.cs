using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class PendingCartRepository: BaseRepository<PendingCart>, IPendingCartRepository
    {
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;
        public PendingCartRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        public PendingCart GetByIdCustom(int id)
        {
            return _context.PendingCart.Where(p=>p.PendingCartId==id).Include(p=>p.PendingCartVariants).FirstOrDefault();
        }
    }
}
