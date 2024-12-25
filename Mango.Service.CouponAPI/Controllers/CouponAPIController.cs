using AutoMapper;
using Mango.Service.CouponAPI.Data;
using Mango.Service.CouponAPI.Models;
using Mango.Service.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Service.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        //Add dependency injection for AppDbContext
        private readonly AppDbContext _context;
        //Add dependency for response dto
        private readonly ResponseDto _responseDTO;
        private readonly IMapper _mapper;


        public CouponAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _responseDTO = new ResponseDto();
            _mapper = mapper;
        }

        //Retrive the existing coupons
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _context.Coupons.ToList();
                _responseDTO.Result = _mapper.Map<IEnumerable<CouponDto>>(objList); ;
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while retrieving coupons: {ex.Message}";
            }
            return _responseDTO;

        }

        //Retrive the existing coupon by its ID
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _context.Coupons.First(U => U.CouponId == id);
                _responseDTO.Result = _mapper.Map<CouponDto>(obj);
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while retrieving coupons: {ex.Message}";
            }
            return _responseDTO;

        }

        //Retrive the existing coupon by its coupon code
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _context.Coupons.FirstOrDefault(U => U.CouponCode.ToLower() == code.ToLower());
                if (obj == null)
                {
                    _responseDTO.IsSuccess = false;
                }
                _responseDTO.Result = _mapper.Map<CouponDto>(obj);
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while retrieving coupons: {ex.Message}";
            }
            return _responseDTO;
        }

        //Create new coupon
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _context.Coupons.Add(obj);
                _context.SaveChanges();

                _responseDTO.Result = _mapper.Map<CouponDto>(obj);
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while creating coupon: {ex.Message}";
            }
            return _responseDTO;
        }


        //Upadte the coupon
        [HttpPut]
        [Route("{id:int}")]
        public ResponseDto Put(int id, [FromBody] CouponDto couponDto)
        {
            try
            {

                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _context.Update(obj);
                _context.SaveChanges();

                _responseDTO.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while updating coupon: {ex.Message}";

            }
            return _responseDTO;
        }

        //Delete a coupon
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _context.Coupons.FirstOrDefault(U => U.CouponId == id);
                if (obj == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Coupon not found";
                    return _responseDTO;
                }
                _context.Coupons.Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while deleting coupon: {ex.Message}";
            }
            return _responseDTO;
        }
    }
}
