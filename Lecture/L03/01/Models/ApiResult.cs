namespace _01.Models
{
    public class ApiResult
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Result { get; set; }
    }
}
