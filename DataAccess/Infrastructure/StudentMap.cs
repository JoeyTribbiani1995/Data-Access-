using System;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Models.ViewModels;

namespace DataAccess.Infrastructure
{
    public class StudentMap : Profile
    {
        public StudentMap()
        {
            CreateMap<Student, StudentViewModel>();
        }
    }
}
