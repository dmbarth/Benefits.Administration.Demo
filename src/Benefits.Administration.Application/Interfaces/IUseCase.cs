namespace Benefits.Administration.Application.Interfaces
{
    public interface IUseCase<out TOut, in TIn1>
    {
        TOut InvokeAsync(TIn1 in1);
    }

    public interface IUseCase<out TOut, in TIn1, in TIn2>
    {
        TOut InvokeAsync(TIn1 in1, TIn2 in2);
    }

    public interface IUseCase<out TOut, in TIn1, in TIn2, in TIn3>
    {
        TOut InvokeAsync(TIn1 in1, TIn2 in2, TIn3 in3);
    }
}
