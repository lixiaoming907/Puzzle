
public class SripteData
{

    public int curIndex;
    public int realIndex;

    public SripteData(int curIndex, int realIndex)
    {
        this.curIndex = curIndex;
        this.realIndex = realIndex;
    }

    public void ExchangeIndex(int _curIndex)
    {
        curIndex = _curIndex;
    }
}
