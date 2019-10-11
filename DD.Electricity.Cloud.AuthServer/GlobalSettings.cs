using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer
{
    public class GlobalSettings
    {
        public static readonly string DbContext_DingDing = "dd";//总部上下文的key
        #region Scope相关
        public static readonly string Scope_Roles = "roles";
        public static readonly string Scope_Display_Name_Role = "角色";
        public static readonly string Scope_GroupId = "GroupId";
        #endregion


        #region API Resources
        public static readonly string API_For_Mobile_Client_Scope = "for_mobile";
        public static readonly string API_Client_ID = "API_Client_001";
        public static readonly string API_Client_Secret = "Client_001_DaCheng";
        #endregion

        #region 角色
        public static readonly string Temp_Role_Manager = "管理员";
        #endregion

    }
}
