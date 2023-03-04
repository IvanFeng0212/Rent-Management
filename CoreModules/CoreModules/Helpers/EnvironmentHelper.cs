using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Helpers
{
    public class EnvironmentHelper
    {
        public static string GetSecretKey()
        {
            return Environment.GetEnvironmentVariable("RM_SecretKey") ?? string.Empty;
        }

        public static string GetFirebaseUrl()
        {
            return Environment.GetEnvironmentVariable("FirebaseUrl") ?? string.Empty;
        }

        public static string GetFirebaseAuthKey()
        {
            return Environment.GetEnvironmentVariable("FirebaseAuthKey") ?? string.Empty;
        }
    }
}
