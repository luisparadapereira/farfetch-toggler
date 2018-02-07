namespace Farfetch.APIHandler.TogglerAPI.DTO
{
    public class TogglerMessage<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
    }
}