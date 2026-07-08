namespace CRUDEFCore.Common
{
    // Dipakai untuk operasi yang tidak mengembalikan data (Update, Delete, Assign, Register)
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public static ServiceResult Ok(string message = "Berhasil.") =>
            new() { Success = true, Message = message };

        public static ServiceResult Fail(string message, List<string>? errors = null) =>
            new() { Success = false, Message = message, Errors = errors ?? new List<string> { message } };
    }

    // Dipakai untuk operasi yang mengembalikan data (Get, Create, Login)
    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "Berhasil.") =>
            new() { Success = true, Data = data, Message = message };

        public static new ServiceResult<T> Fail(string message, List<string>? errors = null) =>
            new() { Success = false, Message = message, Errors = errors ?? new List<string> { message } };
    }
}
