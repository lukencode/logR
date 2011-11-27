using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logR
{
    partial class LogR
    {
        /// <summary>
        /// Various configuration properties.
        /// </summary>
        public static class Settings
        {
            internal static void InitRouting()
            {
                UI.logRHandler.RegisterRoutes();
            }
        }
    }
}
