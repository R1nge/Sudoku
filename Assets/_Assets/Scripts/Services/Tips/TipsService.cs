namespace _Assets.Scripts.Services.Tips
{
    public class TipsService
    {
        private int _tipsLeft = 5;
        public int TipsLeft => _tipsLeft;

        public bool Show()
        {
            if (_tipsLeft == 0)
            {
                return false;
            }

            _tipsLeft--;

            return true;
        }
    }
}