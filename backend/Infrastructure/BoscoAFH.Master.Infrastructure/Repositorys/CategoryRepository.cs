using BoscoAFH.DBEngine;
using BoscoAFH.MasterInfrastructure.Interfaces;
using BoscoAFH.MasterInfrastructure.Models.Input;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Repositorys
{

    /// <summary>
    /// Repository class interacting directly with School Workload databases logic mappings using Dapper.
    /// Infrastructure Responsibility:
    /// - Interacts directly with the database using IDapperHandler to run a multi-result query.
    /// </summary>
    public class CategoryRepository(IDapperHandler _dapperHandler) : ICategoryRepository
    {
        #region GET Methods
        public async Task<dynamic> GetCategoryAsync()
        {
            var result = await _dapperHandler.QueryAsync<dynamic>(SQLQuery.Category.GetcategoryList);
            return result.ToList();
        }

        public async Task<List<CategoryDTO>> GetCategoryListAsync()
        {

            return [.. (await _dapperHandler.QueryAsync<CategoryDTO>(SQLQuery.Category.GetcategoryList))];
        }

        #endregion GET Methods
    }
}
