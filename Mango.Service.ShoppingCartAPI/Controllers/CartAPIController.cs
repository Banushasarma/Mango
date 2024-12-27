using AutoMapper;
using Mango.Service.ShoppingCartAPI.Data;
using Mango.Service.ShoppingCartAPI.Models;
using Mango.Service.ShoppingCartAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Mango.Service.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {

        //Add dependency injection for AppDbContext
        private readonly AppDbContext _context;
        //Add dependency for response dto
        private readonly ResponseDto _responseDTO;
        private readonly IMapper _mapper;

        public CartAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _responseDTO = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = new CartDto()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(await _context.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId))
                };
                cartDto.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_context.CartDetails.Where(u => u.CartHeader.UserId == userId));

                foreach (var cartDetail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.CartTotal += cartDetail.Product.Price * cartDetail.Count;
                }

                _responseDTO.Result = cartDto;
                _responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while retrieving the cart: {ex.Message}";
            }
            return _responseDTO;
        }


        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //create the cart header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _context.CartHeaders.Add(cartHeader);
                    await _context.SaveChangesAsync();

                    //add the cart details
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _context.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //If header is not null
                    //check if the cart details has the same product
                    var cartDetailsFromDb = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cartDto.CartDetails.First().ProductId
                        && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if (cartDetailsFromDb == null)
                    {
                        //create new cart details
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _context.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in the cart details
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _context.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _context.SaveChangesAsync();

                    }
                }
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Operation completed successfully";

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while adding/updating the cart: {ex.Message}";
            }
            return _responseDTO;
        }


        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _context.CartDetails.First(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = _context.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _context.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeader);
                }
                await _context.SaveChangesAsync();

                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Operation completed successfully";

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while adding/updating the cart: {ex.Message}";
            }
            return _responseDTO;
        }
    }
}
