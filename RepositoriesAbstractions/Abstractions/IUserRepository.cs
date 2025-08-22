using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects.Enums;
using Entities;

namespace RepositoriesAbstractions.Abstractions
{

    public interface IUserRepository
    {
        /// <summary>
        /// Получить роль пользователя по id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RoleEnum?> GetUserRoleAsync(int userId);
    }

}