using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BoscoAFH.Base
{
    public class GenericMappingProfile<T1, T2> : Profile
    {
        public GenericMappingProfile()
        {
            // Create a map between T1 and T2, with reverse mapping
            CreateMap<T1, T2>().ReverseMap();
        }
    }
}
