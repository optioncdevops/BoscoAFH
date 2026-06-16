using BoscoAFH.MasterInfrastructure.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.MasterInfrastructure.Models.OutPut
{
    public class RoleResult
    {
        public RoleDTO? Role { get; set; }
        public List<RoleDTO>? RoleList { get; set; }
    }
}
