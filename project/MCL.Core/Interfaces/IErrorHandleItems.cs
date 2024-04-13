namespace MCL.Core.Interfaces;

public interface IErrorHandleItems<in T1, in T2>
{
    public static abstract bool Exists(T1 type1, T2 type2);
}
