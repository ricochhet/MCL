namespace MCL.Core.Interfaces;

public interface IErrorHandleItem<in T>
{
    public static abstract bool Exists(T type);
}
