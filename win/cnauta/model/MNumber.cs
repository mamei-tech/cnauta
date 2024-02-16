namespace cnauta.model
{
    internal class MNumber
    {
        #region ============ Fields ==================================================

        private int number;

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================
        
        public MNumber()
        {
            this.number = 0;
        }
        
        #endregion ===================================================================
        
        #region ============ METHODS =================================================
        
        public void Increment()
        {
            this.number++;
        }

        public override string ToString()
        {
            return this.number.ToString();
        }
        
        #endregion ===================================================================
        
    }
}