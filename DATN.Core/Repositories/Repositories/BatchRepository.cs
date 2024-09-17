using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.BatchVM;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Processing;

namespace DATN.Core.Repositories.Repositories
{
    public class BatchRepository:BaseRepository<Batch>, IBatchRepository
    {
        private readonly IMapper _mapper;
        public BatchRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<bool> AnyAsync(Expression<Func<Batch, bool>> predicate)
        {
            return Context.Batches.AnyAsync(predicate);
        }

   
        public async Task<Batch> GetByName(string Name,DateTime CurrentDate,decimal GrandTotalValue)
        {
            var queryabler = Context.Batches.AsQueryable();
            var batch = await queryabler.Where(x => x.Name == Name).FirstOrDefaultAsync();
            if (batch is not null)
            {
                if ((batch.StartDate <= CurrentDate && CurrentDate < batch.EndDate) &&
                    batch.MinOrderAmount <= GrandTotalValue)
                {
                    return batch;
                }
                else
                {
                    Batch batchNull = new Batch();
                    return batchNull;
                }
            }
            else
            {
                Batch batchNull = new Batch();
                return batchNull;
            }
        }

        public BatchPaging batchPaging(BatchPaging request)
        {
            var query = Context.Batches.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<BatchVM>>(list);
            return request;
        }

        public Batch GetByIdCustom(int id)
        {
            return Context.Batches.Include(v=>v.Vouchers).ThenInclude(v=>v.User).FirstOrDefault(x => x.Id == id);
        }
    }
}
