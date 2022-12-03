namespace Chess;

public interface ICaptured
{

    public bool IsPieceCaptured();

    public void IsPieceCaptured(bool isOnBoard);
}