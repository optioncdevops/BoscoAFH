using BoscoAFH.Common;
using BoscoAFH.DBEngine;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Repositorys
{
    public class AuthenticateRepository(IDapperHandler _repository) : IAuthenticateRepository
    {
        public async Task<UserDetailResult> AuthenticateUserAsync(UserCredential user)
        {
            var objResult = new UserDetailResult();

            try
            {


                string loginQuery = "SELECT user_id as UserId, user_name as UserName , password_hash as Password, full_name as FullName, email as Email, mobile_no, is_active as IsActive,role_id as RoleId FROM public.users where user_name='" + user.UserName + "'";

                objResult.UserDetails = await _repository.QueryFirstOrDefaultAsync<UserDetailDTO>(loginQuery);

                objResult.StatusCode = 200;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objResult;
        }
    }
}
