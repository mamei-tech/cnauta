
namespace cnauta.model.schema
{
    /// <summary>User / account credential</summary>
    public class SchCredential
    {
        public string User { get; set; }
        public string Pass { get; set; }
        
        /// <summary>
        /// Tells which account is active, matching one of the values defines in <see cref="SchConfigData.ActiveAccount"/> docstring
        /// </summary>
        /// <remarks>
        /// -1 => no active account
        /// 0 => default
        /// 1 => A
        /// 2 => B
        /// </remarks>
        public int ActiveAccIndex { get; set; }
    }
}

