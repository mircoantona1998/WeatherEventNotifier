using Microsoft.AspNetCore.Mvc;
namespace EntityFramework.Utils
{
    public class UserdataLib : ControllerBase
    {

        #region UTILS
        /// <summary>
        /// Validate files checking extension
        /// </summary>
        public static bool IsNullOrZero(float? toBeChecked = null) => toBeChecked == null || (int)toBeChecked == 0;
        #endregion
    }
}
