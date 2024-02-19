
namespace cnauta.model.schema
{
    /// <summary>CNauta app configuration schema</summary>
    public class SchConfigData
    {
        public string DefaultUser { get; set; }
        public string DefaultUserPass { get; set; }

        /// <summary>Alternative user A</summary>
        public string AltAUSer { get; set; }
        public string AltAUSerPass { get; set; }

        /// <summary>Alternative user B</summary>
        public string AltBUSer { get; set; }
        public string AltBUSerPass { get; set; }

        public string LogIdToken { get; set; }
        public string UuidToken { get; set; }
        public string CsrfHwToken { get; set; }
        
        public bool AreWeConnected { get; set; }
        /// <summary>Tells which account is active</summary>
        /// <remarks>
        /// -1 => no active account
        /// 0 => default
        /// 1 => A
        /// 2 => B
        /// </remarks>
        public int ActiveAccount { get; set; }
    }
}

