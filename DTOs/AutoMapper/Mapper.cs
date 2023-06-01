using AutoMapper;
using E_PaymentSystemAPI.Data.Models;

namespace E_PaymentSystemAPI.DTOs.Configurations
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateUserDTO, User>().ReverseMap();
            //CreateMap<User, UserDTO>().ReverseMap();
            //CreateMap<Payment, PaymentDTO>().ReverseMap();
            //CreateMap<Payment, CreatePaymentDTO>().ReverseMap();
        }
    }
}
