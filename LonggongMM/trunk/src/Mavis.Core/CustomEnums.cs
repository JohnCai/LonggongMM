namespace Mavis.Core
{
    public class CustomEnums
    {
        #region LockMode enum

        public enum LockMode
        {
            None,
            Read,
            Upgrade,
            UpgradeNoWait,
            Write
        }

        #endregion
    }
}