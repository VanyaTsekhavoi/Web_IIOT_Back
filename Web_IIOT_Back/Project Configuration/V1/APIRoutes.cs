using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_IIOT_Back.Project_Configuration.V1
{
    public static class APIRoutes
    {
        public const string Root = "api";

        public const string Version = "V1";

        public const string Base = Root + "/" + Version;

        public static class Box
        {
            public const string GetAllInfo = Base + "/Box";
        }

    }
}
