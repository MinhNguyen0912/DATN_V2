using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.PendingCartVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PendingCartController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PendingCartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddToPendingCart([FromBody] AddToPendingCartVM addToPendingCartVM)
        {
            var user = _unitOfWork.UserRepository.GetByIdCustom(addToPendingCartVM.UserId);
            var variant = _unitOfWork.VariantRepository.GetByIdCustom(addToPendingCartVM.VariantId);
            var check = user.PendingCart.PendingCartVariants.FirstOrDefault(p => p.VariantId == addToPendingCartVM.VariantId);
            if (check != null)
            {
                
            }
            else
            {
                if (variant.Quantity>0&&variant.MaximumQuantityPerOrder>0)
                {
                    var pendingCartVariant = new PendingCartVariant()
                    {
                        PendingCartId = user.PendingCartId,
                        VariantId = addToPendingCartVM.VariantId,
                        Quantity = 1
                    };
                    user.PendingCart.PendingCartVariants.Add(pendingCartVariant);
                }
            }
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();


            return Ok(variant.ProductId); // 201 Created
        }
        [HttpPost]
        public async Task<IActionResult> GetByUserId([FromBody] Guid userId)
        {
            var pendingCart = _unitOfWork.UserRepository.GetByIdCustom(userId).PendingCart;
            return Ok(_mapper.Map<PendingCartVM>( pendingCart));
        }
        [HttpGet]
        public async Task<IActionResult> RemoveVariant(int variantId, int pendingCartId)
        {
            var variant = await _unitOfWork.VariantRepository.GetById(variantId);
            var pendingCart = _unitOfWork.PendingCartRepository.GetByIdCustom(pendingCartId);
            pendingCart.PendingCartVariants.RemoveAll(p=>p.VariantId == variantId);
            _unitOfWork.PendingCartRepository.Update(pendingCart);
            _unitOfWork.SaveChanges();
            return Ok();
        }
    }
}
