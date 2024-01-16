using Microsoft.AspNetCore.Mvc;
namespace EntityFramework.Classes
{
    public class SLAManagerdataLib : ControllerBase
    {

        #region UTILS
        /// <summary>
        /// Validate files checking extension
        /// </summary>
        public static bool IsNullOrZero(float? toBeChecked = null) => toBeChecked == null || (int)toBeChecked == 0;
        #endregion
    }
}
