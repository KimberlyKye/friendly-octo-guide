using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public class HomeWorkFactory : IHomeWorkFactory
    {

        public Task<Entities.HomeWork> CreateFromAsync(DataModels.HomeWork homeWorkModel)
        {
            return Task.FromResult(new Entities.HomeWork
            {
                Id = homeWorkModel.Id,
                RoleId = (int)RoleEnum.Student,
                Name = homeWorkModel.Name.FirstName,
                Surname = homeWorkModel.Name.LastName,
                Email = homeWorkModel.Email.Value,
                PhoneNumber = homeWorkModel.PhoneNumber,
                DateOfBirth = homeWorkModel.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue),
                Password = homeWorkModel.Password
            });
        }

        public Task<DataModels.HomeWork> CreateDataModelAsync(Entities.HomeWork homeWork)
        {
            throw new NotImplementedException();
            
        }        
    }
}



//return Task.FromResult(new User
//{
//    Id = student.Id,
//    RoleId = (int)RoleEnum.Student,
//    Name = student.Name.FirstName,
//    Surname = student.Name.LastName,
//    Email = student.Email.Value,
//    PhoneNumber = student.PhoneNumber,
//    DateOfBirth = student.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue),
//    Password = student.Password
//});