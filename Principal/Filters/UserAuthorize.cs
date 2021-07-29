using Principal.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Principal.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserAuthorize : AuthorizeAttribute
    {
        private RepairEverythingEntities assistant = new RepairEverythingEntities();
        private int operationId;
        private User user;

        public UserAuthorize(int operationId = 0)
        {
            this.operationId = operationId;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String operationName = ""; String moduleName = "";
    
            try
            {
                user = (User)HttpContext.Current.Session["user"];
                var operations = (from r in assistant.RoleOperations where r.role_id == user.role_id && r.operation_id == operationId select r);

                if (operations.ToList().Count() == 0)
                {
                    var operation = assistant.Operations.Find(operationId);

                    if (operation != null)
                    {
                        int? moduleId = operation.module_id;
                        operationName = OperationName(operationId);
                        moduleName = ModuleName(moduleId);
                    }

                    filterContext.Result = new RedirectResult("~/Error/Unauthorized?operation=" + operationName + "&module=" + moduleName + "&message=");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/Unauthorized?operation=" + operationName + "&module=" + moduleName + "&message=" + ex.Message);
            }
        }

        public string OperationName (int? operationId)
        {
            var operation = (from o in assistant.Operations where o.id == operationId select o.name);
            String operationName;

            try
            {
                operationName = operation.First();
            }
            catch (Exception)
            {
                operationName = "";
            }

            return operationName;
        }

        public string ModuleName (int? moduleId)
        {
            var module = (from m in assistant.Modules where m.id == moduleId select m.name);
            String moduleName;

            try
            {
                moduleName = module.First();
            }
            catch (Exception)
            {
                moduleName = "";
            }

            return moduleName;
        }
    }
}