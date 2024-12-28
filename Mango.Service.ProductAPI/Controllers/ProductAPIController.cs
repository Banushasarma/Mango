using Mango.Service.ProductAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mango.Service.ProductAPI.Models.Dto;
using AutoMapper;
using Mango.Service.ProductAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Mango.Service.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize]
    public class ProductAPIController : ControllerBase
    {
        //Add dependency injection for AppDbContext
        private readonly AppDbContext _context;
        //Add dependency for response dto
        private readonly ResponseDto _responseDTO;
        private readonly IMapper _mapper;

        public ProductAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _responseDTO = new ResponseDto();
            _mapper = mapper;
        }

        //Retrive the existing products
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _context.Products.ToList();
                _responseDTO.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while retrieving products: {ex.Message}";
            }
            return _responseDTO;
        }

        //Retrive the existing product by id
        [HttpGet("{id}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product obj = _context.Products.FirstOrDefault(u => u.ProductId == id);
                if (obj == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Product not found";
                }
                else
                {
                    _responseDTO.Result = _mapper.Map<ProductDto>(obj);
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while retrieving product: {ex.Message}";
            }
            return _responseDTO;
        }

        //Create a new product
        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _context.Products.Add(product);
                _context.SaveChanges();
                _responseDTO.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while creating product: {ex.Message}";
            }
            return _responseDTO;
        }

        //Update an existing product
        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _context.Products.Update(product);
                _context.SaveChanges();
                _responseDTO.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while updating product: {ex.Message}";
            }
            return _responseDTO;
        }

        //Delete an existing product
        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product product = _context.Products.FirstOrDefault(u => u.ProductId == id);
                if (product == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Product not found";
                }
                else
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    _responseDTO.Result = _mapper.Map<ProductDto>(product);
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = $"An error occurred while deleting product: {ex.Message}";
            }
            return _responseDTO;
        }
    }
}
