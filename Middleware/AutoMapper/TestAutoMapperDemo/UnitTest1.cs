using AutoMapper;
using AutoMapperDemo.Models;
using AutoMapperDemo.ViewModels;
using System;
using Xunit;

namespace TestAutoMapperDemo
{
    public class UnitTest1 : CommonHelper
    {
        [Fact]
        public void Test1()
        {
            User u = new User { 
                Id = 1,
                Address =new Address { 
                    Id = 1000,
                    State = "LA",
                    City = "asdf",
                },
                LastName = "adsf",
                FirstName = "asdfa",
            };
            IMapper _mapper = GetMapper();
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(u);
        }
    }
}
