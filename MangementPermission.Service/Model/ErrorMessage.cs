using System.ComponentModel.DataAnnotations;

namespace MangementPermission.Service.Model
{
    public struct ErrorMessage
    {
        public static string QuantityInvalid = "Quatity is invalid";
        public static string QuantityNotFix = "Quatity doesn't fix with number of User";
        public static string QuantityOutOfRange = "Quatity is out of range";
        public static string PermissionOutOfRange = "Permission is out of range";
        public static string PermissionInvalid = "Permission is invalid";
        public static string ManagerInvalid = "Manager is invalid";
        public static string CEONoMember = "CEO hasn't member";
        public static string QueryInvalid = "Query is invalid";
    }
}
