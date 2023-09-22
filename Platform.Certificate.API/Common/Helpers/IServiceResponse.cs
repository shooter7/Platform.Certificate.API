namespace Platform.Certificate.API.Common.Helpers;

public interface IServiceResponse
{
    IReadOnlyCollection<IResponseError> Errors { get; }
    string? Message { get; }
    bool Succeeded { get; }
    int ItemsCount { get; }
}

public interface IServiceResponse<out T> : IServiceResponse
{
    T? Data { get; }
}