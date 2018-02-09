namespace Farfetch.APIHandler.Common.DTO
{
    public class FarfetchMessage<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
    }
}