﻿using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.ViewModel.ProductCommentVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers
{
    [Authorize(Roles = "User")]
    public class ShippingOrderController : Controller
    {
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public ShippingOrderController(ClientService clientService, HttpClient httpClient, IMapper mapper)
        {
            _clientService = clientService;
            _httpClient = httpClient;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
            if (user == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            try
            {
                ViewData["user"] = user;
            }
            catch (Exception ex)
            {

                ToastHelper.ShowError(TempData, ex.Message); ;
            }

            return View();
        }
        public async Task<IActionResult> WriteComment(string Content, int RattedStar, int ProductId, int InvoiceDetailId)
        {
            var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
            CommentVM comment = new CommentVM();
            comment.Content = Content;
            comment.Rating = RattedStar;
            comment.InvoiceDetailId = InvoiceDetailId;
            comment.ProductId = ProductId;
            comment.Date = DateTime.Now;
            comment.Type = 0;
            comment.UserName = user.Username;
            comment.UserId = user.UserId;
            var productPromotionResponse = await _clientService.Post($"{ApiPaths.Comment}", comment);
            ToastHelper.ShowSuccess(TempData, "Cảm ơn bạn đã để lại bình luận"); ;

            return Json(new { prodId = ProductId });
        }
    }
}
