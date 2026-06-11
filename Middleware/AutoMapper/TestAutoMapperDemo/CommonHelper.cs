using AutoMapper;
using AutoMapperDemo.MappingConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAutoMapperDemo
{
    public class CommonHelper
    {
        protected IMapper GetMapper()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });
            IMapper _mapper = new Mapper(mapperConfig);
            return _mapper;
        }
    }
}
